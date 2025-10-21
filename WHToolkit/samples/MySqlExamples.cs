using System.Data;
using HWH.Database;

namespace HWH.Framework.Examples;

/// <summary>
/// MySQL 전용 사용 예제들
/// </summary>
public class MySqlExamples
{
    private const string ConnectionString = "Server=localhost;Database=testdb;Uid=root;Pwd=password;Charset=utf8mb4;";

    /// <summary>
    /// MySQL 기본 CRUD 작업 예제
    /// </summary>
    public async Task BasicCrudExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MySQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("=== MySQL 기본 CRUD 예제 ===");

        // CREATE - AUTO_INCREMENT를 사용한 사용자 추가
        var insertParams = new DataParameterCollection();
        insertParams.Add("p_name", "김마이에스큐엘");
        insertParams.Add("p_email", "kim@mysql.com");
        insertParams.Add("p_age", 28);

        var insertSql = @"
            INSERT INTO users (name, email, age, status, created_date) 
            VALUES (p_name, p_email, p_age, 'Active', NOW())";

        await db.ExecuteNonQueryAsync(CommandType.Text, insertSql, insertParams);

        // 방금 삽입된 ID 가져오기
        var newUserId = await db.ExecuteScalarAsync(CommandType.Text, "SELECT LAST_INSERT_ID()");
        Console.WriteLine($"새 사용자 생성 완료. ID: {newUserId}");

        // READ - 사용자 조회
        var selectParams = new DataParameterCollection();
        selectParams.Add("p_user_id", newUserId);

        var selectSql = "SELECT id, name, email, age, status, created_date FROM users WHERE id = p_user_id";
        var userData = await db.ExecuteDataSetAsync(CommandType.Text, selectSql, selectParams);

        if (userData.Tables.Count > 0 && userData.Tables[0].Rows.Count > 0)
        {
            var row = userData.Tables[0].Rows[0];
            Console.WriteLine($"조회된 사용자: {row["name"]} ({row["email"]})");
        }

        // UPDATE - 사용자 정보 수정
        var updateParams = new DataParameterCollection();
        updateParams.Add("p_user_id", newUserId);
        updateParams.Add("p_new_email", "kim.updated@mysql.com");

        var updateSql = "UPDATE users SET email = p_new_email, updated_date = NOW() WHERE id = p_user_id";
        var updatedRows = await db.ExecuteNonQueryAsync(CommandType.Text, updateSql, updateParams);
        Console.WriteLine($"업데이트된 행 수: {updatedRows}");

        // DELETE - 사용자 삭제 (실제로는 비활성화)
        var deleteParams = new DataParameterCollection();
        deleteParams.Add("p_user_id", newUserId);

        var deleteSql = "UPDATE users SET status = 'Inactive', updated_date = NOW() WHERE id = p_user_id";
        var deletedRows = await db.ExecuteNonQueryAsync(CommandType.Text, deleteSql, deleteParams);
        Console.WriteLine($"비활성화된 행 수: {deletedRows}");
    }

    /// <summary>
    /// MySQL 트랜잭션 예제
    /// </summary>
    public async Task TransactionExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MySQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== MySQL 트랜잭션 예제 ===");

        try
        {
            await db.TransactionBeginAsync();

            // 1. 새 주문 생성
            var orderParams = new DataParameterCollection();
            orderParams.Add("p_user_id", 1);
            orderParams.Add("p_total_amount", 175.50m);

            var orderSql = @"
                INSERT INTO orders (user_id, order_date, total_amount, status) 
                VALUES (p_user_id, NOW(), p_total_amount, 'Processing')";

            await db.ExecuteNonQueryAsync(CommandType.Text, orderSql, orderParams);

            // 방금 삽입된 주문 ID 가져오기
            var orderId = await db.ExecuteScalarAsync(CommandType.Text, "SELECT LAST_INSERT_ID()");
            Console.WriteLine($"주문 생성 완료. 주문 ID: {orderId}");

            // 2. 주문 항목들 추가
            var orderItems = new[]
            {
                new { ProductId = 1, Quantity = 2, UnitPrice = 45.25m },
                new { ProductId = 2, Quantity = 3, UnitPrice = 28.33m }
            };

            foreach (var item in orderItems)
            {
                var itemParams = new DataParameterCollection();
                itemParams.Add("p_order_id", orderId);
                itemParams.Add("p_product_id", item.ProductId);
                itemParams.Add("p_quantity", item.Quantity);
                itemParams.Add("p_unit_price", item.UnitPrice);

                var itemSql = @"
                    INSERT INTO order_items (order_id, product_id, quantity, unit_price) 
                    VALUES (p_order_id, p_product_id, p_quantity, p_unit_price)";

                await db.ExecuteNonQueryAsync(CommandType.Text, itemSql, itemParams);
            }

            // 3. 재고 업데이트
            foreach (var item in orderItems)
            {
                var stockParams = new DataParameterCollection();
                stockParams.Add("p_product_id", item.ProductId);
                stockParams.Add("p_quantity", item.Quantity);

                var stockSql = @"
                    UPDATE products 
                    SET stock = stock - p_quantity, updated_date = NOW() 
                    WHERE id = p_product_id AND stock >= p_quantity";

                var affectedRows = await db.ExecuteNonQueryAsync(CommandType.Text, stockSql, stockParams);
                
                if (affectedRows == 0)
                {
                    throw new InvalidOperationException($"상품 ID {item.ProductId}의 재고가 부족합니다.");
                }
            }

            await db.TransactionCommitAsync();
            Console.WriteLine("주문 처리가 성공적으로 완료되었습니다.");
        }
        catch (Exception ex)
        {
            await db.TransactionRollbackAsync();
            Console.WriteLine($"트랜잭션 롤백됨: {ex.Message}");
        }
    }

    /// <summary>
    /// MySQL 저장 프로시저 예제
    /// </summary>
    public async Task StoredProcedureExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MySQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== MySQL 저장 프로시저 예제 ===");

        // 저장 프로시저 호출
        var procParams = new DataParameterCollection();
        procParams.Add(ParameterDirection.Input, "p_start_date", DateTime.Now.AddDays(-30));
        procParams.Add(ParameterDirection.Input, "p_end_date", DateTime.Now);
        procParams.Add(ParameterDirection.Output, DbType.Int32, "p_total_orders", null);
        procParams.Add(ParameterDirection.Output, DbType.Decimal, "p_total_revenue", null);

        try
        {
            var result = await db.ExecuteDataSetAsync(CommandType.StoredProcedure, "GetSalesReport", procParams);

            Console.WriteLine("월간 판매 보고서:");
            if (result.Tables.Count > 0)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    Console.WriteLine($"날짜: {row["order_date"]}, 주문수: {row["order_count"]}, 매출: ${row["revenue"]:F2}");
                }
            }

            // 출력 매개변수 값 확인
            Console.WriteLine($"총 주문수: {procParams["p_total_orders"].Value}");
            Console.WriteLine($"총 매출: ${procParams["p_total_revenue"].Value:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"저장 프로시저 실행 실패: {ex.Message}");
        }

        // MySQL 함수 호출
        var funcParams = new DataParameterCollection();
        funcParams.Add("p_user_id", 1);

        var funcSql = "SELECT GetUserTotalSpent(p_user_id) as total_spent";
        
        try
        {
            var funcResult = await db.ExecuteScalarAsync(CommandType.Text, funcSql, funcParams);
            Console.WriteLine($"사용자 총 구매 금액: ${funcResult:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"함수 실행 실패: {ex.Message}");
        }
    }

    /// <summary>
    /// MySQL 대용량 데이터 처리 예제
    /// </summary>
    public async Task BulkDataExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MySQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== MySQL 대용량 데이터 처리 예제 ===");

        // 대용량 조회 - LIMIT와 OFFSET 사용
        var pageSize = 100;
        
        // 전체 레코드 수 조회
        var countSql = "SELECT COUNT(*) FROM users WHERE status = 'Active'";
        var countResult = await db.ExecuteScalarAsync(CommandType.Text, countSql);
        var totalRecords = Convert.ToInt32(countResult);

        Console.WriteLine($"전체 활성 사용자 수: {totalRecords:N0}");

        // 페이지별로 데이터 조회
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        for (int page = 1; page <= Math.Min(3, totalPages); page++) // 첫 3페이지만 예시로 출력
        {
            var offset = (page - 1) * pageSize;
            
            var pageParams = new DataParameterCollection();
            pageParams.Add("p_limit", pageSize);
            pageParams.Add("p_offset", offset);

            var pageSql = @"
                SELECT id, name, email, created_date 
                FROM users 
                WHERE status = 'Active'
                ORDER BY created_date DESC
                LIMIT p_limit OFFSET p_offset";

            var pageData = await db.ExecuteDataSetAsync(CommandType.Text, pageSql, pageParams);

            Console.WriteLine($"페이지 {page}/{totalPages} ({pageData.Tables[0].Rows.Count}개 행):");
            foreach (DataRow row in pageData.Tables[0].Rows)
            {
                Console.WriteLine($"  {row["name"]} ({row["email"]}) - {row["created_date"]:yyyy-MM-dd}");
            }
        }

        // 대용량 삽입 - INSERT IGNORE 사용
        var bulkInsertSql = @"
            INSERT IGNORE INTO bulk_test (name, email, created_date)
            VALUES 
                ('User1', 'user1@test.com', NOW()),
                ('User2', 'user2@test.com', NOW()),
                ('User3', 'user3@test.com', NOW()),
                ('User4', 'user4@test.com', NOW()),
                ('User5', 'user5@test.com', NOW())";

        var insertedRows = await db.ExecuteNonQueryAsync(CommandType.Text, bulkInsertSql);
        Console.WriteLine($"대량 삽입 완료: {insertedRows}개 행");
    }

    /// <summary>
    /// MySQL 특화 기능 예제
    /// </summary>
    public async Task MySqlSpecificFeaturesExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MySQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== MySQL 특화 기능 예제 ===");

        // 1. JSON 데이터 처리 (MySQL 5.7+)
        var jsonParams = new DataParameterCollection();
        jsonParams.Add("p_user_preferences", @"{""language"":""ko"",""theme"":""dark"",""notifications"":true}");
        jsonParams.Add("p_user_id", 1);

        var jsonSql = @"
            UPDATE users 
            SET preferences = p_user_preferences 
            WHERE id = p_user_id";

        await db.ExecuteNonQueryAsync(CommandType.Text, jsonSql, jsonParams);

        // JSON 데이터 조회
        var jsonQuerySql = @"
            SELECT 
                name,
                JSON_EXTRACT(preferences, '$.language') as language,
                JSON_EXTRACT(preferences, '$.theme') as theme,
                JSON_EXTRACT(preferences, '$.notifications') as notifications,
                JSON_UNQUOTE(JSON_EXTRACT(preferences, '$.language')) as language_clean
            FROM users 
            WHERE id = p_user_id AND preferences IS NOT NULL";

        var jsonResult = await db.ExecuteDataSetAsync(CommandType.Text, jsonQuerySql, 
            new DataParameterCollection { { "p_user_id", 1 } });

        if (jsonResult.Tables.Count > 0 && jsonResult.Tables[0].Rows.Count > 0)
        {
            var row = jsonResult.Tables[0].Rows[0];
            Console.WriteLine($"JSON 설정 - 사용자: {row["name"]}, 언어: {row["language_clean"]}, 테마: {row["theme"]}");
        }

        // 2. 전체 텍스트 검색 (Full-Text Search)
        var searchParams = new DataParameterCollection();
        searchParams.Add("p_search_term", "MySQL 데이터베이스");

        var fullTextSql = @"
            SELECT id, name, description, 
                   MATCH(name, description) AGAINST(p_search_term IN NATURAL LANGUAGE MODE) as relevance
            FROM products 
            WHERE MATCH(name, description) AGAINST(p_search_term IN NATURAL LANGUAGE MODE)
            ORDER BY relevance DESC";

        try
        {
            var searchResult = await db.ExecuteDataSetAsync(CommandType.Text, fullTextSql, searchParams);
            Console.WriteLine($"전체 텍스트 검색 결과: {searchResult.Tables[0].Rows.Count}개");
            
            foreach (DataRow row in searchResult.Tables[0].Rows)
            {
                Console.WriteLine($"  상품: {row["name"]}, 관련도: {row["relevance"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"전체 텍스트 검색 실행 실패 (인덱스 미설정?): {ex.Message}");
        }

        // 3. ON DUPLICATE KEY UPDATE
        var upsertParams = new DataParameterCollection();
        upsertParams.Add("p_email", "unique@example.com");
        upsertParams.Add("p_name", "유니크 사용자");
        upsertParams.Add("p_age", 25);

        var upsertSql = @"
            INSERT INTO users (email, name, age, status, created_date)
            VALUES (p_email, p_name, p_age, 'Active', NOW())
            ON DUPLICATE KEY UPDATE
                name = VALUES(name),
                age = VALUES(age),
                updated_date = NOW()";

        var upsertRows = await db.ExecuteNonQueryAsync(CommandType.Text, upsertSql, upsertParams);
        Console.WriteLine($"UPSERT 작업 완료: {upsertRows}개 행 영향받음");

        // 4. 윈도우 함수 (MySQL 8.0+)
        var windowSql = @"
            SELECT 
                name,
                email,
                created_date,
                ROW_NUMBER() OVER (ORDER BY created_date DESC) as row_num,
                RANK() OVER (ORDER BY created_date DESC) as rank_num,
                DENSE_RANK() OVER (ORDER BY created_date DESC) as dense_rank_num,
                LAG(name, 1) OVER (ORDER BY created_date) as prev_user,
                LEAD(name, 1) OVER (ORDER BY created_date) as next_user
            FROM users 
            WHERE status = 'Active'
            LIMIT 10";

        try
        {
            var windowResult = await db.ExecuteDataSetAsync(CommandType.Text, windowSql);
            Console.WriteLine("\n윈도우 함수 결과:");
            foreach (DataRow row in windowResult.Tables[0].Rows)
            {
                Console.WriteLine($"순위 {row["row_num"]}: {row["name"]} (이전: {row["prev_user"] ?? "없음"})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"윈도우 함수 실행 실패 (MySQL 8.0+ 필요): {ex.Message}");
        }

        // 5. CTE (Common Table Expression) - MySQL 8.0+
        var cteSql = @"
            WITH RECURSIVE employee_hierarchy AS (
                SELECT id, name, manager_id, 1 as level
                FROM employees
                WHERE manager_id IS NULL
                
                UNION ALL
                
                SELECT e.id, e.name, e.manager_id, eh.level + 1
                FROM employees e
                INNER JOIN employee_hierarchy eh ON e.manager_id = eh.id
            )
            SELECT id, name, level, LPAD('', (level-1)*2, ' ') as indent
            FROM employee_hierarchy
            ORDER BY level, name";

        try
        {
            var cteResult = await db.ExecuteDataSetAsync(CommandType.Text, cteSql);
            Console.WriteLine("\n직원 계층 구조 (CTE):");
            foreach (DataRow row in cteResult.Tables[0].Rows)
            {
                Console.WriteLine($"{row["indent"]}레벨 {row["level"]}: {row["name"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CTE 실행 실패: {ex.Message}");
        }
    }

    /// <summary>
    /// MySQL 성능 최적화 예제
    /// </summary>
    public async Task PerformanceExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MySQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== MySQL 성능 최적화 예제 ===");

        // 1. EXPLAIN을 사용한 실행 계획 분석
        var explainSql = @"
            EXPLAIN FORMAT=JSON
            SELECT u.name, COUNT(o.id) as order_count, SUM(o.total_amount) as total_spent
            FROM users u
            LEFT JOIN orders o ON u.id = o.user_id
            WHERE u.status = 'Active'
            GROUP BY u.id, u.name
            HAVING COUNT(o.id) > 0
            ORDER BY total_spent DESC";

        var explainResult = await db.ExecuteDataSetAsync(CommandType.Text, explainSql);
        
        if (explainResult.Tables.Count > 0 && explainResult.Tables[0].Rows.Count > 0)
        {
            Console.WriteLine("실행 계획 (JSON):");
            Console.WriteLine(explainResult.Tables[0].Rows[0]["EXPLAIN"]);
        }

        // 2. 인덱스 힌트 사용
        var hintSql = @"
            SELECT /*+ USE_INDEX(u, idx_user_status) */
                u.name, o.order_date, o.total_amount
            FROM users u USE INDEX (idx_user_status)
            INNER JOIN orders o ON u.id = o.user_id
            WHERE u.status = 'Active'
            AND o.order_date >= DATE_SUB(NOW(), INTERVAL 30 DAY)";

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var hintResult = await db.ExecuteDataSetAsync(CommandType.Text, hintSql);
        stopwatch.Stop();

        Console.WriteLine($"인덱스 힌트 사용 쿼리 실행 시간: {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine($"결과 행 수: {hintResult.Tables[0].Rows.Count}");

        // 3. 프로파일링 활성화
        await db.ExecuteNonQueryAsync(CommandType.Text, "SET profiling = 1");

        // 프로파일링할 쿼리 실행
        var profileSql = @"
            SELECT u.name, COUNT(o.id) as order_count
            FROM users u
            LEFT JOIN orders o ON u.id = o.user_id
            WHERE u.status = 'Active'
            GROUP BY u.id, u.name
            LIMIT 100";

        await db.ExecuteDataSetAsync(CommandType.Text, profileSql);

        // 프로파일링 결과 조회
        var profileResults = await db.ExecuteDataSetAsync(CommandType.Text, "SHOW PROFILES");
        
        Console.WriteLine("\n쿼리 프로파일링 결과:");
        foreach (DataRow row in profileResults.Tables[0].Rows)
        {
            Console.WriteLine($"Query ID: {row["Query_ID"]}, Duration: {row["Duration"]}s, Query: {row["Query"].ToString()?.Substring(0, Math.Min(50, row["Query"].ToString()?.Length ?? 0))}...");
        }

        // 상세 프로파일링 정보
        if (profileResults.Tables[0].Rows.Count > 0)
        {
            var queryId = profileResults.Tables[0].Rows[profileResults.Tables[0].Rows.Count - 1]["Query_ID"];
            var detailProfile = await db.ExecuteDataSetAsync(CommandType.Text, 
                $"SHOW PROFILE FOR QUERY {queryId}");

            Console.WriteLine($"\nQuery {queryId} 상세 프로파일:");
            foreach (DataRow row in detailProfile.Tables[0].Rows)
            {
                Console.WriteLine($"  {row["Status"]}: {row["Duration"]}s");
            }
        }

        // 프로파일링 비활성화
        await db.ExecuteNonQueryAsync(CommandType.Text, "SET profiling = 0");
    }
}
using System.Data;
using HWH.Database;

namespace HWH.Framework.Examples;

/// <summary>
/// PostgreSQL 전용 사용 예제들
/// </summary>
public class PostgreSqlExamples
{
    private const string ConnectionString = "Host=localhost;Database=testdb;Username=postgres;Password=password;";

    /// <summary>
    /// PostgreSQL 기본 CRUD 작업 예제
    /// </summary>
    public async Task BasicCrudExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.PostgreSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("=== PostgreSQL 기본 CRUD 예제 ===");

        // CREATE - SERIAL/IDENTITY를 사용한 사용자 추가
        var insertParams = new DataParameterCollection();
        insertParams.Add("name_param", "김포스트그레");
        insertParams.Add("email_param", "kim@postgresql.com");
        insertParams.Add("age_param", 32);

        var insertSql = @"
            INSERT INTO users (name, email, age, status, created_date) 
            VALUES (@name_param, @email_param, @age_param, 'Active', NOW())
            RETURNING id";

        var newUserId = await db.ExecuteScalarAsync(CommandType.Text, insertSql, insertParams);
        Console.WriteLine($"새 사용자 생성 완료. ID: {newUserId}");

        // READ - 사용자 조회
        var selectParams = new DataParameterCollection();
        selectParams.Add("user_id_param", newUserId);

        var selectSql = "SELECT id, name, email, age, status, created_date FROM users WHERE id = @user_id_param";
        var userData = await db.ExecuteDataSetAsync(CommandType.Text, selectSql, selectParams);

        if (userData.Tables.Count > 0 && userData.Tables[0].Rows.Count > 0)
        {
            var row = userData.Tables[0].Rows[0];
            Console.WriteLine($"조회된 사용자: {row["name"]} ({row["email"]})");
        }

        // UPDATE - 사용자 정보 수정
        var updateParams = new DataParameterCollection();
        updateParams.Add("user_id_param", newUserId);
        updateParams.Add("new_email_param", "kim.updated@postgresql.com");

        var updateSql = "UPDATE users SET email = @new_email_param, updated_date = NOW() WHERE id = @user_id_param";
        var updatedRows = await db.ExecuteNonQueryAsync(CommandType.Text, updateSql, updateParams);
        Console.WriteLine($"업데이트된 행 수: {updatedRows}");

        // DELETE - 사용자 삭제 (실제로는 비활성화)
        var deleteParams = new DataParameterCollection();
        deleteParams.Add("user_id_param", newUserId);

        var deleteSql = "UPDATE users SET status = 'Inactive', updated_date = NOW() WHERE id = @user_id_param";
        var deletedRows = await db.ExecuteNonQueryAsync(CommandType.Text, deleteSql, deleteParams);
        Console.WriteLine($"비활성화된 행 수: {deletedRows}");
    }

    /// <summary>
    /// PostgreSQL 트랜잭션 예제
    /// </summary>
    public async Task TransactionExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.PostgreSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== PostgreSQL 트랜잭션 예제 ===");

        try
        {
            await db.TransactionBeginAsync();

            // 1. 새 주문 생성
            var orderParams = new DataParameterCollection();
            orderParams.Add("user_id_param", 1);
            orderParams.Add("total_amount_param", 320.75m);

            var orderSql = @"
                INSERT INTO orders (user_id, order_date, total_amount, status) 
                VALUES (@user_id_param, NOW(), @total_amount_param, 'Processing')
                RETURNING id";

            var orderId = await db.ExecuteScalarAsync(CommandType.Text, orderSql, orderParams);
            Console.WriteLine($"주문 생성 완료. 주문 ID: {orderId}");

            // 2. 주문 항목들 추가
            var orderItems = new[]
            {
                new { ProductId = 1, Quantity = 4, UnitPrice = 75.25m },
                new { ProductId = 2, Quantity = 1, UnitPrice = 19.25m }
            };

            foreach (var item in orderItems)
            {
                var itemParams = new DataParameterCollection();
                itemParams.Add("order_id_param", orderId);
                itemParams.Add("product_id_param", item.ProductId);
                itemParams.Add("quantity_param", item.Quantity);
                itemParams.Add("unit_price_param", item.UnitPrice);

                var itemSql = @"
                    INSERT INTO order_items (order_id, product_id, quantity, unit_price) 
                    VALUES (@order_id_param, @product_id_param, @quantity_param, @unit_price_param)";

                await db.ExecuteNonQueryAsync(CommandType.Text, itemSql, itemParams);
            }

            // 3. 재고 업데이트
            foreach (var item in orderItems)
            {
                var stockParams = new DataParameterCollection();
                stockParams.Add("product_id_param", item.ProductId);
                stockParams.Add("quantity_param", item.Quantity);

                var stockSql = @"
                    UPDATE products 
                    SET stock = stock - @quantity_param, updated_date = NOW() 
                    WHERE id = @product_id_param AND stock >= @quantity_param";

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
    /// PostgreSQL 저장 함수 예제
    /// </summary>
    public async Task StoredFunctionExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.PostgreSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== PostgreSQL 저장 함수 예제 ===");

        // PostgreSQL 함수 호출
        var funcParams = new DataParameterCollection();
        funcParams.Add("start_date_param", DateTime.Now.AddDays(-30));
        funcParams.Add("end_date_param", DateTime.Now);

        var funcSql = "SELECT * FROM get_sales_report(@start_date_param, @end_date_param)";

        try
        {
            var result = await db.ExecuteDataSetAsync(CommandType.Text, funcSql, funcParams);

            Console.WriteLine("월간 판매 보고서:");
            if (result.Tables.Count > 0)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    Console.WriteLine($"날짜: {row["order_date"]}, 주문수: {row["order_count"]}, 매출: ${row["revenue"]:F2}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"저장 함수 실행 실패: {ex.Message}");
        }

        // 스칼라 함수 호출
        var scalarParams = new DataParameterCollection();
        scalarParams.Add("user_id_param", 1);

        var scalarSql = "SELECT get_user_total_spent(@user_id_param) as total_spent";

        try
        {
            var funcResult = await db.ExecuteScalarAsync(CommandType.Text, scalarSql, scalarParams);
            Console.WriteLine($"사용자 총 구매 금액: ${funcResult:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"스칼라 함수 실행 실패: {ex.Message}");
        }

        // 테이블 반환 함수
        var tableParams = new DataParameterCollection();
        tableParams.Add("category_param", "Electronics");

        var tableSql = "SELECT * FROM get_products_by_category(@category_param)";

        try
        {
            var tableResult = await db.ExecuteDataSetAsync(CommandType.Text, tableSql, tableParams);
            Console.WriteLine($"\n카테고리별 상품 목록: {tableResult.Tables[0].Rows.Count}개");
            
            foreach (DataRow row in tableResult.Tables[0].Rows)
            {
                Console.WriteLine($"  상품: {row["name"]}, 가격: ${row["price"]:F2}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"테이블 반환 함수 실행 실패: {ex.Message}");
        }
    }

    /// <summary>
    /// PostgreSQL 대용량 데이터 처리 예제
    /// </summary>
    public async Task BulkDataExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.PostgreSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== PostgreSQL 대용량 데이터 처리 예제 ===");

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
            pageParams.Add("limit_param", pageSize);
            pageParams.Add("offset_param", offset);

            var pageSql = @"
                SELECT id, name, email, created_date 
                FROM users 
                WHERE status = 'Active'
                ORDER BY created_date DESC
                LIMIT @limit_param OFFSET @offset_param";

            var pageData = await db.ExecuteDataSetAsync(CommandType.Text, pageSql, pageParams);

            Console.WriteLine($"페이지 {page}/{totalPages} ({pageData.Tables[0].Rows.Count}개 행):");
            foreach (DataRow row in pageData.Tables[0].Rows)
            {
                Console.WriteLine($"  {row["name"]} ({row["email"]}) - {row["created_date"]:yyyy-MM-dd}");
            }
        }

        // COPY 명령을 사용한 대량 삽입 (실제 구현에서는 PostgreSQL 전용 라이브러리 필요)
        var bulkInsertSql = @"
            INSERT INTO bulk_test (name, email, created_date)
            VALUES 
                ('User1', 'user1@test.com', NOW()),
                ('User2', 'user2@test.com', NOW()),
                ('User3', 'user3@test.com', NOW()),
                ('User4', 'user4@test.com', NOW()),
                ('User5', 'user5@test.com', NOW())
            ON CONFLICT (email) DO NOTHING";

        var insertedRows = await db.ExecuteNonQueryAsync(CommandType.Text, bulkInsertSql);
        Console.WriteLine($"대량 삽입 완료: {insertedRows}개 행");
    }

    /// <summary>
    /// PostgreSQL 특화 기능 예제
    /// </summary>
    public async Task PostgreSqlSpecificFeaturesExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.PostgreSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== PostgreSQL 특화 기능 예제 ===");

        // 1. JSONB 데이터 처리
        var jsonParams = new DataParameterCollection();
        jsonParams.Add("user_preferences_param", @"{""language"":""ko"",""theme"":""dark"",""notifications"":true,""settings"":{""autoSave"":true,""fontSize"":14}}");
        jsonParams.Add("user_id_param", 1);

        var jsonSql = @"
            UPDATE users 
            SET preferences = @user_preferences_param::jsonb 
            WHERE id = @user_id_param";

        await db.ExecuteNonQueryAsync(CommandType.Text, jsonSql, jsonParams);

        // JSONB 데이터 조회 및 연산
        var jsonQuerySql = @"
            SELECT 
                name,
                preferences->>'language' as language,
                preferences->>'theme' as theme,
                (preferences->'notifications')::boolean as notifications,
                preferences->'settings'->>'fontSize' as font_size,
                preferences ? 'language' as has_language_setting,
                preferences @> '{""theme"":""dark""}' as is_dark_theme
            FROM users 
            WHERE id = @user_id_param AND preferences IS NOT NULL";

        var jsonResult = await db.ExecuteDataSetAsync(CommandType.Text, jsonQuerySql, 
            new DataParameterCollection { { "user_id_param", 1 } });

        if (jsonResult.Tables.Count > 0 && jsonResult.Tables[0].Rows.Count > 0)
        {
            var row = jsonResult.Tables[0].Rows[0];
            Console.WriteLine($"JSONB 설정 - 사용자: {row["name"]}, 언어: {row["language"]}, 다크테마: {row["is_dark_theme"]}");
        }

        // 2. 배열 데이터 처리
        var arrayParams = new DataParameterCollection();
        arrayParams.Add("user_id_param", 1);
        arrayParams.Add("tags_param", "{technology,database,postgresql}");

        var arraySql = @"
            UPDATE users 
            SET tags = @tags_param::text[]
            WHERE id = @user_id_param";

        await db.ExecuteNonQueryAsync(CommandType.Text, arraySql, arrayParams);

        // 배열 데이터 조회
        var arrayQuerySql = @"
            SELECT 
                name,
                tags,
                array_length(tags, 1) as tag_count,
                'database' = ANY(tags) as has_database_tag,
                tags[1] as first_tag
            FROM users 
            WHERE id = @user_id_param AND tags IS NOT NULL";

        var arrayResult = await db.ExecuteDataSetAsync(CommandType.Text, arrayQuerySql,
            new DataParameterCollection { { "user_id_param", 1 } });

        if (arrayResult.Tables.Count > 0 && arrayResult.Tables[0].Rows.Count > 0)
        {
            var row = arrayResult.Tables[0].Rows[0];
            Console.WriteLine($"배열 데이터 - 태그 수: {row["tag_count"]}, 첫 번째 태그: {row["first_tag"]}");
        }

        // 3. CTE (Common Table Expression) 및 재귀 쿼리
        var cteSql = @"
            WITH RECURSIVE employee_hierarchy AS (
                SELECT id, name, manager_id, 1 as level, name::text as path
                FROM employees
                WHERE manager_id IS NULL
                
                UNION ALL
                
                SELECT e.id, e.name, e.manager_id, eh.level + 1, eh.path || ' -> ' || e.name
                FROM employees e
                INNER JOIN employee_hierarchy eh ON e.manager_id = eh.id
                WHERE eh.level < 10  -- 무한 루프 방지
            )
            SELECT id, name, level, path
            FROM employee_hierarchy
            ORDER BY level, name";

        try
        {
            var cteResult = await db.ExecuteDataSetAsync(CommandType.Text, cteSql);
            Console.WriteLine("\n직원 계층 구조 (재귀 CTE):");
            foreach (DataRow row in cteResult.Tables[0].Rows)
            {
                Console.WriteLine($"레벨 {row["level"]}: {row["path"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"재귀 CTE 실행 실패: {ex.Message}");
        }

        // 4. 윈도우 함수
        var windowSql = @"
            SELECT 
                name,
                email,
                created_date,
                ROW_NUMBER() OVER (ORDER BY created_date DESC) as row_num,
                RANK() OVER (ORDER BY created_date DESC) as rank_num,
                DENSE_RANK() OVER (ORDER BY created_date DESC) as dense_rank_num,
                LAG(name, 1) OVER (ORDER BY created_date) as prev_user,
                LEAD(name, 1) OVER (ORDER BY created_date) as next_user,
                FIRST_VALUE(name) OVER (ORDER BY created_date ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) as first_user,
                LAST_VALUE(name) OVER (ORDER BY created_date ROWS BETWEEN CURRENT ROW AND UNBOUNDED FOLLOWING) as last_user
            FROM users 
            WHERE status = 'Active'
            ORDER BY created_date DESC
            LIMIT 10";

        var windowResult = await db.ExecuteDataSetAsync(CommandType.Text, windowSql);
        Console.WriteLine("\n윈도우 함수 결과:");
        foreach (DataRow row in windowResult.Tables[0].Rows)
        {
            Console.WriteLine($"순위 {row["row_num"]}: {row["name"]} (이전: {row["prev_user"] ?? "없음"})");
        }

        // 5. 전체 텍스트 검색
        var searchParams = new DataParameterCollection();
        searchParams.Add("search_term_param", "PostgreSQL & database");

        var fullTextSql = @"
            SELECT id, name, description, 
                   ts_rank(to_tsvector('korean', name || ' ' || COALESCE(description, '')), 
                          plainto_tsquery('korean', @search_term_param)) as rank
            FROM products 
            WHERE to_tsvector('korean', name || ' ' || COALESCE(description, '')) 
                  @@ plainto_tsquery('korean', @search_term_param)
            ORDER BY rank DESC";

        try
        {
            var searchResult = await db.ExecuteDataSetAsync(CommandType.Text, fullTextSql, searchParams);
            Console.WriteLine($"\n전체 텍스트 검색 결과: {searchResult.Tables[0].Rows.Count}개");
            
            foreach (DataRow row in searchResult.Tables[0].Rows)
            {
                Console.WriteLine($"  상품: {row["name"]}, 관련도: {row["rank"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"전체 텍스트 검색 실행 실패: {ex.Message}");
        }

        // 6. UPSERT (INSERT ... ON CONFLICT)
        var upsertParams = new DataParameterCollection();
        upsertParams.Add("email_param", "unique@postgresql.com");
        upsertParams.Add("name_param", "유니크 포스트그레 사용자");
        upsertParams.Add("age_param", 30);

        var upsertSql = @"
            INSERT INTO users (email, name, age, status, created_date)
            VALUES (@email_param, @name_param, @age_param, 'Active', NOW())
            ON CONFLICT (email) 
            DO UPDATE SET
                name = EXCLUDED.name,
                age = EXCLUDED.age,
                updated_date = NOW()
            RETURNING id, name, (xmax = 0) as inserted";

        var upsertResult = await db.ExecuteDataSetAsync(CommandType.Text, upsertSql, upsertParams);
        
        if (upsertResult.Tables.Count > 0 && upsertResult.Tables[0].Rows.Count > 0)
        {
            var row = upsertResult.Tables[0].Rows[0];
            var action = Convert.ToBoolean(row["inserted"]) ? "삽입" : "업데이트";
            Console.WriteLine($"UPSERT 작업 완료: {action} - ID: {row["id"]}, 이름: {row["name"]}");
        }
    }

    /// <summary>
    /// PostgreSQL 성능 최적화 예제
    /// </summary>
    public async Task PerformanceExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.PostgreSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== PostgreSQL 성능 최적화 예제 ===");

        // 1. EXPLAIN ANALYZE를 사용한 실행 계획 분석
        var explainSql = @"
            EXPLAIN (ANALYZE, BUFFERS, FORMAT JSON)
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
            Console.WriteLine(explainResult.Tables[0].Rows[0]["QUERY PLAN"]);
        }

        // 2. 인덱스 사용률 통계
        var indexUsageSql = @"
            SELECT 
                schemaname,
                tablename,
                indexname,
                idx_tup_read,
                idx_tup_fetch,
                idx_scan
            FROM pg_stat_user_indexes 
            WHERE schemaname = 'public'
            AND tablename IN ('users', 'orders', 'products')
            ORDER BY idx_scan DESC";

        var indexUsage = await db.ExecuteDataSetAsync(CommandType.Text, indexUsageSql);
        
        Console.WriteLine("\n인덱스 사용 통계:");
        foreach (DataRow row in indexUsage.Tables[0].Rows)
        {
            Console.WriteLine($"인덱스: {row["indexname"]}, 스캔 횟수: {row["idx_scan"]}, 읽은 튜플: {row["idx_tup_read"]}");
        }

        // 3. 테이블 통계 정보
        var tableStatsSql = @"
            SELECT 
                schemaname,
                tablename,
                n_tup_ins as inserts,
                n_tup_upd as updates,
                n_tup_del as deletes,
                n_live_tup as live_tuples,
                n_dead_tup as dead_tuples,
                last_vacuum,
                last_autovacuum,
                last_analyze,
                last_autoanalyze
            FROM pg_stat_user_tables 
            WHERE schemaname = 'public'
            ORDER BY n_live_tup DESC";

        var tableStats = await db.ExecuteDataSetAsync(CommandType.Text, tableStatsSql);
        
        Console.WriteLine("\n테이블 통계 정보:");
        foreach (DataRow row in tableStats.Tables[0].Rows)
        {
            Console.WriteLine($"테이블: {row["tablename"]}, 활성 튜플: {row["live_tuples"]}, 사망 튜플: {row["dead_tuples"]}");
            Console.WriteLine($"  마지막 VACUUM: {row["last_vacuum"]}, 마지막 ANALYZE: {row["last_analyze"]}");
        }

        // 4. 실행 중인 쿼리 모니터링
        var activeQueriesSql = @"
            SELECT 
                pid,
                usename,
                application_name,
                client_addr,
                state,
                query_start,
                now() - query_start as duration,
                LEFT(query, 100) as query_preview
            FROM pg_stat_activity 
            WHERE state = 'active' 
            AND pid != pg_backend_pid()
            ORDER BY query_start";

        var activeQueries = await db.ExecuteDataSetAsync(CommandType.Text, activeQueriesSql);
        
        Console.WriteLine($"\n실행 중인 쿼리: {activeQueries.Tables[0].Rows.Count}개");
        foreach (DataRow row in activeQueries.Tables[0].Rows)
        {
            Console.WriteLine($"PID: {row["pid"]}, 사용자: {row["usename"]}, 지속시간: {row["duration"]}");
            Console.WriteLine($"  쿼리: {row["query_preview"]}...");
        }

        // 5. 테이블 크기 및 디스크 사용량
        var tableSizesSql = @"
            SELECT 
                tablename,
                pg_size_pretty(pg_total_relation_size(schemaname||'.'||tablename)) as total_size,
                pg_size_pretty(pg_relation_size(schemaname||'.'||tablename)) as table_size,
                pg_size_pretty(pg_indexes_size(schemaname||'.'||tablename)) as indexes_size
            FROM pg_tables 
            WHERE schemaname = 'public'
            ORDER BY pg_total_relation_size(schemaname||'.'||tablename) DESC";

        var tableSizes = await db.ExecuteDataSetAsync(CommandType.Text, tableSizesSql);
        
        Console.WriteLine("\n테이블 크기 정보:");
        foreach (DataRow row in tableSizes.Tables[0].Rows)
        {
            Console.WriteLine($"테이블: {row["tablename"]}");
            Console.WriteLine($"  전체: {row["total_size"]}, 테이블: {row["table_size"]}, 인덱스: {row["indexes_size"]}");
        }
    }
}
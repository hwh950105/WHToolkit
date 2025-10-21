using System.Data;
using HWH.Database;

namespace HWH.Framework.Examples;

/// <summary>
/// Oracle Database 전용 사용 예제들
/// </summary>
public class OracleExamples
{
    private const string ConnectionString = "Data Source=localhost:1521/XE;User Id=hr;Password=password;";

    /// <summary>
    /// Oracle 기본 CRUD 작업 예제
    /// </summary>
    public async Task BasicCrudExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.Oracle, ConnectionString);
        if (db == null) return;

        Console.WriteLine("=== Oracle 기본 CRUD 예제 ===");

        // CREATE - 시퀀스를 사용한 사용자 추가
        var insertParams = new DataParameterCollection();
        insertParams.Add(":name", "김오라클");
        insertParams.Add(":email", "kim@oracle.com");
        insertParams.Add(":age", 35);

        var insertSql = @"
            INSERT INTO USERS (ID, NAME, EMAIL, AGE, STATUS, CREATED_DATE) 
            VALUES (USER_SEQ.NEXTVAL, :name, :email, :age, 'Active', SYSDATE)
            RETURNING ID INTO :new_id";

        // Oracle에서는 RETURNING INTO 사용
        insertParams.Add(ParameterDirection.Output, DbType.Int32, ":new_id", null);
        
        await db.ExecuteNonQueryAsync(CommandType.Text, insertSql, insertParams);
        var newUserId = insertParams[":new_id"].Value;
        Console.WriteLine($"새 사용자 생성 완료. ID: {newUserId}");

        // READ - 사용자 조회
        var selectParams = new DataParameterCollection();
        selectParams.Add(":user_id", newUserId);

        var selectSql = "SELECT ID, NAME, EMAIL, AGE, STATUS, CREATED_DATE FROM USERS WHERE ID = :user_id";
        var userData = await db.ExecuteDataSetAsync(CommandType.Text, selectSql, selectParams);

        if (userData.Tables.Count > 0 && userData.Tables[0].Rows.Count > 0)
        {
            var row = userData.Tables[0].Rows[0];
            Console.WriteLine($"조회된 사용자: {row["NAME"]} ({row["EMAIL"]})");
        }

        // UPDATE - 사용자 정보 수정
        var updateParams = new DataParameterCollection();
        updateParams.Add(":user_id", newUserId);
        updateParams.Add(":new_email", "kim.updated@oracle.com");

        var updateSql = "UPDATE USERS SET EMAIL = :new_email, UPDATED_DATE = SYSDATE WHERE ID = :user_id";
        var updatedRows = await db.ExecuteNonQueryAsync(CommandType.Text, updateSql, updateParams);
        Console.WriteLine($"업데이트된 행 수: {updatedRows}");

        // DELETE - 사용자 삭제 (실제로는 비활성화)
        var deleteParams = new DataParameterCollection();
        deleteParams.Add(":user_id", newUserId);

        var deleteSql = "UPDATE USERS SET STATUS = 'Inactive', UPDATED_DATE = SYSDATE WHERE ID = :user_id";
        var deletedRows = await db.ExecuteNonQueryAsync(CommandType.Text, deleteSql, deleteParams);
        Console.WriteLine($"비활성화된 행 수: {deletedRows}");
    }

    /// <summary>
    /// Oracle 트랜잭션 예제
    /// </summary>
    public async Task TransactionExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.Oracle, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== Oracle 트랜잭션 예제 ===");

        try
        {
            await db.TransactionBeginAsync();

            // 1. 새 주문 생성 (시퀀스 사용)
            var orderParams = new DataParameterCollection();
            orderParams.Add(":user_id", 1);
            orderParams.Add(":total_amount", 250.00m);

            var orderSql = @"
                INSERT INTO ORDERS (ID, USER_ID, ORDER_DATE, TOTAL_AMOUNT, STATUS) 
                VALUES (ORDER_SEQ.NEXTVAL, :user_id, SYSDATE, :total_amount, 'Processing')
                RETURNING ID INTO :order_id";

            orderParams.Add(ParameterDirection.Output, DbType.Int32, ":order_id", null);
            
            await db.ExecuteNonQueryAsync(CommandType.Text, orderSql, orderParams);
            var orderId = orderParams[":order_id"].Value;
            Console.WriteLine($"주문 생성 완료. 주문 ID: {orderId}");

            // 2. 주문 항목들 추가
            var orderItems = new[]
            {
                new { ProductId = 1, Quantity = 3, UnitPrice = 50.00m },
                new { ProductId = 2, Quantity = 2, UnitPrice = 50.00m }
            };

            foreach (var item in orderItems)
            {
                var itemParams = new DataParameterCollection();
                itemParams.Add(":order_id", orderId);
                itemParams.Add(":product_id", item.ProductId);
                itemParams.Add(":quantity", item.Quantity);
                itemParams.Add(":unit_price", item.UnitPrice);

                var itemSql = @"
                    INSERT INTO ORDER_ITEMS (ID, ORDER_ID, PRODUCT_ID, QUANTITY, UNIT_PRICE) 
                    VALUES (ORDER_ITEM_SEQ.NEXTVAL, :order_id, :product_id, :quantity, :unit_price)";

                await db.ExecuteNonQueryAsync(CommandType.Text, itemSql, itemParams);
            }

            // 3. 재고 업데이트
            foreach (var item in orderItems)
            {
                var stockParams = new DataParameterCollection();
                stockParams.Add(":product_id", item.ProductId);
                stockParams.Add(":quantity", item.Quantity);

                var stockSql = @"
                    UPDATE PRODUCTS 
                    SET STOCK = STOCK - :quantity, UPDATED_DATE = SYSDATE 
                    WHERE ID = :product_id AND STOCK >= :quantity";

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
    /// Oracle 저장 프로시저 및 함수 예제
    /// </summary>
    public async Task StoredProcedureExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.Oracle, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== Oracle 저장 프로시저/함수 예제 ===");

        // 저장 프로시저 호출
        var procParams = new DataParameterCollection();
        procParams.Add(ParameterDirection.Input, ":p_start_date", DateTime.Now.AddDays(-30));
        procParams.Add(ParameterDirection.Input, ":p_end_date", DateTime.Now);
        procParams.Add(ParameterDirection.Output, DbType.Int32, ":p_total_orders", null);
        procParams.Add(ParameterDirection.Output, DbType.Decimal, ":p_total_revenue", null);

        // REF CURSOR를 반환하는 프로시저
        procParams.Add(ParameterDirection.Output, DbType.Object, ":p_cursor", null);

        var procSql = "BEGIN GET_SALES_REPORT(:p_start_date, :p_end_date, :p_total_orders, :p_total_revenue, :p_cursor); END;";
        
        try
        {
            var result = await db.ExecuteDataSetAsync(CommandType.Text, procSql, procParams);

            Console.WriteLine("월간 판매 보고서:");
            if (result.Tables.Count > 0)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    Console.WriteLine($"날짜: {row["ORDER_DATE"]}, 주문수: {row["ORDER_COUNT"]}, 매출: {row["REVENUE"]:F2}");
                }
            }

            // 출력 매개변수 값 확인
            Console.WriteLine($"총 주문수: {procParams[":p_total_orders"].Value}");
            Console.WriteLine($"총 매출: {procParams[":p_total_revenue"].Value:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"저장 프로시저 실행 실패: {ex.Message}");
        }

        // Oracle 함수 호출
        var funcParams = new DataParameterCollection();
        funcParams.Add(":user_id", 1);
        funcParams.Add(ParameterDirection.ReturnValue, DbType.Decimal, ":return_value", null);

        var funcSql = "BEGIN :return_value := GET_USER_TOTAL_SPENT(:user_id); END;";

        try
        {
            await db.ExecuteNonQueryAsync(CommandType.Text, funcSql, funcParams);
            var totalSpent = funcParams[":return_value"].Value;
            Console.WriteLine($"사용자 총 구매 금액: {totalSpent:F2}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"함수 실행 실패: {ex.Message}");
        }
    }

    /// <summary>
    /// Oracle 대용량 데이터 처리 예제
    /// </summary>
    public async Task BulkDataExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.Oracle, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== Oracle 대용량 데이터 처리 예제 ===");

        // 대용량 조회 - Oracle의 ROWNUM과 분석 함수 사용
        var pageSize = 100;
        var currentPage = 1;

        // 전체 레코드 수 조회
        var countSql = "SELECT COUNT(*) FROM USERS WHERE STATUS = 'Active'";
        var countResult = await db.ExecuteScalarAsync(CommandType.Text, countSql);
        var totalRecords = Convert.ToInt32(countResult);

        Console.WriteLine($"전체 활성 사용자 수: {totalRecords:N0}");

        // Oracle 12c+ 방식 (OFFSET/FETCH)
        for (int page = 1; page <= Math.Min(3, (int)Math.Ceiling((double)totalRecords / pageSize)); page++)
        {
            var offset = (page - 1) * pageSize;
            
            var pageParams = new DataParameterCollection();
            pageParams.Add(":offset_val", offset);
            pageParams.Add(":page_size", pageSize);

            var pageSql = @"
                SELECT ID, NAME, EMAIL, CREATED_DATE 
                FROM USERS 
                WHERE STATUS = 'Active'
                ORDER BY CREATED_DATE DESC
                OFFSET :offset_val ROWS 
                FETCH NEXT :page_size ROWS ONLY";

            var pageData = await db.ExecuteDataSetAsync(CommandType.Text, pageSql, pageParams);

            Console.WriteLine($"페이지 {page} ({pageData.Tables[0].Rows.Count}개 행):");
            foreach (DataRow row in pageData.Tables[0].Rows)
            {
                Console.WriteLine($"  {row["NAME"]} ({row["EMAIL"]}) - {row["CREATED_DATE"]:yyyy-MM-dd}");
            }
        }

        // Oracle 11g 방식 (ROWNUM 사용)
        var rowNumParams = new DataParameterCollection();
        rowNumParams.Add(":start_row", 1);
        rowNumParams.Add(":end_row", pageSize);

        var rowNumSql = @"
            SELECT * FROM (
                SELECT a.*, ROWNUM rnum FROM (
                    SELECT ID, NAME, EMAIL, CREATED_DATE 
                    FROM USERS 
                    WHERE STATUS = 'Active'
                    ORDER BY CREATED_DATE DESC
                ) a 
                WHERE ROWNUM <= :end_row
            ) 
            WHERE rnum >= :start_row";

        var rowNumData = await db.ExecuteDataSetAsync(CommandType.Text, rowNumSql, rowNumParams);
        Console.WriteLine($"ROWNUM 방식 결과: {rowNumData.Tables[0].Rows.Count}개 행");
    }

    /// <summary>
    /// Oracle 특화 기능 예제
    /// </summary>
    public async Task OracleSpecificFeaturesExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.Oracle, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== Oracle 특화 기능 예제 ===");

        // 1. 계층적 쿼리 (Hierarchical Queries)
        var hierarchySql = @"
            SELECT LEVEL, LPAD(' ', 2 * (LEVEL - 1)) || NAME AS INDENTED_NAME, ID, PARENT_ID
            FROM CATEGORIES
            START WITH PARENT_ID IS NULL
            CONNECT BY PRIOR ID = PARENT_ID
            ORDER SIBLINGS BY NAME";

        try
        {
            var hierarchyResult = await db.ExecuteDataSetAsync(CommandType.Text, hierarchySql);
            Console.WriteLine("카테고리 계층 구조:");
            foreach (DataRow row in hierarchyResult.Tables[0].Rows)
            {
                Console.WriteLine($"레벨 {row["LEVEL"]}: {row["INDENTED_NAME"]}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"계층적 쿼리 실행 실패: {ex.Message}");
        }

        // 2. 분석 함수 (Analytic Functions)
        var analyticSql = @"
            SELECT 
                NAME,
                EMAIL,
                CREATED_DATE,
                ROW_NUMBER() OVER (ORDER BY CREATED_DATE DESC) as ROW_NUM,
                RANK() OVER (ORDER BY CREATED_DATE DESC) as RANK_NUM,
                DENSE_RANK() OVER (ORDER BY CREATED_DATE DESC) as DENSE_RANK_NUM,
                LAG(NAME, 1) OVER (ORDER BY CREATED_DATE) as PREV_USER,
                LEAD(NAME, 1) OVER (ORDER BY CREATED_DATE) as NEXT_USER
            FROM USERS 
            WHERE STATUS = 'Active'
            AND ROWNUM <= 10";

        var analyticResult = await db.ExecuteDataSetAsync(CommandType.Text, analyticSql);
        Console.WriteLine("\n분석 함수 결과:");
        foreach (DataRow row in analyticResult.Tables[0].Rows)
        {
            Console.WriteLine($"순위 {row["ROW_NUM"]}: {row["NAME"]} (이전: {row["PREV_USER"] ?? "없음"})");
        }

        // 3. MERGE 문 사용
        var mergeParams = new DataParameterCollection();
        mergeParams.Add(":product_id", 1);
        mergeParams.Add(":product_name", "업데이트된 상품");
        mergeParams.Add(":price", 149.99m);

        var mergeSql = @"
            MERGE INTO PRODUCTS target
            USING (SELECT :product_id as ID, :product_name as NAME, :price as PRICE FROM DUAL) source
            ON (target.ID = source.ID)
            WHEN MATCHED THEN
                UPDATE SET NAME = source.NAME, PRICE = source.PRICE, UPDATED_DATE = SYSDATE
            WHEN NOT MATCHED THEN
                INSERT (ID, NAME, PRICE, CREATED_DATE) 
                VALUES (PRODUCT_SEQ.NEXTVAL, source.NAME, source.PRICE, SYSDATE)";

        var mergeRows = await db.ExecuteNonQueryAsync(CommandType.Text, mergeSql, mergeParams);
        Console.WriteLine($"MERGE 작업 완료: {mergeRows}개 행 영향받음");

        // 4. XMLTYPE 데이터 처리
        var xmlParams = new DataParameterCollection();
        xmlParams.Add(":user_id", 1);
        xmlParams.Add(":xml_data", "<preferences><language>ko</language><theme>dark</theme></preferences>");

        var xmlSql = @"
            UPDATE USERS 
            SET PREFERENCES = XMLTYPE(:xml_data)
            WHERE ID = :user_id";

        await db.ExecuteNonQueryAsync(CommandType.Text, xmlSql, xmlParams);

        // XML 데이터 조회
        var xmlQuerySql = @"
            SELECT 
                NAME,
                EXTRACTVALUE(PREFERENCES, '/preferences/language') as LANGUAGE,
                EXTRACTVALUE(PREFERENCES, '/preferences/theme') as THEME
            FROM USERS 
            WHERE ID = :user_id AND PREFERENCES IS NOT NULL";

        var xmlResult = await db.ExecuteDataSetAsync(CommandType.Text, xmlQuerySql, 
            new DataParameterCollection { { ":user_id", 1 } });

        if (xmlResult.Tables.Count > 0 && xmlResult.Tables[0].Rows.Count > 0)
        {
            var row = xmlResult.Tables[0].Rows[0];
            Console.WriteLine($"XML 설정 - 사용자: {row["NAME"]}, 언어: {row["LANGUAGE"]}, 테마: {row["THEME"]}");
        }
    }

    /// <summary>
    /// Oracle 성능 최적화 예제
    /// </summary>
    public async Task PerformanceExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.Oracle, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== Oracle 성능 최적화 예제 ===");

        // 1. 실행 계획 확인
        var explainSql = @"
            EXPLAIN PLAN FOR
            SELECT u.NAME, COUNT(o.ID) as ORDER_COUNT, SUM(o.TOTAL_AMOUNT) as TOTAL_SPENT
            FROM USERS u
            LEFT JOIN ORDERS o ON u.ID = o.USER_ID
            WHERE u.STATUS = 'Active'
            GROUP BY u.ID, u.NAME
            HAVING COUNT(o.ID) > 0
            ORDER BY TOTAL_SPENT DESC";

        await db.ExecuteNonQueryAsync(CommandType.Text, explainSql);

        var planSql = @"
            SELECT PLAN_TABLE_OUTPUT 
            FROM TABLE(DBMS_XPLAN.DISPLAY('PLAN_TABLE', NULL, 'BASIC'))";

        var planResult = await db.ExecuteDataSetAsync(CommandType.Text, planSql);
        Console.WriteLine("실행 계획:");
        foreach (DataRow row in planResult.Tables[0].Rows)
        {
            Console.WriteLine(row["PLAN_TABLE_OUTPUT"]);
        }

        // 2. 힌트 사용 예제
        var hintSql = @"
            SELECT /*+ INDEX(u, USER_STATUS_IDX) USE_NL(u o) */
                u.NAME, o.ORDER_DATE, o.TOTAL_AMOUNT
            FROM USERS u
            INNER JOIN ORDERS o ON u.ID = o.USER_ID
            WHERE u.STATUS = 'Active'
            AND o.ORDER_DATE >= SYSDATE - 30";

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var hintResult = await db.ExecuteDataSetAsync(CommandType.Text, hintSql);
        stopwatch.Stop();

        Console.WriteLine($"힌트 사용 쿼리 실행 시간: {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine($"결과 행 수: {hintResult.Tables[0].Rows.Count}");

        // 3. 통계 정보 수집
        var statsSql = @"
            BEGIN
                DBMS_STATS.GATHER_TABLE_STATS(
                    OWNNAME => USER,
                    TABNAME => 'USERS',
                    ESTIMATE_PERCENT => DBMS_STATS.AUTO_SAMPLE_SIZE,
                    CASCADE => TRUE
                );
            END;";

        try
        {
            await db.ExecuteNonQueryAsync(CommandType.Text, statsSql);
            Console.WriteLine("USERS 테이블 통계 정보 수집 완료");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"통계 정보 수집 실패: {ex.Message}");
        }
    }
}
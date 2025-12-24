using System.Data;
using HWH.Database;

namespace HWH.Framework.Examples;

/// <summary>
/// SQL Server 전용 사용 예제들
/// </summary>
public class SqlServerExamples
{
    private const string ConnectionString = "Server=localhost;Database=TestDB;Integrated Security=true;TrustServerCertificate=true";

    /// <summary>
    /// 기본 CRUD 작업 예제
    /// </summary>
    public async Task BasicCrudExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("=== SQL Server 기본 CRUD 예제 ===");

        // CREATE - 사용자 추가
        var insertParams = new DataParameterCollection();
        insertParams.Add("@Name", "홍길동");
        insertParams.Add("@Email", "hong@example.com");
        insertParams.Add("@Age", 30);

        var insertSql = @"
            INSERT INTO Users (Name, Email, Age, Status, CreatedDate) 
            OUTPUT INSERTED.Id
            VALUES (@Name, @Email, @Age, 'Active', GETDATE())";

        var newUserId = await db.ExecuteScalarAsync(CommandType.Text, insertSql, insertParams);
        Console.WriteLine($"새 사용자 생성 완료. ID: {newUserId}");

        // READ - 사용자 조회
        var selectParams = new DataParameterCollection();
        selectParams.Add("@UserId", newUserId);

        var selectSql = "SELECT Id, Name, Email, Age, Status, CreatedDate FROM Users WHERE Id = @UserId";
        var userData = await db.ExecuteDataSetAsync(CommandType.Text, selectSql, selectParams);

        if (userData.Tables.Count > 0 && userData.Tables[0].Rows.Count > 0)
        {
            var row = userData.Tables[0].Rows[0];
            Console.WriteLine($"조회된 사용자: {row["Name"]} ({row["Email"]})");
        }

        // UPDATE - 사용자 정보 수정
        var updateParams = new DataParameterCollection();
        updateParams.Add("@UserId", newUserId);
        updateParams.Add("@NewEmail", "hong.updated@example.com");

        var updateSql = "UPDATE Users SET Email = @NewEmail, UpdatedDate = GETDATE() WHERE Id = @UserId";
        var updatedRows = await db.ExecuteNonQueryAsync(CommandType.Text, updateSql, updateParams);
        Console.WriteLine($"업데이트된 행 수: {updatedRows}");

        // DELETE - 사용자 삭제 (실제로는 비활성화)
        var deleteParams = new DataParameterCollection();
        deleteParams.Add("@UserId", newUserId);

        var deleteSql = "UPDATE Users SET Status = 'Inactive', UpdatedDate = GETDATE() WHERE Id = @UserId";
        var deletedRows = await db.ExecuteNonQueryAsync(CommandType.Text, deleteSql, deleteParams);
        Console.WriteLine($"비활성화된 행 수: {deletedRows}");
    }

    /// <summary>
    /// 트랜잭션을 사용한 복합 작업 예제
    /// </summary>
    public async Task TransactionExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== SQL Server 트랜잭션 예제 ===");

        try
        {
            await db.TransactionBeginAsync();

            // 1. 새 주문 생성
            var orderParams = new DataParameterCollection();
            orderParams.Add("@UserId", 1);
            orderParams.Add("@TotalAmount", 150.00m);

            var orderSql = @"
                INSERT INTO Orders (UserId, OrderDate, TotalAmount, Status) 
                OUTPUT INSERTED.Id
                VALUES (@UserId, GETDATE(), @TotalAmount, 'Processing')";

            var orderId = await db.ExecuteScalarAsync(CommandType.Text, orderSql, orderParams);
            Console.WriteLine($"주문 생성 완료. 주문 ID: {orderId}");

            // 2. 주문 항목들 추가
            var orderItems = new[]
            {
                new { ProductId = 1, Quantity = 2, UnitPrice = 50.00m },
                new { ProductId = 2, Quantity = 1, UnitPrice = 50.00m }
            };

            foreach (var item in orderItems)
            {
                var itemParams = new DataParameterCollection();
                itemParams.Add("@OrderId", orderId);
                itemParams.Add("@ProductId", item.ProductId);
                itemParams.Add("@Quantity", item.Quantity);
                itemParams.Add("@UnitPrice", item.UnitPrice);

                var itemSql = @"
                    INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) 
                    VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice)";

                await db.ExecuteNonQueryAsync(CommandType.Text, itemSql, itemParams);
            }

            // 3. 재고 업데이트
            foreach (var item in orderItems)
            {
                var stockParams = new DataParameterCollection();
                stockParams.Add("@ProductId", item.ProductId);
                stockParams.Add("@Quantity", item.Quantity);

                var stockSql = @"
                    UPDATE Products 
                    SET Stock = Stock - @Quantity, UpdatedDate = GETDATE() 
                    WHERE Id = @ProductId AND Stock >= @Quantity";

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
    /// 저장 프로시저 사용 예제
    /// </summary>
    public async Task StoredProcedureExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== SQL Server 저장 프로시저 예제 ===");

        // 입력 및 출력 매개변수를 사용한 저장 프로시저 호출
        var parameters = new DataParameterCollection();
        parameters.Add(ParameterDirection.Input, "@StartDate", DateTime.Now.AddDays(-30));
        parameters.Add(ParameterDirection.Input, "@EndDate", DateTime.Now);
        parameters.Add(ParameterDirection.Output, DbType.Int32, "@TotalOrders", null);
        parameters.Add(ParameterDirection.Output, DbType.Decimal, "@TotalRevenue", null);

        var result = await db.ExecuteDataSetAsync(CommandType.StoredProcedure, "GetSalesReport", parameters);

        Console.WriteLine("월간 판매 보고서:");
        if (result.Tables.Count > 0)
        {
            foreach (DataRow row in result.Tables[0].Rows)
            {
                Console.WriteLine($"날짜: {row["Date"]}, 주문수: {row["OrderCount"]}, 매출: ${row["Revenue"]:F2}");
            }
        }

        // 출력 매개변수 값 확인 (프레임워크에서 구현 필요)
        // Console.WriteLine($"총 주문수: {parameters["@TotalOrders"].Value}");
        // Console.WriteLine($"총 매출: ${parameters["@TotalRevenue"].Value:F2}");
    }

    /// <summary>
    /// 대용량 데이터 처리 예제
    /// </summary>
    public async Task BulkDataExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== SQL Server 대용량 데이터 처리 예제 ===");

        // 대용량 조회 - 페이징 처리
        var pageSize = 100;
        var currentPage = 1;
        var totalRecords = 0;

        // 전체 레코드 수 조회
        var countSql = "SELECT COUNT(*) FROM Users WHERE Status = 'Active'";
        var countResult = await db.ExecuteScalarAsync(CommandType.Text, countSql);
        totalRecords = Convert.ToInt32(countResult);

        Console.WriteLine($"전체 활성 사용자 수: {totalRecords:N0}");

        // 페이지별로 데이터 조회
        var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

        for (int page = 1; page <= Math.Min(3, totalPages); page++) // 첫 3페이지만 예시로 출력
        {
            var offset = (page - 1) * pageSize;
            
            var pageParams = new DataParameterCollection();
            pageParams.Add("@Offset", offset);
            pageParams.Add("@PageSize", pageSize);

            var pageSql = @"
                SELECT Id, Name, Email, CreatedDate 
                FROM Users 
                WHERE Status = 'Active'
                ORDER BY CreatedDate DESC
                OFFSET @Offset ROWS 
                FETCH NEXT @PageSize ROWS ONLY";

            var pageData = await db.ExecuteDataSetAsync(CommandType.Text, pageSql, pageParams);

            Console.WriteLine($"페이지 {page}/{totalPages} ({pageData.Tables[0].Rows.Count}개 행):");
            foreach (DataRow row in pageData.Tables[0].Rows)
            {
                Console.WriteLine($"  {row["Name"]} ({row["Email"]}) - {row["CreatedDate"]:yyyy-MM-dd}");
            }
        }
    }

    /// <summary>
    /// SQL Server 특화 기능 예제
    /// </summary>
    public async Task SqlServerSpecificFeaturesExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== SQL Server 특화 기능 예제 ===");

        // 1. JSON 데이터 처리
        var jsonParams = new DataParameterCollection();
        jsonParams.Add("@UserPreferences", @"{""language"":""ko"",""theme"":""dark"",""notifications"":true}");
        jsonParams.Add("@UserId", 1);

        var jsonSql = @"
            UPDATE Users 
            SET Preferences = @UserPreferences 
            WHERE Id = @UserId";

        await db.ExecuteNonQueryAsync(CommandType.Text, jsonSql, jsonParams);

        // JSON 데이터 조회
        var jsonQuerySql = @"
            SELECT 
                Name,
                JSON_VALUE(Preferences, '$.language') as Language,
                JSON_VALUE(Preferences, '$.theme') as Theme,
                JSON_VALUE(Preferences, '$.notifications') as Notifications
            FROM Users 
            WHERE Id = @UserId";

        var jsonResult = await db.ExecuteDataSetAsync(CommandType.Text, jsonQuerySql, 
            new DataParameterCollection { { "@UserId", 1 } });

        if (jsonResult.Tables.Count > 0 && jsonResult.Tables[0].Rows.Count > 0)
        {
            var row = jsonResult.Tables[0].Rows[0];
            Console.WriteLine($"사용자 설정 - 언어: {row["Language"]}, 테마: {row["Theme"]}, 알림: {row["Notifications"]}");
        }

        // 2. MERGE 문 사용
        var mergeParams = new DataParameterCollection();
        mergeParams.Add("@ProductId", 1);
        mergeParams.Add("@ProductName", "업데이트된 상품");
        mergeParams.Add("@Price", 99.99m);

        var mergeSql = @"
            MERGE Products AS target
            USING (SELECT @ProductId as Id, @ProductName as Name, @Price as Price) AS source
            ON target.Id = source.Id
            WHEN MATCHED THEN
                UPDATE SET Name = source.Name, Price = source.Price, UpdatedDate = GETDATE()
            WHEN NOT MATCHED THEN
                INSERT (Name, Price, CreatedDate) VALUES (source.Name, source.Price, GETDATE())
            OUTPUT $action, INSERTED.Id, INSERTED.Name;";

        var mergeResult = await db.ExecuteDataSetAsync(CommandType.Text, mergeSql, mergeParams);
        
        if (mergeResult.Tables.Count > 0 && mergeResult.Tables[0].Rows.Count > 0)
        {
            var row = mergeResult.Tables[0].Rows[0];
            Console.WriteLine($"MERGE 작업: {row["$action"]} - 상품 ID: {row["Id"]}, 이름: {row["Name"]}");
        }

        // 3. 전체 텍스트 검색 (Full-Text Search)
        var searchParams = new DataParameterCollection();
        searchParams.Add("@SearchTerm", "검색어");

        var fullTextSql = @"
            SELECT Id, Name, Description, RANK() OVER (ORDER BY KEY_TBL.RANK DESC) as SearchRank
            FROM Products 
            INNER JOIN CONTAINSTABLE(Products, (Name, Description), @SearchTerm) AS KEY_TBL
            ON Products.Id = KEY_TBL.[KEY]
            ORDER BY SearchRank";

        // 전체 텍스트 인덱스가 설정되어 있다고 가정
        try
        {
            var searchResult = await db.ExecuteDataSetAsync(CommandType.Text, fullTextSql, searchParams);
            Console.WriteLine($"전체 텍스트 검색 결과: {searchResult.Tables[0].Rows.Count}개");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"전체 텍스트 검색 실행 실패 (인덱스 미설정?): {ex.Message}");
        }
    }

    /// <summary>
    /// 성능 모니터링 및 최적화 예제
    /// </summary>
    public async Task PerformanceExample()
    {
        using var db = await DBHelper.CreateConnectionAsync(ProviderKind.MSSQL, ConnectionString);
        if (db == null) return;

        Console.WriteLine("\n=== SQL Server 성능 모니터링 예제 ===");

        // 실행 계획 정보 조회
        var planSql = @"
            SET STATISTICS IO ON;
            SET STATISTICS TIME ON;
            
            SELECT u.Name, COUNT(o.Id) as OrderCount, SUM(o.TotalAmount) as TotalSpent
            FROM Users u
            LEFT JOIN Orders o ON u.Id = o.UserId
            WHERE u.Status = 'Active'
            GROUP BY u.Id, u.Name
            HAVING COUNT(o.Id) > 0
            ORDER BY TotalSpent DESC;";

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var performanceResult = await db.ExecuteDataSetAsync(CommandType.Text, planSql);
        stopwatch.Stop();

        Console.WriteLine($"쿼리 실행 시간: {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine($"결과 행 수: {performanceResult.Tables[0].Rows.Count}");

        // 인덱스 사용률 조회
        var indexUsageSql = @"
            SELECT 
                OBJECT_NAME(s.object_id) AS TableName,
                i.name AS IndexName,
                s.user_seeks,
                s.user_scans,
                s.user_lookups,
                s.user_updates
            FROM sys.dm_db_index_usage_stats s
            INNER JOIN sys.indexes i ON s.object_id = i.object_id AND s.index_id = i.index_id
            WHERE OBJECTPROPERTY(s.object_id, 'IsUserTable') = 1
            AND OBJECT_NAME(s.object_id) IN ('Users', 'Orders', 'Products')
            ORDER BY s.user_seeks + s.user_scans + s.user_lookups DESC";

        var indexUsage = await db.ExecuteDataSetAsync(CommandType.Text, indexUsageSql);
        
        Console.WriteLine("\n인덱스 사용 통계:");
        foreach (DataRow row in indexUsage.Tables[0].Rows)
        {
            Console.WriteLine($"테이블: {row["TableName"]}, 인덱스: {row["IndexName"]}");
            Console.WriteLine($"  Seeks: {row["user_seeks"]}, Scans: {row["user_scans"]}, Lookups: {row["user_lookups"]}, Updates: {row["user_updates"]}");
        }
    }
}
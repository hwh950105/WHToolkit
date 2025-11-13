-- MS SQL Server 초기화 스크립트
USE master;
GO

-- testdb 데이터베이스 생성
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'testdb')
BEGIN
    CREATE DATABASE testdb;
END
GO

USE testdb;
GO

-- users 테이블 생성
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'users')
BEGIN
    CREATE TABLE users (
        id INT IDENTITY(1,1) PRIMARY KEY,
        name NVARCHAR(100) NOT NULL,
        email NVARCHAR(200) UNIQUE NOT NULL,
        age INT,
        is_active BIT DEFAULT 1,
        created_date DATETIME DEFAULT GETDATE()
    );
END
GO

-- 더미 데이터 삽입
INSERT INTO users (name, email, age, is_active) VALUES
(N'김철수', 'kim@example.com', 30, 1),
(N'이영희', 'lee@example.com', 25, 1),
(N'박민수', 'park@example.com', 35, 0),
(N'정수진', 'jung@example.com', 28, 1),
(N'최동욱', 'choi@example.com', 32, 1);
GO

-- 확인
SELECT 'MS SQL 테이블 생성 완료!' as message;
SELECT COUNT(*) as user_count FROM users;
GO


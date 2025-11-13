-- MySQL 초기화 스크립트
USE testdb;

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(200) UNIQUE NOT NULL,
    age INT,
    is_active TINYINT(1) DEFAULT 1,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 더미 데이터 삽입
INSERT INTO users (name, email, age, is_active) VALUES
('김철수', 'kim@example.com', 30, 1),
('이영희', 'lee@example.com', 25, 1),
('박민수', 'park@example.com', 35, 0),
('정수진', 'jung@example.com', 28, 1),
('최동욱', 'choi@example.com', 32, 1);

-- 확인
SELECT 'MySQL 테이블 생성 완료!' as message;
SELECT COUNT(*) as user_count FROM users;


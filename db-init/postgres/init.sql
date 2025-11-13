-- PostgreSQL 초기화 스크립트
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(200) UNIQUE NOT NULL,
    age INT,
    is_active BOOLEAN DEFAULT true,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 더미 데이터 삽입
INSERT INTO users (name, email, age, is_active) VALUES
('김철수', 'kim@example.com', 30, true),
('이영희', 'lee@example.com', 25, true),
('박민수', 'park@example.com', 35, false),
('정수진', 'jung@example.com', 28, true),
('최동욱', 'choi@example.com', 32, true);

-- 확인
SELECT 'PostgreSQL 테이블 생성 완료!' as message;
SELECT COUNT(*) as user_count FROM users;


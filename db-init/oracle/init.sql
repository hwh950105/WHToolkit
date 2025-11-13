-- Oracle 초기화 스크립트
-- testuser로 실행됨

-- users 테이블 생성
CREATE TABLE users (
    id NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    name VARCHAR2(100) NOT NULL,
    email VARCHAR2(200) UNIQUE NOT NULL,
    age NUMBER,
    is_active NUMBER(1) DEFAULT 1,
    created_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 더미 데이터 삽입
INSERT INTO users (name, email, age, is_active) VALUES ('김철수', 'kim@example.com', 30, 1);
INSERT INTO users (name, email, age, is_active) VALUES ('이영희', 'lee@example.com', 25, 1);
INSERT INTO users (name, email, age, is_active) VALUES ('박민수', 'park@example.com', 35, 0);
INSERT INTO users (name, email, age, is_active) VALUES ('정수진', 'jung@example.com', 28, 1);
INSERT INTO users (name, email, age, is_active) VALUES ('최동욱', 'choi@example.com', 32, 1);
COMMIT;

-- 확인
SELECT 'Oracle 테이블 생성 완료!' as message FROM DUAL;
SELECT COUNT(*) as user_count FROM users;


# WHToolkit Database Helper 통합 테스트

## 📋 생성된 파일 목록

```
d:\git\WHToolkit2\
├── docker-compose.yml              # Docker 설정 (볼륨 마운트 추가됨)
├── db-init\                        # DB 초기화 스크립트
│   ├── postgres\init.sql          # PostgreSQL 테이블 & 더미 데이터
│   ├── mysql\init.sql             # MySQL 테이블 & 더미 데이터
│   ├── mariadb\init.sql           # MariaDB 테이블 & 더미 데이터
│   ├── mssql\init.sql             # MS SQL 테이블 & 더미 데이터
│   └── oracle\init.sql            # Oracle 테이블 & 더미 데이터
├── Models\User.cs                  # 공통 사용자 모델
└── DatabaseIntegrationTest.cs      # 통합 테스트 코드
```

## 🚀 빠른 시작 (3단계)

### 1단계: Docker로 모든 DB 실행

```powershell
# 프로젝트 루트로 이동
cd d:\git\WHToolkit2

# 모든 DB 시작 (백그라운드)
docker-compose up -d

# 상태 확인
docker-compose ps

# 초기화 완료될 때까지 로그 확인 (Ctrl+C로 중단)
docker-compose logs -f
```

**대기 시간:**
- PostgreSQL: ~10초 ✅
- MySQL: ~20초 ✅
- MariaDB: ~20초 ✅
- MS SQL: ~30초 ✅
- Oracle: ~60초 ✅ (가장 느림)

### 2단계: 콘솔 앱 프로젝트 생성 (선택사항)

```powershell
# 새 콘솔 프로젝트 생성
dotnet new console -n WHToolkit.DatabaseTest
cd WHToolkit.DatabaseTest

# WHToolkit 참조 추가
dotnet add reference ../WHToolkit/WHToolkit.csproj

# 파일 복사
copy ..\DatabaseIntegrationTest.cs Program.cs
mkdir Models
copy ..\Models\User.cs Models\
```

### 3단계: 테스트 실행

```powershell
dotnet run
```

또는 Visual Studio에서 `DatabaseIntegrationTest.cs`를 직접 실행

## 📊 테스트 항목

각 DB에 대해 다음 5가지를 테스트합니다:

1. **ExecuteList<T>** - 타입 리스트 조회
   - 5명의 사용자 데이터 조회
   - 객체 자동 매핑 테스트

2. **ExecuteDataTable** - DataTable 조회
   - 총 사용자 수와 평균 나이 계산

3. **ExecuteDataSet** - DataSet 조회
   - 활성 사용자 필터링 쿼리

4. **ExecuteNonQuery** - INSERT 실행
   - 새 사용자 추가 테스트

5. **최종 카운트** - 데이터 확인
   - INSERT 후 총 사용자 수 확인

## 🗃️ 공통 테이블 구조

모든 DB에 동일한 `users` 테이블이 생성됩니다:

| 컬럼 | 타입 | 설명 |
|------|------|------|
| id | INT/SERIAL | 자동 증가 기본키 |
| name | VARCHAR(100) | 사용자 이름 |
| email | VARCHAR(200) | 이메일 (UNIQUE) |
| age | INT | 나이 (NULL 가능) |
| is_active | BOOLEAN/BIT | 활성 상태 |
| created_date | TIMESTAMP | 생성 일시 |

## 👥 더미 데이터

각 DB에 5명의 사용자가 자동으로 삽입됩니다:

1. 김철수 (kim@example.com) - 30세, 활성
2. 이영희 (lee@example.com) - 25세, 활성
3. 박민수 (park@example.com) - 35세, 비활성
4. 정수진 (jung@example.com) - 28세, 활성
5. 최동욱 (choi@example.com) - 32세, 활성

## 🔧 연결 문자열

```csharp
// PostgreSQL
Host=localhost;Port=5432;Database=testdb;Username=testuser;Password=Test1234!

// MySQL
Server=localhost;Port=3306;Database=testdb;Uid=testuser;Pwd=Test1234!

// MariaDB
Server=localhost;Port=3307;Database=testdb;Uid=testuser;Pwd=Test1234!

// MS SQL Server
Server=localhost,1433;Database=testdb;User Id=sa;Password=Test1234!;TrustServerCertificate=True

// Oracle
Data Source=localhost:1521/XEPDB1;User Id=testuser;Password=Test1234!
```

## 🛠️ 유용한 Docker 명령어

```powershell
# 특정 DB만 시작
docker-compose up -d postgres mysql

# 특정 DB 중지
docker-compose stop oracle

# 전체 중지
docker-compose stop

# 전체 삭제 (데이터 유지)
docker-compose down

# 전체 삭제 (데이터까지 삭제 후 재시작)
docker-compose down -v
docker-compose up -d

# 재시작
docker-compose restart

# 특정 DB 로그 확인
docker-compose logs -f postgres

# 컨테이너 접속
docker exec -it whtoolkit-postgres psql -U testuser -d testdb
docker exec -it whtoolkit-mysql mysql -u testuser -pTest1234! testdb
docker exec -it whtoolkit-mariadb mysql -u testuser -pTest1234! testdb
docker exec -it whtoolkit-mssql /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Test1234!
docker exec -it whtoolkit-oracle sqlplus testuser/Test1234!@XEPDB1
```

## 🎯 예상 테스트 결과

```
╔════════════════════════════════════════════════════════╗
║   WHToolkit Database Helper 통합 테스트               ║
╚════════════════════════════════════════════════════════╝

== PostgreSQL 테스트 ============================================
  📋 [1/5] ExecuteList 테스트...
      ✓ 5명의 사용자 조회 성공
        - [1] 김철수 (kim@example.com) - Age: 30, Active: True, Created: 2024-01-15 10:30:00
        - [2] 이영희 (lee@example.com) - Age: 25, Active: True, Created: 2024-01-15 10:30:00
        ...

  📊 [2/5] ExecuteDataTable 테스트...
      ✓ 총 사용자: 5, 평균 나이: 30.0

  📦 [3/5] ExecuteDataSet 테스트...
      ✓ DataSet 테이블 수: 1, 활성 사용자: 4명

  ➕ [4/5] ExecuteNonQuery (INSERT) 테스트...
      ✓ 1개 행 삽입 성공

  🔢 [5/5] 최종 데이터 확인...
      ✓ 최종 사용자 수: 6명

✅ PostgreSQL 모든 테스트 통과!

... (다른 DB들도 동일) ...

╔════════════════════════════════════════════════════════╗
║  테스트 결과: ✅ 5개 성공 / ❌ 0개 실패                ║
╚════════════════════════════════════════════════════════╝
```

## ❗ 문제 해결

### Docker Desktop WSL 오류
```powershell
# WSL 업데이트
wsl --update

# WSL 재시작
wsl --shutdown

# Docker Desktop 재시작
```

### 포트 충돌
- 다른 PostgreSQL이 5432 포트 사용 중: docker-compose.yml에서 포트 변경
- 다른 MySQL이 3306 포트 사용 중: docker-compose.yml에서 포트 변경

### DB 초기화 안됨
```powershell
# 완전 삭제 후 재시작
docker-compose down -v
docker-compose up -d
docker-compose logs -f
```

## 📝 참고사항

- MS SQL은 초기화 스크립트가 자동 실행 안 될 수 있음 (수동 실행 필요)
- Oracle은 시작이 가장 느림 (1-2분 대기)
- 모든 비밀번호는 `Test1234!` 로 통일
- 테스트 데이터는 컨테이너 삭제 시 함께 삭제됨

## 🎉 완료!

이제 5개 데이터베이스에 대한 WHToolkit Helper 클래스를 한 번에 테스트할 수 있습니다!


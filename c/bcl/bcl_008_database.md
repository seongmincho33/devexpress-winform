# 데이터베이스
    1. 마이크로소프트 SQL서버
        1. 실습용 데이터베이스 준비
        2. SQL쿼리
    2. ADO.NET 데이터 제공자
        1. System.Data.SqlClient.SqlConnection
        2. System.Data.SqlClient.SqlCommand
        3. System.Data.SqlClient.SqlDataReader
        4. System.Data.SqlClient.SqlParameter
        5. System.Data.SqlClient.SqlDataAdapter
    3. 데이터 컨테이너
        1. 일반 닷넷 클래스
        2. System.Data.DataSet
        3. Typed DataSet
    4. 데이터베이스 트랜잭션

<hr />
<br />
<br />
<br />

## 2. ADO.NET 데이터 제공자

데이터베이스 프로그램은 대부분 TCP서버로 동작합니다. 따라서 TCP클라이언트 프로그램에서 데이터베이스를 사용하려면 서버의 IP주소 또는 컴퓨터 이름과 함께 포트 번호가 필요합니다. SQL서버의 경우 기본적으로 1433 포트 번호를 사용하므로 만약 SQL서버가 192.168.0.10 컴퓨터에서 실행 중이라면 그 접점 정보는 192.168.0.10:1433 이 됩니다. SQL서버와 통신하기 위해 그다음으로 알아야 할 것은 데이터를 주고받는 프로토콜 형식입니다. 다행이 이 프로토콜 형식을 개발자가 알 필요가 없습니다. 왜냐하면 데이터베이스를 만든 업체에서 프로토콜을 가장 잘 알고있기 때문에 데이터베이스 통신을 위한 전용 라이브러리를 제작해서 배포하기 때문입니다. 결국 개발자가 알아야 할 것은 DB서버와의 통신 프로토콜이 아니라 해당 라이브러리를 어떻게 사용하느냐 입니다.

이러한 전용 라이브러리를 일걸어 "ADO.NET데이터 제공자(data provider)"라고 하며, 모든ADO.NET데이터 제공자는 마이크로소프트에서 미리 정의해 둔 다음의 공통 인터페이스를 상속받아 구현합니다. 

- System.Data.IDbConnection : 데이터 베이스 서버와의 연결을 담당하는 클래스가 구현해야 할 인터페이스 정의 
- System.Data.IDbCommand : 데이터베이스 서버 측으로 실행될 SQL문을 전달하는 클래스가 구현해야 할 인터페이스 정의
- System.Data.IDataReader : 실행된 SQL문으로부터 반환받은 데이터를 열람하는 클래스가 구현해야할 인터페이스
- System.DataIDbDataParameter : IDbCommand에 전달되는 SQL문의 인자(Parameter)값을 보관하는 클래스가 구현해야할 인터페이스 정의
- System.Data.IDbDataAdapter : System.Data.DataTable개체와 상호작용하는 Data Adapter 클래스가 구현해야할 인터페이스 정의

ADO.NET데이터 제공자는 보통 데이터베이스 서버를 만든 업체에서 만들어서 배포합니다. 마이크로소프트 SQL서버의 경우 닷넷 프레임워크를 만든 마이크로스프트에서 만들었기 때문에 그에 대한 ADO.NET 데이터 제공자를 BCL에 포함시켜서 배포하고 있으므로 별도로 내려받을 필요는 없습니다. 하지만 실습하는 데이터베이스 서버가 MySQL 이라면 오라클 웹 사이트를 방문해 MySQL용 ADO.NET데이터 제공자를 내려받아 사용해야 합니다. 

마이크로소프트 SQL서버용 ADO.NET데이터 제공자는 BCL에서 System.Data.SqlClient네임스페이스 아래에 각각 다음과 같이 구현돼 있습니다.

|인터페이스|SQL 서버용 ADO.NET구현 클래스|
|--|--|
|System.Data.IDbConnection|System.Data.SqlClient.SqlConnection|
|System.Data.IDbCommand|System.Data.SqlClient.SqlCommand|
|System.Data.IDataReader|System.Data.SqlClient.SqlDataReader|
|System.DataIDbDataParameter|System.Data.SqlClient.SqlParameter|
|System.Data.IDbDataAdapter|System.Data.SqlClient.SqlDataAdapter|

이론상 적어도 데이터베이스 수만큼 ADO.NET데이터 제공자가 있을 테지만 모두 System.Data에서 제공되는 인터페이스를 상속받아 구현하고 있으므로 사용법을 한번 익혀두면 다른 데이터 제공자도 어렵지 않게 사용할 수 있습니다.

<br />

### 1. System.Data.SqlClient.SqlConnection

데이터베이스를 사용하기 위해 맨 먼저 해야 할 일은 데이터베이스에 연결하는 것입니다. 소켓 프로그래밍 단계에서 보면 Socket.Connect 메서드를 호출해 서버 프로그램에 연결하는 동작에 해당합니다. 대신 일반적인 소켓 접속과 다른점이 있다면 각 ADO.NET데이터 제공자마다 정형화된 "연결 문자열(connection string)"이 정해져 있다는 것입니다. SqlClient데이터 제공자의 경우 다음과 같은 형식입니다. 

```
Data Source=[서버]\[인스턴스명];Initial Catalog=[DB명];User ID=[계정명];Password=[비밀번호]
```

예를들어 설치한 데이터베이스와 접속계정이 다음과 같다면

```
데이터베이스 서버주소 : 192.168.0.10
데이터베이스 인스턴스 이름 : SQLEXPRESS
데이터베이스 이름 : TestDB
SQL 서버 계정 : sa
SQL 서버 계정 비밀번호 : qwer@
```

SQL서버에 접근하기 위한 연결 문자열은 이렇게 구성됩니다.

```
Data Source = 192.168.0.10\SQLEXPRESS;Initial Catalog=TestDB;User ID=sa;Password=qwer@
```

또는 SQL서버의 주소가 여러분이 만든 프로그램이 실행 될 컴퓨터와 동일하다면 다음과 같이 축약해 서 쓸 수 있습니다. (서버 주소가 점(dot)으로 대체됬습니다.)

```
Data Source = .\SQLEXPRESS;Initial Catalog=TestDB;User ID=sa;Password=qwer@
```

데이터베이스 연결 문자열이 마련되면 이제 다음과 같이 SqlConnection 개체를 사용해 DB에 접속하고 해제할 수 있습니다. 

```C#
SqlCOnnection sqlCon = new SqlCOnnection();
sqlCon.ConnectionString = 
@"Data Source = .\SQLEXPRESS;Initial Catalog=TestDB;User ID=sa;Password=qwer@";

//DB에 연결하고,
sqlCon.Open();

    // ....[DB 에 연결된 동안 DB쿼리 실행]

//연결을 닫습니다.
sqlCon.Close();
```

연결 문자열의 경우 SqlConnection을 사용할 때마다 중복해서사용하기 보다는 공통 변수에 넣어두고 재사용하는 편이 더 선호됩니다. 보통 연결 문자열은 app.config에 넣는것이 관례입니다. 

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <connectionString>
        <add name="TestDB"
            connectionString="Data Source = .\SQLEXPRESS;Initial Catalog=TestDB;User ID=sa;Password=qwer@"/>
    </connectionString>
</configuration>
```

이렇게 하는 이유는 나중에 SQL서버의 위치가 바뀌거나 계정 정보가 바뀌는 경우에도 쉽게 대응할 수 있기 때문입니다. 이렇게 app.config에 설정된 connectionString값을 코드에서 사용하려면 "System.Configuration"어셈블리를 참조 추가한 다음 그것에 포함된 ConfigurationManager타입을 이용해 가져올 수 있습니다.

```C#
string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionsString;

SqlConnection sqlCon = new SqlConnection();
sqlCon.ConnectionString = connectionString;
```

또한 SqlConnection 개체는 IDisposable인터페이스를 구현했으므로 최종적으로 다음과 같이 코드를 작성할 수 있습니다.

```C#
string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionsString;

using(SqlConnection sqlCon = new SqlConnection())
{
    sqlCon.ConnectionString = connectionString;
    sqlCon.Open();
       // ....[DB 에 연결된 동안 DB쿼리 실행]
}
```
<br />

### 2. System.Data.SqlClient.SqlCommand

일단 데이터베이스에 연결 됬으면 이제부터 해당 DB가 소유한 모든 자원을 SqlCommand타입을 이용해 조작할 수 있습니다 Ekfktj 이전에 SSMS도구의 쿼리창에서 실습했던 CRUD쿼리(INSERT, SELECT, UPDATE, DELETE) 를 SqlCommand를 이용해 실행 할 수 있습니다.

SqlCommand는 쿼리를 실행하기 위해 대표적으로 3개의 메서드를 제공합니다. 어떤 메서드를 사용하느냐는 쿼리문의 수행 결과가 반환하는 값의 종류에 따라 달라집니다.

|쿼리 종류|SqlCommand메서드|설명|
|--|--|--|
|INSERT|ExecuteNonQuery|영향받은 Row의 수를 반환|
|UPDATE|~|~|
|DELETE|~|~|
|SELECT|ExecuteScalar|1개의 값을 반환하는 쿼리를 수행|
|~|ExcuteReader|다중 레코드를 반환하는 쿼리를 수행|

INSERT, UPDATE, DELETE구문은 값을 반환하기 보다는 수행하는 것에 의미가 있는반면 SELECT는 데이터를 조회해서 반환한다는 차이가 있음을 염두해둡니다.

그럼 먼저 INSERT 쿼리를 먼저 실습해 봅니다. 어렵지 않게 다음과 같이 코드를 작성할 수 있습니다.

```C#
using(SqlConnection sqlCon = new SqlConnection())
{
    sqlCon.ConnectionString = connectionString;
    sqlCon.Open();

    SqlCOmmand cmd = new SqlCommand();
    cmd.Connection = sqlCon;
    cmd.CommandText = 
    "INSERT INTO MemberInfo(Name, Birth, Email, Family) VALUES('Fox', '2000-01-01', 'fox@gmail.com', 5)";
    int affectedCount = cmd.ExecuteNonQuery();
    Console.WriteLine(affectedCount); //출력결과 : 1
}
```

SqlCommand의 Connection 속성에 SqlConnection 객체를 할당한 후 SQL문을 COmmandText에 넣어두고 ExecuteNonQuery 메서드를 호출합니다. 반환값인 affectedCount에는 해당 SQL문을 실행 했을 때 테이블에 영향을 받는 레코드의 수가 구해집니다. 여기서는 INSERT구문으로 1개의 레코드가 추가됐으므로 1을 반환한 것입니다. 

UPDATE와 DELETE쿼리도 사용법은 완전히 같습니다. 단지 SqlCommand, CommandText속성에 해당 쿼리 문자열을 넣는 차이만 있을 뿐입니다. 

<br />

### 3. System.Data.SqlClient.SqlDataReader

<br />

### 4. System.Data.SqlClient.SqlParameter

<br />

### 5. System.Data.SqlClient.SqlDataAdapter
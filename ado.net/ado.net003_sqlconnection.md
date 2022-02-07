## SqlConnection 클래스

### 상속 계층 구조
```
- System.Object
    - System.MarshalByRefObject
        -System.ComponentModel.Component
            - System.Data.Common.DbConnection
                -System.Data.SqlClient.SqlConnection
```
### SqlConnection 연결

- 생성자를 통해 연결 SqlConnection(String)
- 생성자 + ConectionString SqlConnection()
- 연결문자열 만들기
    - 대소문자 구분하지 않음
    - MSDN 참조
    - 기본적인 연결 문자열

아래 메서드는 MSDN에서 가져온 SqlConnection의 예제코드입니다. 여기 파라미터로 연결 문자열을 넣어줘야하는데 형식이 당연히 정해져 있습니다. 알맞지 않은 상상속의 형식을 준다면 작동하지 않을겁니다.

```C#
private static void CreateCommand(string queryString, string connectionString)
{
    using (SqlConnection connection = new SqlConnection(
               connectionString))
    {
        SqlCommand command = new SqlCommand(queryString, connection);
        command.Connection.Open();
        command.ExecuteNonQuery();
    }
}
```

### SqlConnection연결문자열

- data source 또는 server 키워드에 연결문자열을 저장합니다.

example
```C#
//SQLEXPRESS는 "본인컴퓨터이름"\"DB생성했을때 이름" 에서 DB생성했을때 이름이다. 위에서 DB생성할때 이름을 SQLEXPRESS 디폴트로 주었던것을 기억해주세요. (local) 안에 아이피 주소를 넣으면 로컬이 아니고 다른컴퓨터에서도 접속이 가능해집니다.
data source = .\\SQLEXPRESS;
              (local)\SQLEXPRESS;
              (localhost)\SQLEXPRESS  
```

- initial catalog 또는 database , database = 데이터베이스명;
- integrated security 또는 trusted_connection
    - 사용자 ID와 암호 사용 : integrated security = false;
    - 윈도우즈 인증 : integrated security = true;
        - 생략시에는 false로 기본설정됨.
- user id 또는 uid : SQL 로그인 계정
- password 또는 pwd : 비밀번호
- connect timeout 또는 connection timeout 또는 timeout = 초 : 연결 대기시간으로 기본은 15초
- 기본적인 연결 문자열 예시 : "server=.\\SQLEXPRESS;database=test;uid=sa;pwd=12345;"
- 데이터베이스 연결 열기
    - public override void Open()
- 데이터베이스 버전 및 상태 출력
    - ServerVersion 속성
    - State
        - public override ConnectionState State {get;}
        - public enum ConnectionState -> Broken, Open, Closed, Connecting, Executing

<hr />
<br />
<br />
<br />
<br />
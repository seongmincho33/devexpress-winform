# BCL

C#의 기본적인 문법을 배워도 여전히 프로그램을 만들기 위한 지식이 부족합니다. 왜그러냐면 일반적으로 프로그램이란 입력 -> 처리 -> 출력 이라는 과정을 거칩니다. 지금까지 배운 C#문법은 "처리"를 위한 단계에서 사용될 수 있습니다. 입력/출력은 C#문법의 어디에도 나와있지 않습니다.

입력과 출력은 운영체제와 밀접하게 관련돼 있습니다. 닷넷 프레임워크에서는 C#과 같은 언어로 만들어진 프로그램에서 운영체제와 연동할 수 있게 관련 기능을 모아서 BCL에 담아두었습니다. 일례로 지금까지 사용한 Console타입이 한 예입니다. BCL은 운영체제에서 제공되는 입출력 기능에 의존해서 Console타입을 만들어 제공하고, C#응용프로그램에서는 Console타입을 이용해 입출력 기능을 수행하는것입니다. BCL에는 Console타입 외에도 닷넷 응용프로그램과 운영체제 사이를 중계하는 다양한 클래스를 미리 만들어 제공하고 있습니다. 즉, 운영체제의 소켓(Socket), 스레드(Thread), 파일(File), 레지스트리(Registry)등에 접근하고 싶다면 BCL에서 제공하는 클래스를 사용하면 됩니다. 

하지만 BCL이 반드시 운영체제와의 중계 역할만 담당하는것은 아닙니다. 프로그램의 "처리" 에 해당하는 과정에서 자주 사용되는 것들도 함께 포함시키기도 합니다. 예를 들어, 개발자들은 데이터의 "처리" 과정중에 다양한 수학적인 연산을 포함시키는 경우가 있습니다. Log(자연로그), Cos(코사인) 등의 메서드는 C#으로 직접 만들어 쓸 수도 있지만 이런 기능은 자주 사용되기 때문에 마이크로소프트에서는 BCL에 Math타입을 만들어 제공하고 있습니다. 

닷넷 프레임워크의 버전이 올라가면서 BCL에도 꾸준히 새로운 기능들이 추가되고있습니다. 예를 들어, 닷넷 프레임워크 1.1 시절에는 string 변수가 값을 가지고 있는지 확인하기 위해 다음과 같이 코드를 작성해야했습니다. 

```C#
string text = .....;

if(text == null || text == "") 
{

}
```

마이크로소프트에서는 이런 표현이 자주 사용된다는 것을 알고 닷넷 프레임워크 2.0 의 BCL에는 string 타입에 IsNullOrEmpty 메서드를 추가했습니다. 따라서 다음과 같이 바꿀 수 있습니다. 

```C#
string text = .....;

if (string.IsNullOrEmpty(text) == true)
{

}
```

당연하지만 IsNullOrEmpty메서드를 사용한 프로그램은 닷넷 프레임워크 1.1 환경에서는 동작하지 않습니다. 따라서 C#으로 응용프로그램을 만들 때는 우선 만든 프로그램이 어떤 버전의 닷넷 프레임워크부터 지원할지 결정해야 합니다. 예를 들어, 닷넷 프레임워크 3.0 이상의 환경에서 동작하는 프로그램을 만들기로 했다면 절대로 3.5 이상에서만 제공되는 BCL기능을 사용해서는 안됩니다. 

1. 시간
    1. System.DateTime
    2. System.TimeSpan
    3. System.Diagnostics.Stopwatch

2. 문자열 처리
    1. System.String
    2. System.Text.StringBuilder
    3. System.Text.Encoding
    4. System.Text.RegularExpressions.Regex

3. 직렬화/역직렬화
    1. System.BitConverter
    2. System..IO.MemoryStream
    3. System.IO.StreamWriter / System.IO.StreamReader
    4. System.IO.BinaryWriter / System.IO.BinaryReader
    5. System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
    6. System.Sml.Serialization.XmlSerializer
    7.System.Runtime.Serialization.Json.DataContractJsonSerializer

4. 컬렉션
    1. System.Collections.ArrayList
    2. System.Collections.Hashtable
    3. System.Collections.SortedList
    4. System.Collections.Stack
    5. System.Collections.Queue

5. 파일 
    1. System.IO.FileStream
    2. System.IO.File /System.IO.FileInfo
    3. System.IO.Directory /System.IO.DirectoryInfo
    4. System.IO.Path

6. 스레딩
    1. System.Threading.Thread
    2. System.Threading.Monitor
    3. System.Threading.Interlocked
    4. System.Threading.ThreadPool
    5. System.Threading.EventWaitHandle
    6. 비동기 호출
    7. System.Delegate 비동기 호출

7. 네트워크 통신
    1. System.Net.IPAdress
    2. 포트
    3. System.Net.IPEndPoint
    4. System.Net.Dns
    5. System.NetSockets.Socket
        1. UDP 소켓
        2. TCP 소켓
        3. TCP 서버 개선 - 다중 스레드와 비동기 통신
        4. HTTP 통신
    6. System.Net.HttpWebRequest
    7. System.Net.WebClient

8. 데이터베이스
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

9. 리플렉션
    1. AppDomain과 Assembly
    2. Type과 리플렉션
    3. 리플렉션을 이용한 확장 모듈 구현

10. 기타
    1. 윈도우 레지스트리
    2. BigInteger
    3. IntPtr


# C# 인덱스

1. [BCL](bcl_library/bcl_library001.md)

2. C# 1.0
    1. [문법 요소](c1/syntax_elements001.md)
        1. 구문
            1. 전처리기 지시문
            2. 지역 변수의 유효 범위
            3. 리터럴에도 적용되는 타입
            4. 특성
                - 사용자 정의 특성
                - 특성이 적용될 대상을 제한
                - 다중 적용과 상속
                - AssemblyInfo.cs
        2. 예약어
            1. 연산 법위 확인 : checked, unchecked
            2. 가변 매개변수 : params
            3. Win32 API 호출 : extern
            4. 안전하지 않은 컨텍스트 : unsafe
            5. 참조 형식의 맴버에 대한 포인터 : fixed
            6. 고정 크기 버퍼 : fixed
            7. 스택을 이용한 값 형식 배열 : stackalloc
    2. [프로젝트 구성](c1/project_structure001.md)
        1. 다중 소스코드 파일
        2. 라이브러리
            1. csc.exe로 라이브러리 생성 및 사용
            2. 비주얼 스튜디오에서 라이브러리 생성 및 사용
        3. 응용프로그램 구성 파일 : app.config
            1. supportedRuntime
            2. appSettings
        4. 디버그 빌드와 릴리스 빌드
            1. DEBUG, TRACE 전처리 상수
            2. Debug 타입과 Trace 타입
        5. 플랫폼 (x86, x64, AnyCPU) 선택
        6. 버전관리 
            1. 어셈블리의 버전과 이름
            2. 공개키 토큰과 어셈블리 서명
            3. 전용 어셈블리, 전역 어셈블리
    3. [예외](c1/exception001.md)
        1. 예외 타입
        2. 예외 처리기
        3. 호출 스택
        4. 예외 발생
        5. 사용자 정의 예외 타입
        6. 올바른 예외 처리
    4. [스택](c1/stack001.md)    
        1. 스택 오버플로
        2. 재귀 호출
    5. [힙](c1/heap001.md)
        1. 박싱/언박싱
        2. 가비지 컬렉터
        3. 전체 가비지 컬렉팅
        4. 대용량 객체 힙
        5. 자원 해제
        6. 종료자
3. C# 2.0

4. C# 3.0

5. C# 4.0

6. C# 5.0



<hr />

## 기타
1. [프로퍼티를 Call-By-Reference로 던지기(Passing properties by reference in C#)](etc/etc001_call_by_reference.md)
    - Return Value
    - Delegate
    - LINQ Expression
    - Reflection
2. [Validation(유효성 검사)](etc/etc002_validation.md)
	- 주민등록번호
	- 전화번호
	- 이메일
	- 계좌번호
	- 주소
	- 시간(날짜)
	- Guid?
3. [프로퍼티](property/property.md)
4. [delegate(델리게이트)](delegate_and_event/delegate_and_event.md)

    1. 일반적인경우
    2. delegate 사용
    3. 언제 사용하지
    4. 어디서 본듯한 delegate
    5. 이벤트
    6. MS 제공 델리게이트
        - Action<T> delegate
        - Func<T> delegate
        - Predicate<T> delegate

5. [링큐](linq/linq.md)
    1. 기본사용
    2. LINQ 확장메서드 사용
    3. 기본사용 CROSS JOIN
    4. 기본사용 INNER JOIN
    5. LEFT JOIN
    6. GROUP JOIN
    7. INNER JOIN MULTI 조건
    8. Dictionary LINQ
    9. LINQ를 이용한 UPDATE001
    10. LINQ를 이용한 UPDATE002
    11. LINQ를 이용한 DELETE001
    12. LINQ를 이용한 DELETE002
    13. 링큐패드5 (LINQPad5 .netFrameWork)
    - 링큐패드 소개
		- 링큐패드 튜토리얼
		- 5분 튜토리얼
			- Hello LINQPad!
			- A simple query expression
			- Multiple statements
			- The Big Dump
			- Custom methods and types
			- What about querying a database!
			- But I dont't havve NORTHWIND!
			- More on database querying
			- PredicateBuilder is included!
		- Ui 둘러보기
		- 스크래치패드 특징들
		- 스트립팅 및 자동화 특징들
		- 데이터베이스 쿼리 특징들
		- 링큐패드 링큐 -> 람다링큐
		- 링큐패드 람큐 -> SQL
		- 링큐패드 IL 분석
		- 링큐패드 Tree
		- 단축기
_____________________________________________________

















# C# 1.0

## 1. [문법 요소](c1_001_syntax_element.md)
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

<br/>

## 2. [프로젝트 구성](c1_002_project_structure.md)
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

<br/>

## 3. [예외](c1_003_exception.md)
1. 예외 타입
2. 예외 처리기
3. 호출 스택
4. 예외 발생
5. 사용자 정의 예외 타입
6. 올바른 예외 처리

<br/>

## 4. [스택](c1_004_stack.md)    
1. 스택 오버플로
2. 재귀 호출

<br/>

## 5. [힙](c1_005_heap.md)
1. 박싱/언박싱
2. 가비지 컬렉터
3. 전체 가비지 컬렉팅
4. 대용량 객체 힙
5. 자원 해제
6. 종료자
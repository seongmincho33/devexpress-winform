# C# 9.0

C# 9.0은  2020년 11월 10일에 있었던 ".NET Conf 2020"세미나에서 "닷넷5"의 발표와 함께 공개되었습니다. 개발환경은 16.8 버전의 비주얼 스튜디오 2019 부터 제공되며, 닷넷5 대상 프로젝트만 기본 컴파일러 버전으로 9.0이 설정되고 다른 프로젝트(닷넷 프레임워크 및 닷넷 코어 3.1 이하)에서는 명시적으로 <LangVersion>9.0<LangVersion>을 추가해야 사용할 수 있습니다.

새로 추가된 기능은 15개로 정리할 수 있습니다. 이번에도 C#8.0과 마찬가지로 일부 기능은 닷넷 5의 BCL또는 런타임에 의존합니다. 당연히 런타임에 의존하는 기능은 오직 닷넷 5 환경에서만 사용할 수 있지만, BCL에 의존하는 경우는 다른환경(닷넷 프레임워크, 닷넷 코어 3.1 이하)에도 타입을 직접 정의해 추가하는 방식으로 우회해 사용할 수 있습니다. 

|기능|닷넷 프레임워크, 닷넷 코어 3.1 이하|
|--|--|
|레코드|지원|
|대상으로 형식화된 new 식|지원|
|달라진 조건식 평가|지원|
|로컬 함수에 특성 지정 가능|지원|
|익명 함수 개선|지원|
|최상위 문|지원|
|패턴 일치 개선 사항|지원|
|모듈 이니셜라이저|개발자가 타입추가하면 지원가능|
|공변 반환 형식|미지원(닷넷 5환경에서만 사용가능)|
|foreach루프에 대한 확장 GetEnumerator지원|지원|
|부분 메서드에 대한 새로운 기능|지원|
|localsinit플래그 내보내기 무시|개발자가 타입추가하면 지원가능|
|원시 크기 정수|지원|
|함수 포인터|일부 지원|
|제약 조건이 없는 형식 매개변수 주석|지원|

<hr />
<br />

## 1. [(지원)레코드](c9_001_record.md)
1. init 설정자 추가
2. with 초기화 구문 추가
## 2. [(지원)대상으로 형식화된 new 식(Traget-typed new expressions)](c9_002_new_expressions.md)
## 3. [(지원)달라진 조건식 평가](c9_003_conditional_expressions.md)
1. 대상으로 형식화된 조건식(Target-typed conditional expressions)
2. 메서드 인자로 전달 시 엄격해진 조건식 평가
## 4. [(지원)로컬 함수에 특성 지정 가능(Attributes on local functions)](c9_004_attributes_on_local_functions.md)
## 5. [(지원)익명 함수 개선](c9_005_anonymous_functions.md)
1. 정적 익명 함수(static anonymous functions)
2. 익명 함수의 매개변수 무시
## 6. [(지원)최상위 문(Top-level statements)](c9_006_top_level_statements.md)
## 7. [(지원)패턴 일치 개선 사항(Pattern matching enhancements)](c9_007_pattern_matching.md)
## 8. [(개발자가 타입추가하면 지원가능)모듈 이니셜라이저(Module initializers)](c9_008_module_initializers.md)
## 9. [(미지원)공변 반환 형식(Covariant return types)](c9_009_covariant_return_type.md)
## 10. [(지원)foreach 루프에 대한 GetEnumerator 확장 메서드 지원 (Extention GetEnumerator)](c9_010_getenumerator.md)
## 11. [(지원)부분 메서드에 대한 새로운 기능(New features for partial methods)](c9_011_partial_methods.md)
## 12. [(개발자가 타입추가하면 지원가능)localsinit 플래그 내보내기 무시(Suppress emitting localsinit flag)](c9_012_localsinit_flag.md)
## 13. [(지원)원시 크기 정수(Native ints)](c9_013_native_ints.md)
## 14. [(일부 지원)함수 포인터(Functions pointers)](c9_014_functions_pointers.md)
1. 비관리 함수 포인터 지원
2. 비관리 함수를 통한 콜백 지원
## 15. [(지원)제약 조건이 없는 형식 매개변수 주석(Unconstrained type parameter annotaions)](c9_015_unconstrained_type_parameter_annotaions.md)
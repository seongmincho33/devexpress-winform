# 데브 익스프레스 컨트롤 세팅법

#### 1. [GridControl](https://github.com/seongmincho33/devexpress-winform/blob/main/GridControl/GridControlSetting.md)
1. 컬럼 클릭 방지
2. 셀값 바뀌면 다른셀값도 세팅해주는 방법
	- 셀값이 바뀔때 테이블에서 링큐걸어서 같은로우에 있는 다른 셀값들을 불러올 수 있다. 
3. 셀의 크기와 컬럼헤더(팬드뷰면 밴드 헤더) 크기 셀의 색을 바꿔줄 수 있다.
4. 로우셀들에게 색깔 주기(readonly 회색 포함)
5. 그룹핑 및 컬럼소팅
	- 컬럼 소팅 이벤트정의 (정규표현식)
	- 풋터에 합, 평균 등 계산값 정리해주기
		- 풋터(footer)의 종류
6. 그리드뷰의 종류 : 밴드그리드뷰 (BandedGridView) 
	- BandedGridView 컬럼 Header 에 색깔주기
7. 동적컬럼
	- 동적컬럼 CU 저장할때 컬럼 잘라서 넘기기. 아래 두 메서드는 바로 위 DataSave() 에서 사용은 안했는데 사용할수도 있으니 참고바람. 출처는 MS Q&A
8. 그리드뷰의 로우를 위 아래로 옮겨주기(위아래 버튼만들기)
9. 로우 추가할때 순서대로 번호든 문자든 주기
10. 정규식으로 컬럼에 정해진값만 작성할 수 있게 만들어주기
11. 그리드뷰 필터에 관해서 
	- 필터를 안보이게 하는법
	- 필터가 BOOLEAN 일때 기본 디폴트값을 FALSE로 주는 방법 (기본은 NULL인듯 하다)
12. 그리드 컨트롤 화면단에 불필요한 0 이 있을때 안보이게 하는 방법
#### 2. TextEditControl
1. 텍스트 에딧에 내용을 넣고 엔터를 누르면 검색되게 만들기 (keydown event)
#### 3. DataEditControl
1. DateEdit. 사용자 달력클릭시 일어나는 이벤트
2. DateTime.MinValue 와 (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue 의 차이
3. EditDate 달력 일 클릭시 컬럼에 자동으로 날짜값 넣어주기
4. EditDate 달력에 연월 선택시 일중에 주말만 표시해주기 셀값에 칠하기
5. DateEdit의 .MinValue를 sql에 넘겨주지 못하므로 해결하는 두번째 방법
6. DateEdit 달력을 사용자로부터 연월만 받아오고 연월만 텍스트를 달력컬럼에 붙여주기
#### 4. LookUpEditControl
#### 5. XtraTabControl
#### 6. UserControl
#### 7. SpreadSheetControl
1. 스프레드 시트의 모든 0들을 지워주기
2. 스프레드 시트의 컨트롤러 메서드 정리할때 주의할점
#### 8. 기타
1. 링큐로 테이블데이터 가져오는 방법
2. 이벤트 핸들러의 파라미터 e에 관해서 callby reference사용해 함수로 코드양 줄이기
3. 여러개의 나눠져있는 UI 인터페이스들을 컨트롤로 넘길때 튜플로 묶어주는 방법
4. 텍스트 세팅 (포멧스트링 세팅법)
#### 9. 링큐
1. LINQPad5 (.netFrameWork 버전)

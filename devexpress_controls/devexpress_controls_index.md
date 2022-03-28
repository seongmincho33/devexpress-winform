# 목차

1. [컨트롤 공통속성](devexpress_controls_001_properties.md)
    1. ApplicationSettings
    2. DataBindings
    3. AllowDrop
    4. AllowHtmlTextInToolTip
    5. CausesValidation
    6. EnterMoveNextControl
    7. GenerateMember
    8. ImeMode
    9. Menumanager
    10. StyleController
    11. SuperTip
    12. TabIndex
    13. TabStop
    14. Tag
    15. ToolTipAnchor
    16. ToolTipController
    17. UseWaitCursor
2. [GridControl](devexpress_controls_002_gridcontrol.md)
    1. 컬럼 클릭 방지
    2. 셀값 바뀌면 다른셀값도 세팅해주는 방법
    	- 셀값이 바뀔때 테이블에서 링큐걸어서 같은로우에 있는 다른 셀값들을 불러올 수 있다. 
    3. 셀의 크기와 컬럼헤더(밴드뷰면 밴드 헤더) 크기 셀의 색을 바꿔줄 수 있다.
        - 로우셀들에게 색깔 주기(readonly 회색 포함)
    4. 그룹핑 및 컬럼소팅
    	- 컬럼 소팅 이벤트정의 (정규표현식)
    	- 풋터(footer)의 종류에 대해서
        	- 풋터에 합, 평균 등 계산값 정리해주기
        - Custom Summary 등록 및 CustomSummaryCalculate 이벤트 (동적컬럼)
    5. 그리드뷰의 종류 : 밴드그리드뷰 (BandedGridView)
    	- BandedGridView 컬럼 Header 에 색깔주기
        - BandedGridView 컬럼 Header 에 색을 주면서 동시에 아래 셀들에게도 색깔을 주는 방법
    6. 동적컬럼
        - 동적컬럼 바인딩 했을때 컬럼의 크기 ~DataModel에서 자동으로 맞춰주기 
        - 동적컬럼 바인딩 했을때 만약 ~DataModel의 바인딩인포가 readonly여서 색이 검정이면 바꿔주기
    	- 동적컬럼 CU 저장할때 컬럼 잘라서 넘기기. 아래 두 메서드는 바로 위 DataSave() 에서 사용은 안했는데 사용할수도 있으니 참고바람. 출처는 MS Q&A
    7. 그리드뷰의 로우를 위 아래로 옮겨주기(위아래 버튼만들기)
    8. 로우 추가할때 순서대로 번호든 문자든 주기
    9. 정규식으로 컬럼에 정해진값만 작성할 수 있게 만들어주기
        - 정규식: C# 에서 sql처럼 like형식으로 사용하는 방법 이유 -> like방식이 syntax가 아주 쉽기 때문이다.
    10. 그리드뷰 필터에 관해서
    	- 필터를 안보이게 하는법
    	- 필터가 BOOLEAN 일때 기본 디폴트값을 FALSE로 주는 방법 (기본은 NULL인듯 하다)
    11. 그리드 컨트롤 화면단에 불필요한 0 이 있을때 안보이게 하는 방법
    12. 그리드 컨트롤 CheckBoxSelector(그리드뷰에 체크박스를 넣어줘서 선택하게 만듬)
        - 팝업창에서 그리드 컨트롤 CheckBoxSelector 으로 체크한 것들 메인 그리드에 저장해주기
    13. 그리드뷰의 특정 컬럼을 수정하면 전체 데이터가 수정됨(ex: 모든 로우셀이 boolean체크박스인데 하나 체크하고 다른거 체크하면 전체가 풀려야함)
    14. 컨트롤단에서 밴디드그리드뷰던 그냥 그리드뷰던 visible false주기
    15. Unbound Column 주지 않고 커스텀 계산한 컬럼 만드는법
    16. 이벤트 헤제와 등록을 통해서 특정 이벤트의 무한루프 런타임에 다른이벤트 실행하는 방법
    17. 텝페이지가 바뀔때 저장할건지 물어보기 VALIDATION포함.
    18. TextEditControl의 값을 선택못하게 하고 보여지게 만드는법
    19. 컬럼셀 클릭시 포멧스트링이 변환되는데 이를 막거나 바꾸는 방법
    20. 그리드 뷰 안의 특정 날짜 선택 컬럼의 포멧을 바꾸는 방법
    21. 그리드 뷰 로우 더블클릭 이벤트(ex:더블클릭하면 팝업등이 보여지게 함)
    22. 그리드 뷰의 컬럼사이즈를 수정하고 유지하는방법
    23. 현재 화면의 컨트롤을 클릭하면 커서가 진행중으로 바뀌게 하는방법
    24. CellMerge 이벤트를 사용해서 셀 병합하는 방법
    25. 포커스가 먼저 주어진 그리드의 행에 색을 주는 방법(특이케이스)
    26. 데이터 바인딩이 된 그리드뷰는 행을 추가할 수 없다.
    27. 그리드뷰의 CellValueChanging 의 Cancel이 없기 때문에 셀의 Validation을 KeyPress로 해결하는 방법
    28. 그리드뷰 행 멀티 삭제
    29. PopulateColumns 에 관하여
    30. XtraTabPageControl의 텝페이지를 마우스로 옮기는 방법
3. [SpreadSheetControl](devexpress_controls_003_spreadsheetcontrol.md)
    1. WorkBook과 WorkSheet
    2. SpreadSheetControl의 BeginUpdate()와 EndUpdate()
    3. 위크시트의 범위를 정하고 값과 셀크기등을 조절하는 방법 (FromLTRB)
        - 값 할당
            - 자동 변환
            - 함수 할당
        - 셀 형식
            - 셀 보더
            - 셀 사이즈
            - 셀 폰트 사이즈
            - 셀 Alignment
    4. 셀 머징(Merge)
    5. 데이터 테이블을 워크시트에 붙여주는 방법
    6. 컬럼 더하기 기능 만들기
    7. 스프레드 시트의 모든 0들을 지워주기
    8. 동적컬럼 테이블 붙일때 고려사항
    9. 이름이 애매한 컬럼의 전체 로우들을 색칠해주기
    10. 워크시트의 위치에 대한 변수를 고려해야할 사항들에 관해서.
        - 리펙터링
    11. RibbonControl
4. [DateEditControl](devexpress_controls_004_dateeditcontrol.md)
    1. DateEdit. 사용자 달력클릭시 일어나는 이벤트
    2. DateTime.MinValue 와 (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue 의 차이
    3. EditDate 달력 일 클릭시 컬럼에 자동으로 날짜값 넣어주기
    4. EditDate 달력에 연월 선택시 그리드뷰에 주말만 표시해주기 (셀값에 칠하기)
    5. DateEdit의 .MinValue를 sql에 넘겨주지 못하므로 해결하는 두번째 방법
    6. DateEdit 달력을 사용자로부터 연월만 받아오고 연월만 텍스트를 달력컬럼에 붙여주기
    7. DateEdit 컨트롤을 두개 사용해서 기간을 받아야할때 처음 셋팅값 주기.





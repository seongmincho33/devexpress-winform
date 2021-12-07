# 1. GridControl

<img src="../../img/gridcontrol_img/gridcontrol.png" width="400" height="400"/>

1. [컬럼 클릭 방지](#1-컬럼-클릭-방지)
2. [셀값 바뀌면 다른셀값도 세팅해주는 방법](#2-셀값-바뀌면-다른셀값도-세팅해주는-방법)
	- 셀값이 바뀔때 테이블에서 링큐걸어서 같은로우에 있는 다른 셀값들을 불러올 수 있다. 
3. [셀의 크기와 컬럼헤더(밴드뷰면 밴드 헤더) 크기 셀의 색을 바꿔줄 수 있다.](#3-셀의-크기와-컬럼헤더밴드뷰면-밴드-헤더-크기-셀의-색을-바꿔줄-수-있다)
    - 로우셀들에게 색깔 주기(readonly 회색 포함)
4. [그룹핑 및 컬럼소팅](#4-그룹핑-및-컬럼소팅)
	- 컬럼 소팅 이벤트정의 (정규표현식)
	- 풋터(footer)의 종류에 대해서
    	- 풋터에 합, 평균 등 계산값 정리해주기
    - Custom Summary 등록 및 CustomSummaryCalculate 이벤트 (동적컬럼)
5. [그리드뷰의 종류 : 밴드그리드뷰 (BandedGridView)](#5-그리드뷰의-종류--밴드그리드뷰-bandedgridview) 
	- BandedGridView 컬럼 Header 에 색깔주기
6. [동적컬럼](#6-동적컬럼)
    - 동적컬럼 바인딩 했을때 컬럼의 크기 ~DataModel에서 자동으로 맞춰주기 
    - 동적컬럼 바인딩 했을때 만약 ~DataModel의 바인딩인포가 readonly여서 색이 검정이면 바꿔주기
	- 동적컬럼 CU 저장할때 컬럼 잘라서 넘기기. 아래 두 메서드는 바로 위 DataSave() 에서 사용은 안했는데 사용할수도 있으니 참고바람. 출처는 MS Q&A
7. [그리드뷰의 로우를 위 아래로 옮겨주기(위아래 버튼만들기)](#7-그리드뷰의-로우를-위-아래로-옮겨주기위아래-버튼만들기)
8. [로우 추가할때 순서대로 번호든 문자든 주기](#8-로우-추가할때-순서대로-번호든-문자든-주기)
9. [정규식으로 컬럼에 정해진값만 작성할 수 있게 만들어주기](#9-정규식으로-컬럼에-정해진값만-작성할-수-있게-만들어주기)
    - 정규식: C# 에서 sql처럼 like형식으로 사용하는 방법 이유 -> like방식이 syntax가 아주 쉽기 때문이다.
10. [그리드뷰 필터에 관해서 ](#10-그리드뷰-필터에-관해서)
	- 필터를 안보이게 하는법
	- 필터가 BOOLEAN 일때 기본 디폴트값을 FALSE로 주는 방법 (기본은 NULL인듯 하다)
11. [그리드 컨트롤 화면단에 불필요한 0 이 있을때 안보이게 하는 방법](#11-그리드-컨트롤-화면단에-불필요한-0-이-있을때-안보이게-하는-방법)
12. [그리드 컨트롤 CheckBoxSelector(그리드뷰에 체크박스를 넣어줘서 선택하게 만듬)](#12-그리드-컨트롤-CheckBoxSelector그리드뷰에-체크박스를-넣어줘서-선택하게-만듬)
    - 팝업창에서 그리드 컨트롤 CheckBoxSelector 으로 체크한 것들 메인 그리드에 저장해주기
13. [그리드뷰의 특정 컬럼을 수정하면 전체 데이터가 수정됨(ex: 모든 로우셀이 boolean체크박스인데 하나 체크하고 다른거 체크하면 전체가 풀려야함)](#13-그리드뷰의-특정-컬럼을-수정하면-전체-데이터가-수정됨ex-모든-로우셀이-boolean체크박스인데-하나-체크하고-다른거-체크하면-전체가-풀려야함)
14. [컨트롤단에서 밴디드그리드뷰던 그냥 그리드뷰던 visible false주기](#14-컨트롤단에서-밴디드그리드뷰던-그냥-그리드뷰던-visible-false주기)


_________________________________________________________________________
<br>



# 1. 컬럼 클릭 방지

아래는 그리드뷰의 컬럼을 사용자가 선택하지 못하게 막는 이벤트르 사용했다.
ShowingEditor를 사용하고 e.cancle = true; 로 하면 선택이 되지 않는다. 
스위치문을 사용해서 특정 컬럼만을 대상으로 선택을 막을 수 있다.
gridview.FocusedColumn.FieldName 으로 선택한(포커스된) 셀의 컬럼이름을 비교하여
원하는 컬럼에만 showingeditor 이벤트를 사용할 수 있다. 
```c#
Grid_Something.ShowingEditor += Grid_Something_ShowingEditor;

private void Grid_Something_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
{
    switch (Grid_Something.DefaultView.FocusedColumn.FieldName)
    {
        case "CAR_NUMBER":
        case "PHONE_NUMBER":
        case "TON_CAR":
        case "FC_NAME":
            e.Cancel = true;
            break;
        default:
            e.Cancel = false;
            break;
    }
    
}
```
_________________________________________________________________________
<br>

# 2. 셀값 바뀌면 다른셀값도 세팅해주는 방법
CellValueChanged 이벤트를 사용해서 선택한 셀이 바뀔때 하려는일을 정해줄 수 있다.
``` C#
gridview.CellValueChanged += gridview_CellValueChanged;
```
따라서 선택한 셀값이 바뀔때 다른셀에도 값을 넣어줄 수 있게된다. 보통은 LookUp안의 내용을
선택하면 같은 로우의 다른 셀값들이 자동으로 바인딩되는 식의 일이 많다.
``` C#
private void gridview_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
{
    var item = this.gridview.GetFocusedRow<ISOMETHINGModel>();
    if (item == null) return;

    //SOMETHING_ID셀값을 바꿔주면 차량번호,연락처,차량톤수,거래처 를 자동으로 붙여줍니다. 
    if(e.Column.FieldName == "SOMETHING_ID")
    {
        var items = this.SomethingDataTable.AsEnumerable().Where(i => i["SOMETHING_ID"].ToString() == item.SOMETHING_ID.ToString());

        if(items.Count() > 0)
        {
            DataRow row = items.FirstOrDefault();
            if (row["CAR_NUMBER"] != DBNull.Value)
            {
                item.CAR_NUMBER = row["CAR_NUMBER"].ToString();
            }
            if (row["PHONE_NUMBER"] != DBNull.Value)
            {
                item.PHONE_NUMBER = row["PHONE_NUMBER"].ToString();
            }
            if (row["TON_CAR"] != DBNull.Value)
            {
                item.TON_CAR = row["TON_CAR"].ToString();
            }
            if (row["FC_NAME"] != DBNull.Value)
            {
                item.FC_NAME = row["FC_NAME"].ToString();
            }
        }
    }
}
```
위의 코드를 보면 선택한 그리드뷰의 로우를 GetFocusedRow\<T>() 메서드를 사용해서 item에 붙여주었다. 
```C#
if(e.Column.FieldName == "SOMETHING_ID")
```
여기 if문에서 파라미터 e는 바뀐 셀값의 컬럼을 리턴한다. 
________________________________________________________________________________________________
<br>

# 3. 셀의 크기와 컬럼헤더(밴드뷰면 밴드 헤더) 크기 셀의 색을 바꿔줄 수 있다
밴디드그리드뷰의 헤더면 그냥 그리드뷰의 헤더와 다르니깐 유의해야한다. UI설정탭에서도 설정가능하다. 
그래도 컨트롤에서 수정하는것이 좋다. 안그러면 어디서 어떻게 무엇을 바꾸었는지 잘 모르게 된다.
- XtraGrid BandedGridView.BandPanelRowHeight Property.
- XtraGrid GridView.ColumnPanelRowHeight Property.

UI설정에 여기로 가면 프로퍼티를 설정해 줄 수 있긴 하다.
이름값을 알아오는데 유용하게 쓸 수 있다.
아래 코드는 이벤트 들이다. 설명에 따라서 기능이 여러가지다. 중요한것은 폰트 크기와 색은 셀의
크기와 전혀 상관이 없다는 것이다.!! 폰트크기를 키워주면 자동으로 셀의 크기도 늘어나야 할 것 같지만
꼭 그렇지만은 않다. 셀의 크기를 변경해주는 방법은 

- 이벤트로 하나하나 생성될때 늘려주는 방법(Calc.RowHeight())
- UI에서 초기 세팅값을 바꿔주는 방법

이 있다.
```C#
//SetGridControl
        
//CustomDrawCell 이벤트 사용금지. 셀 선택 안됨. RowCellStyle 사용.
//((BandedGridView)this.gridview).CustomDrawCell += SomethingController_CustomDrawCell;
((BandedGridView)this.gridview).RowCellStyle += SomethingController_RowCellStyle;
//필요시 CalcRowHeight이벤트로 셀 크기변경 가능.
((BandedGridView)this.gridview).CalcRowHeight += SomethingController_CalcRowHeight;
((BandedGridView)this.gridview).CustomDrawBandHeader += SomethingController_CustomDrawBandHeader;
((BandedGridView)this.gridviewv).BandPanelRowHeight = 40;
```
```C#
//bandinfo의 readonly의 색을 디폴트로 바꿔줍니다. 
private void SomethingController_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
{
    if (e.Column.OptionsColumn.ReadOnly)
        e.Appearance.BackColor = Color.Empty;
    //e.Appearance.Font = new Font("", 15);
    e.Appearance.FontSizeDelta = 10;
}
//셀의 크기를 변경해줍니다. 
private void SomethingController_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
{
    e.RowHeight = 60; 
}
//밴드헤더의 크기를 설정합니다.
private void SomethingController_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
{
    e.Appearance.FontSizeDelta = 10;            
}
```
아 그리고 혹시 컬럼헤더에 \n 속성값을 준다면 컬럼헤더의 위아래 너비를 키워줘야 \n이 먹는다. 그래서
이것저것 할거 없이 크기를 늘려주기만 하면 \n이 먹는다. 

```C#
((BandedGridView)this.gridviewv).BandPanelRowHeight = 100; // 밴디드 그리드뷰일 경우~!
```

혹시나 \n이 먹질 않는다면 \r\n을 먹여줘야 줄바꿈이 일어날때도 있을 수 있다. 
영어로 줄바꿈음 "line break" 또는 "multiple line" 혹은 "multiline"으로 검색하면 됩니다.

```C#
gridColumn.Caption = "Line 1" + Environment.NewLine + "Line 2";  
```

특정 컬럼에다가 색을 주는 방법이다. 이름이 SOMETHING(N)인 컬럼에는 엘리스 블루 색깔을 주고 나머지는 READONLY의 회색색깔을 주는 방법이다. READONLY회색은 아래와같이 선언해서 써야한다.
```C#
private void DefaultView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
{
    Skin currentSkin = CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
    Color readOnlyColor = currentSkin.Colors["ReadOnly"];
    GridView currentView = sender as GridView;
    if(
        e.Column.FieldName == "SOMETHING1" ||
        e.Column.FieldName == "SOMETHING2" ||
        e.Column.FieldName == "SOMETHING3" ||
        e.Column.FieldName == "SOMETHING4" ||
        e.Column.FieldName == "SOMETHING5"
        )
    {
        e.Appearance.BackColor = Color.AliceBlue;
    }
    else
    {
        e.Appearance.BackColor = readOnlyColor;
    }
}
```

__________________________________________________________________________
<br>

# 4. 그룹핑 및 컬럼소팅
셀들을 병합 머징 해줘서 보기 편하게 만들어줍니다.

처음에는 아래와 같이 이벤트를 등록합니다. AllowCellMerge 프로퍼티를 true로 셋팅해줘야합니다.
```C#
this.gridview.OptionsView.AllowCellMerge = true;	
this.gridview.CellMerge += Grid_CellMerge	
```
아래는 이벤트 메서드 입니다.
```C#
private void Grid_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
{
    DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
    try
    {
        if (e.Column.FieldName == "SOMETHING_COLUMN_NAME")
        {
            string class1 = view.GetRowCellDisplayText(e.RowHandle1, "SOMETHING_COLUMN_NAME");
            string class2 = view.GetRowCellDisplayText(e.RowHandle2, "SOMETHING_COLUMN_NAME");
            e.Merge = (class1 == class2);
            e.Handled = true;
        }
        else
        {
            e.Merge = false;
            e.Handled = true;
        }
    }
    catch (Exception ex)
    {
    }
}
```
아래는 그룹핑 함수를 만들어 보았습니다. 화면에 그리드가 여러개 이고 탭으로 나누어져 있다고 가정해 보면 그리드마다 아래와같이 써줘야 하므로 함수 하나 만드는게 이득입니다.
``` C#
private void SetGroup(GridControl gcControl, string group_col_NAME)
{
    gcControl.Columns[group_col_NAME].FieldNameSortGroup = group_col_NAME;
    gcControl.Columns[group_col_NAME].SortMode = DevExpress.XtraGrid.ColumnSortMode.Default;
    gcControl.Columns[group_col_NAME].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
    gcControl.Columns[group_col_NAME].Group();
    gcControl.DefaultView.ExpandAllGroups();
}
```
<br>
<br>
<br>

- 1. 컬럼 소팅 이벤트정의 (정규표현식)

아래 코드는 이벤트 CustomColumnSort 를 그리드뷰에 등록합니다.
```C#
this.gridview.CustomColumnSort += gridview_CustomColumnSort;
```
등록한 이벤트를 정의해줍니다.
```C#
private void gridview_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
{
    string pattern = @"^[a-zA-Z0-9가-힣]*$";
    if ((Regex.IsMatch(e.Value2.ToString(), pattern)) && (Regex.IsMatch(e.Value1.ToString(), pattern)))
    {
        try
        {
            int value1 = Convert.ToInt32(e.Value1);
            int value2 = Convert.ToInt32(e.Value2);
            e.Result = value1.CompareTo(value2); //new line  
            e.Handled = true; //new line  
        }
        catch { }
    }
}
```
<br>
<br>
<br>

- 2. 풋터(footer)의 종류에 대해서
  
footer에는 여러 종류가 있는데 거의 두종류가 쓰입니다. 하나는 그리드뷰 전체를 서머리(summary)주는 풋터가 있고 또다른 하나는 그룹핑을 했을때 서머리 주는 풋터가 있습니다. 그 외에도 있는데 devexpress 홈페이지를 확인해주세요. 서머리는 컬럼을 엑셀과 비슷하게 자동 계산해서 보여주는 기능입니다. 다른점은 함수를 구현하는 것이 아니고 자동으로 만들어 져 있는 메서드랑 프로퍼티를 가져다 씁니다. 매우 편리하고 직관적이지만 문법이 좀 있습니다.

- 풋터에 합, 평균 등 계산값 정리해주기
  
먼저 그리드뷰의 옵션뷰를 참으로 설정해줘야 합니다.
```C#
this.gridview.OptionsView.ShowFooter = true; //이건 그룹핑 안했을때
this.gridview.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways; //이건 그룹핑 했을때
```
아래는 그룹핑이 아닐때 예제이다. 보통 온데이터 리트리브 할때 안에다 써주자 이유는 데이터가 바인딩 되어야 합계등 계산값도 바인딩 가능한 것이기 때문입니다. 아래 예제에 None, Sum 말고도 여러가지 기능이 있으니 홈페이지 참고 바랍니다.
```C#
//Summary.Add(DevExpress.Data.SummaryItemType.None 
this.gridview.Columns["SOMETHING_COL1"].Summary.Add(DevExpress.Data.SummaryItemType.None, "SOMETHING_COL1", "보여지는부분:");
//DevExpress.Data.SummaryItemType.Sum
this.gridview.Columns["SOMETHING_COL2"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SOMETHING_COL2", "포멧스트링사용가능:{0}");
```

마지막으로 정리하자면, 사용할 그룹핑 커스텀 셋팅 함수를 정의합니다. 그룹핑의 서머리도 있습니다.
```C#
private void SetGroup(GridControl gcControl, string group_col)
{
    gcControl.Columns[group_col].FieldNameSortGroup = group_col;
    gcControl.Columns[group_col].SortMode = DevExpress.XtraGrid.ColumnSortMode.Default;
    gcControl.Columns[group_col].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
    gcControl.Columns[group_col].Group();
    gcControl.DefaultView.ExpandAllGroups();
    gcControl.DefaultView.OptionsView.ShowFooter = true;
    gcControl.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
    gcControl.DefaultView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "SOME_COL", gcControl.Columns["SOME_COL"], "합계={0:n0}"); //그룹 서머리
    gcControl.Columns["SOME_COL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SOME_COL", "총합{0:n0}"); // 전체 서머리
}
```
<br>
<br>
<br>

- 3. Custom Summary 등록 및 CustomSummaryCalculate 이벤트 (동적컬럼)
[데브 익스프레스 CustomSummaryCalculate 튜토리얼](https://docs.devexpress.com/WindowsForms/114633/controls-and-libraries/data-grid/getting-started/walkthroughs/summaries/tutorial-custom-summary-functions?f=customsummarycalculate)
Summary를 커스터미이징 할 수도 있습니다. 그럼 좀더 자유로운 Summary를 구현할 수 있게 됩니다.

먼저 3가지 단계가 있습니다. 처음엔 GridControl의 속성을 바꿔줘야하고 다음엔 잠시 데이터를 저장할 변수를 필드에 선언해야 합니다. 이벤트를 등록해준 다음 이벤트 함수(콜백메서드 : CustomSummaryCalculate())를 작성합니다.

```
1. GridControl 속성 바꾸기 (뷰어단이 아닌 컨트롤단에서 코딩으로)
2. 데이터 저장변수 필드에 선언
3. 이벤트 등록 및 이벤트 작성
```

먼저 GridControl의 속성을 변경합니다.
```C#
gridview_Something.Column[index_Somthing].SummaryItem.SummaryType = SummaryItemType.Custom;
gridview_Something.Column[index_Somthing].SummaryItem.DisplayFormat = "{0:0.0}";
gridview_Something.Column[index_Somthing].SummaryItem.Tag = index_Somthing; //꼭 컬럼인덱스와 같은값을 줄 필요 없음 아무거나 줘도 됩니다.
```

다음으로는 임시로 데이터를 저장할 변수를 필드에! 선언해주어야합니다.
```C#
double totalPrice = 0;
```

마지막으로는 CustomSummaryCalculate 이벤트 메서드를 정의합니다. 메서드 안에는 총 3단계로 나눠서 작성해야합니다. Initialize, Calculate, Finalize단계가 있습니다. 아래 코드를 보면 CustomSummaryProcess 프로퍼티와 e.SummaryProcess 와 비교를 합니다. 이 과정을 지켜주어야 제대로 Custom서머리가 화면에 붙습니다. 

이벤트가 작동하는 원리에 대해서 생각을 해보겠습니다. 이벤트가 등록된 GridControl은 sender로서, 항상 같은값입니다. 문제는 아규먼트 e 값인데 이게 sender의 모든 셀을 훑습니다. 제 생각엔 이벤트의 포인터가 2개라는것입니다. 하나는 컬럼을 가리키고 하나는 셀을 가리킵니다. 컬럼포인터는 좌에서 우로 움직이고 셀포인터가 모든셀의 탐색을 마치면 다음 컬럼으로 움직입니다. 셀포인터는 자신의 로우에서 좌에서 우로 한번 탐색을 마치면 RowHandler를 하나 올리고 다음줄을 탐색합니다. (셀포인터는 컬럼의 개수만큼 모든셀을 탐색함을 알 수 있습니다. 전체컬럼개수 x 모든셀 개수 = 탐색수)

따라서 e.TotalValue는 하나의 컬럼에 대한 서머리값입니다~ 'ㅁ' ~~!!

아래 예시는 데브 홈페이지 예제와는 좀 다릅니다. 동적컬럼을 대비해서 생각해봤습니다.

```C#
private void DefaultView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
{
    //등록된 그리드컨트롤의 그리드뷰가 sender입니다.
    GridView view = sender as GridView;           
    // GridControl의 속성을 변경했을때 서머리 아이템에 태그를 부여했습니다. 
    // 그 특정 컬럼의 태그를 summaryID에 넣어줍니다. 이건 이벤트가 불려질때마다 수행됩니다.
    int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);

    //initialize 시작
    if (e.SummaryProcess == CustomSummaryProcess.Start)
    {
        totalPrice = 0;
    }

    //Calculate 계산
    if (e.SummaryProcess == CustomSummaryProcess.Calculate)
    {
        e.TotalValue = 0;
        //스위치문으로 해도 됩니다. 원래 코드는 for if else밖에 안되서 이렇게 했습니다.
        //여러가지 조건을 만들어낼 수 있습니다 여기가 커스터마이징 하는곳입니다.
        for (int i = 0; i < this.dataTable_Something.Columns.Count; i++)
        {
            if (summaryID == i)
            {
                //현재 포커싱된 셀(e)의 값을 totalPrice 필드변수에 임시로 저장합니다.
                totalPrice += Convert.ToDouble(e.FieldValue);
                
                ////커스터마이징 예시        
                // if(view.GetRowCellValue(e.RowHandle, "특정컬럼 이름").ToString() == Somthing_Value)
                // {
                //     totalPrice += Convert.ToDouble(e.FieldValue);
                // }
                // else
                // {
                //     totalPrice -= Convert.ToDouble(e.FieldValue);
                // }
            }
        }
    }

    // Finalization. 서머리 footer에 값을 붙여줍니다. 여기가 마지막입니다.
    if (e.SummaryProcess == CustomSummaryProcess.Finalize)
    {
        for (int i = 0; i < this.dataTable_Something.Columns.Count; i++)
        {
            if (summaryID == i)
            {
                e.TotalValue = totalPrice;
                break;
            }
        }
    }
}

```

_____________________________________________________________________

<br>

# 5. 그리드뷰의 종류 : 밴드그리드뷰 (BandedGridView) 

- BandedGridView 컬럼 Header 에 색깔주기

아래 코드는 좀 과한건데 그라데이션을 넣어줄 수 있는 코드입니다. 혹시해서 기록해둡니다.
출처 링크 : https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.BandedGrid.BandedGridView.CustomDrawBandHeader

```C#
private void bandedGridView1_CustomDrawBandHeader(object sender, DevExpress.XtraGrid.Views.BandedGrid.BandHeaderCustomDrawEventArgs e) {
    if (e.Band == null) return;
    if (e.Info.State != ObjectState.Pressed) return;
    using (Brush brushPressed = new LinearGradientBrush(e.Bounds, Color.WhiteSmoke, Color.Gray, LinearGradientMode.ForwardDiagonal)) {
        Rectangle r = e.Bounds;
        Draw3DBorder(e.Cache, r);
        r.Inflate(-1, -1);
        //Fill the background
        e.Cache.FillRectangle(brushPressed, r);

        //Draw a band glyph
        foreach (DrawElementInfo info in e.Info.InnerElements) {
            if (!info.Visible) continue;
            GlyphElementInfoArgs glyphInfoArgs = info.ElementInfo as GlyphElementInfoArgs;
            if (glyphInfoArgs == null) continue;
            info.ElementInfo.OffsetContent(1, 1);
            ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
            info.ElementInfo.OffsetContent(-1, -1);
            break;
        }

        //Draw the band's caption with a shadowed effect
        Rectangle textRect = e.Info.CaptionRect;
        textRect.Offset(2, 2);
        e.Appearance.DrawString(e.Cache, e.Info.Caption, textRect, Brushes.White);
        textRect.Offset(-1, -1);
        e.Appearance.DrawString(e.Cache, e.Info.Caption, textRect, Brushes.Black);

        //Prevent default painting
        e.Handled = true;
    }
}

private void Draw3DBorder(GraphicsCache cache, Rectangle rect) {
    //Draw a 3D border
    BorderPainter painter = BorderHelper.GetPainter(DevExpress.XtraEditors.Controls.BorderStyles.Style3D);
    AppearanceObject borderAppearance = new AppearanceObject();
    borderAppearance.BorderColor = Color.DarkGray;
    painter.DrawObject(new BorderObjectInfoArgs(cache, borderAppearance, rect));
}
```

____________________________________________________________________________________

<br>

# 6. 동적컬럼

#### 1) 바인딩 인포 자동맞춤
동적컬럼 바인딩 했을때 컬럼의 크기 ~DataModel에서 자동으로 맞춰주기는 방법으로는 인덱스값으로
길이에 8을 곱하고 40정도를 더하면됩니다. 맘에 안들면 조금씩 조정하세요.

```C#
this.BindingControlInfo.AddBand("", $"{i.ColumnName}", $"{i.ColumnName}", $"{i.ColumnName}", true, index++, true, true, ($"{i.ColumnName}".Length)*8+40, HAlignment.Center, false);
```
<br>
<br>
<br>

#### 2) 동적컬럼 바인딩 했을때 만약 ~DataModel의 바인딩인포가 readonly여서 색이 검정이면 바꿔주기

바인딩 인포의 AddBand던 Add던간에 readonly 가 true이면 화면의 선택은 못하고 클릭못하고 검정색일것입니다.
검정색을 디폴드 색으로 바꿔주는 방법입니다. RowCellStyle이벤트는 그리드뷰의 셀들의 스타일을 바꿔주는 이벤트임을
위에서 알았는데요, Color.Empty는 디폴트 색입니다.

```C#
private void SomethingGridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
{
    if (e.Column.OptionsColumn.ReadOnly)
        e.Appearance.BackColor = Color.Empty;
}
```
! 주의점 ! : ShowingEditor를 사용할 때 스위치문에서 아래와 같이 default: e.cancle = false;를 하면 안됩니다. 이유는 동적그리드가 바인드에 붙을때 readonly로 이미 되어
있는데 또 e.Cancle = false;해주면 이상하게 됩니다. 따라서 ShowingEditor도 고려해주세요.
```C#
private void Grid_Something_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
{
    switch (Grid_Something.DefaultView.FocusedColumn.FieldName)
    {
        case "CAR_NUMBER":
        case "PHONE_NUMBER":
        case "TON_CAR":
        case "FC_NAME":
            e.Cancel = true;
            break;
        default:
            e.Cancel = false;
            break;
    }
    
}
```
TextEdit컨트롤은 좀 다릅니다. 만약 텍스트박스를 클릭방지하면 연한 회색이 되므로 딱봐도 클릭방지임을 알 수는 있지만 디폴트색으로 바꿔줘야하는 상황이 있습니다.
아래는 디폴트색을 주는방법입니다. 그런데 디폴트처럼 보이게하려면 하양을 줘야합니다. 
```C#
private void Txt_setting(TextEdit txt)
{
    txt.Enabled = false;
    txt.ReadOnly = true;
    txt.Properties.AppearanceReadOnly.BackColor = Color.White;
}
```

<br>
<br>
<br>

#### 3) 동적컬럼 CU 저장할때 컬럼 잘라서 넘기기. 아래 두 메서드는 바로 위 DataSave() 에서 사용은 안했는데 사용할수도 있으니 참고바람. 출처는 MS Q&A

동적컬럼을 저장할때는 데이터 테이블의 커럼을 잘라서 넘길것입니다. 사용자 정의 테이블 형식은 정해진 컬럼들만 받을 수 있기 때문에 동적으로 생성된 커럶들은
잘라주어야 됩니다. 그렇지 않으면 오류를 냅니다. 근데 정말 주의해야할 것이 사용자 정의 테이블의 컬럼명들 순서와 테이블 데이터의 컬럼명들 순서가 같아야 한다는
것입니다.....'ㅁ'...!!! 그래서 테이블을 바꿔주던가 잘라줄때 잘 잘라주어야 합니다. 아래는 12번째 컬럼을 계속 잘라주는 방법입니다. 테이블 컬럼 사이가 아니라
뒤에 동적으로 컬럼들이 붙으면 이렇게 계속 똑같은 위치에 오는 컬럼을 잘라주는 방법을 씁니다.

```C#
int index = dataTable.Columns.Count;
for (int i = 12; i < index; i++)
{
    dataTable.Columns.RemoveAt(12);
}
```

참고하면 좋은 메서드들을 기록해둡니다. 출처는 MS Q&A

```C#
//데이터 테이블 컬럼이름을 받아서 잘라주는 메서드입니다. 
public static DataTable CutDataTableByString(DataTable dt, IList<string> columnsToRemove,
                            bool keepData = true)
{
    DataTable newDt = (keepData ? dt.Copy() : dt.Clone());
    foreach (string colName in columnsToRemove)
    {
        if (!newDt.Columns.Contains(colName)) continue;
        newDt.Columns.Remove(colName);
    }

    return newDt;
}
```
<br>

```C#
//데이터 테이블 인덱스로 자르는 방법입니다.
public static DataTable CutDataTableByIndex(DataTable dt, int index_count, bool keepData = true)
{
    DataTable newDt = (keepData ? dt.Copy() : dt.Clone());
    for (int i = 12; i < index_count; i++)
    {
        newDt.Columns.RemoveAt(12);
    }

    return newDt;
}
```
_______________________________________________________________________________________________

<br>

# 7. 그리드뷰의 로우를 위 아래로 옮겨주기(위아래 버튼만들기)

위아래 버튼권한을 줘야합니다. IS_EDIT. 포커스는 사실 구글에 검색할때는 selection이라고 검색해야 편할겁니다. selection 은 셀을 클릭하면 보라색 혹은 파란색
으로 바뀌면서 선택된 느낌이 들게 하는겁니다. 이게 왜 위아래로 옮기는 버튼을 만드는데 중요하냐면 선택한 로우가 위아래로 바뀔려면 포커스를 줘야하기 때문입니다.
먼저 버튼 리스너를 등록합니다.

```C#
button.Click += Btn_Click;
```
리스너를 정의 하구
```C#
private void Btn_Click(object sender, EventArgs e)
{
    var btn = sender as SimpleButton
    switch(btn.Text) 
    {
        case "Up":
            Btn_Up();
            break;
        case "Down":
            Btn_Down();
            break;   
    }
}
```

위아래니깐 버튼 두개 메서드를 만들어줍니다.

```C#
private void Btn_Up()
{
    if (this.DateEdit_SEQ.DateTime == DateTime.MinValue)
    {
        XMsgBx.ShowInfoOK("순서 변경 전, 도착일을 지정해야합니다.");
    }
    else
    {
        int index = ((BandedGridView)this.Grid_SEQ.DefaultView).FocusedRowHandle;
        if (index <= 0) return;

        DataRow row1 = ((BandedGridView)this.Grid_SEQ.DefaultView).GetDataRow(index);
        DataRow row2 = ((BandedGridView)this.Grid_SEQ.DefaultView).GetDataRow(index - 1);

        object[] val1 = row1.ItemArray;
        object[] val2 = row2.ItemArray;

        row1.ItemArray = val2;
        row2.ItemArray = val1;

        row1["SORT_NO"] = val1[0];
        row2["SORT_NO"] = val2[0];
        
        ColumnView cv = ((ColumnView)((BandedGridView)this.Grid_SEQ.DefaultView).GridControl.FocusedView);
        cv.MovePrev();
        cv.Focus();
    }
```

```C#
private void Btn_Down()
{
    if(this.DateEdit_SEQ.DateTime == DateTime.MinValue)
    {
        XMsgBx.ShowInfoOK("순서 변경 전, 도착일을 지정해야합니다.");
    }
    else
    {
        int index = ((BandedGridView)this.Grid_SEQ.DefaultView).FocusedRowHandle;
        GridColumn index_col = ((BandedGridView)this.Grid_SEQ.DefaultView).FocusedColumn;
        if (index >= ((BandedGridView)this.Grid_SEQ.DefaultView).DataRowCount - 1) return;

        DataRow row1 = ((BandedGridView)this.Grid_SEQ.DefaultView).GetDataRow(index);
        DataRow row2 = ((BandedGridView)this.Grid_SEQ.DefaultView).GetDataRow(index + 1);

        object[] val1 = row1.ItemArray;
        object[] val2 = row2.ItemArray;

        row1.ItemArray = val2;
        row2.ItemArray = val1;

        row1["SORT_NO"] = val1[0];
        row2["SORT_NO"] = val2[0];
        ColumnView cv = ((ColumnView)((BandedGridView)this.Grid_SEQ.DefaultView).GridControl.FocusedView);
        cv.MoveNext();
        cv.Focus();
    }
}
```
______________________________________________________________________________________________________

<br>

# 8. 로우 추가할때 순서대로 번호든 문자든 주기

먼저 방식이 2가지가 있습니다. 그리드뷰 컬럼에 NO.라는 컬럼을 줄때일것 같은데 sql 프로시저에서 관리하거나
아니면 직접 C#코드에서 데이터로우를 추가할때마다 새로 생성하는 방식을 코딩하는것입니다. sql에서 관리한다면
IDENTITY 속성 을 테이블 컬럼에 부여해야하는데 테이블 수정을 원하지 않는다면 C#단에서 해결하는것이 좋습니다. 
<br>
먼저 SQL 에서 가져올때부터 저장이 먼저된순으로 부여된 IDENTITY값을 가져올수 있게 테이블을 만드는법을 
살펴보겠습니다.

```SQL
CREATE TABLE dbo.Something(
    SOMETHING_ID INT IDENTITY(1,1),
    BEGINDATE DATE NULL,
    ENDDATE DATE NULL,
    SOMETHING_COL VARCHAR(100) NULL
)
GO
```

데이터를 추가해 보겠습니다.

```SQL
INSERT INTO dbo.SOMETHING(
    , BEGINDATE
    , ENDDATE
    , SOMETHING_COL)
    VALUES('2021-10-27', '2021-10-28', 'HELLO_SQL')


INSERT INTO dbo.SOMETHING(
    , BEGINDATE
    , ENDDATE
    , SOMETHING_COL)
    VALUES('2021-10-28', '2021-10-29', 'BYE_SQL')
```
그러면 테이블은 이런식이 될것입니다.
||SOMETHING_ID|BEGINDATE|ENDDATE|SOMETHING_COL|
|---|---|---|---|---|
|1|1|2021-10-27|2021-10-28|HELLO_SQL|
|2|2|2021-10-28|2021-10-29|BYE_SQL|

그런데 IDENTITY의 속성열이 같는 특징은 이전행을 지워도 지워진값이 채워지는것이 아닌 그냥 다음것이 쭉 증가되는값이
계속 저장된다는것입니다. 해결방법은 나중에 설명을 쓰겠습니다(추후 수저예정)
<br>
이제 C#단에서 해결하는방법입니다. 데이터모델단에서 추가해줘야합니다.

```C# 
//먼저 사용중인 테이블의 모양이 같은 로우객체를 생성합니다.
DataRow row = this.somethingtable.NewRow();
```

그리고 생성한 row 객체에 원하는 스트링포멧을 주고 데이터를 넣어줍니다. 

```C#
~Model add = ~Model.CreateInstance(row);
add.SOMETHINGCOLUMN = string.Format(DateTime.Now.Year.ToString().Substring(2), (property + 1).ToString("0000"));

```

마지막으로 row객체를 데이터테이블에 넣어줍니다.

```C#
this.somethingtable.Rows.Add(row);
```
______________________________________________________________________________________________________

<br>

# 9. 정규식으로 컬럼에 정해진값만 작성할 수 있게 만들어주기
정규표현식 참고 블로그 https://jacking75.github.io/csharp_RegularExpression/
<br>
ColumnView.CustomColumnSort 이벤트에 대해서 알아보겠습니다.
원래는 커스텀 컬럼소트는 어떠한 컬럼에 숫자들이 있을때 정렬을 도와주는 이벤트입니다.
왜 이게 필요하냐면, 갸령 3, 10, 1 숫자가 있다고 합시다. 그러면 문자열로 비교되어 소팅되기 때문에
1, 10, 3 이런식으로 리턴받게 됩니다. 1, 3, 10 이런식으로 만들어주기 위해 CustomColumSort가 있습니다.
사용하기에 앞서서 먼저 그리드 컨트롤의 소팅할 컬럼에 이벤트를 등록하고, 컬럼의 소팅모드를 커스텀으로 바꿔줘야합니다.

```C#
gridControl.gridview.CustomColumnSort += GridView_CustomColumnSort;
gridControl.Columns["SOMETHING_COL"].SortMode = Devexpress.XtraGrid.ColumnSortMode.Custom;
```

모드를 추가했으면 이벤트를 작성해줍니다. 아래는 정규표현식을 사용해서 소팅합니다. 이러면 아래에 정규식으로 패턴을 부여했는데
부여한 패턴만 화면에 붙습니다.

```C#
private void GridView_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
{
    string pattern = @"^[a-zA-Z0-9가-힣]*$";
    if ((Regex.IsMatch(e.Value2.ToString(), pattern)) && (Regex.IsMatch(e.Value1.ToString(), pattern)))
    {
        try
        {
            int value1 = Convert.ToInt32(e.Value1);
            int value2 = Convert.ToInt32(e.Value2);
            e.Result = value1.CompareTo(value2); //new line  
            e.Handled = true; //new line  
        }
        catch { }
    }
}
```

<br>
<br>
<br>

#### 정규식: C# 에서 sql처럼 like형식으로 사용하는 방법 이유 -> like방식이 syntax가 아주 쉽기 때문이다.

참고 사이트입니다. https://stackoverflow.com/questions/19582256/unable-to-cast-object-of-type-system-int32-to-type-system-string-in-dataread

이건 스테틱이 아닌 그저 프라이빗 함수입니다. 스태틱으로 하면 클래스를 새로만들어줘야하니 위 참고링크를 따라가세요. 아래 메서드는 C# 에서 마치 sql의 like를 정규식을 사용해서 쓸수 있게 만들어줍니다. 유용합니다.

```C#
 private bool Like(string toSearch, string toFind)
{
    return new Regex(
        @"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\")
        .Replace(toFind, ch => @"\" + ch)
        .Replace('_', '.')
        .Replace("%", ".*") + @"\z", RegexOptions.Singleline
        ).IsMatch(toSearch);
}

```

아래는 사용 예시입니다.

```C#
 if(
    Like((string)a.ToString(), "1%") ||
    Like((string)a.ToString(), "2%") ||
    Like((string)a.ToString(), "3%") ||
    Like((string)a.ToString(), "4%") ||
    Like((string)a.ToString(), "5%") 
    )
{
    a = "M" + a;
}
```
______________________________________________________________________________________________________

<br>

# 10. 그리드뷰 필터에 관해서

참고링크 데브익스프레스 명세 : https://docs.devexpress.com/WindowsForms/1428/controls-and-libraries/data-grid/visual-elements/grid-view-elements/auto-filter-row

<br>

#### 1). 필터를 안보이게 하는법

아래는 그리드뷰에 필터를 없애는 코드입니다.

```C#
this.GridControl.OptionsView.ShowAutoFilterRow = false;
```

#### 2). 필터가 BOOLEAN 일때 기본 디폴트값을 FALSE로 주는 방법 (기본은 NULL인듯 하다)

필터가 Boolean일때는 디폴트로 null이 들어가있습니다. 즉 검정색 네모가 조그만 하양네모를 채우고있는.. 이것이 false 인지 null인지 true 인지 정말
알기 힘듭니다. 따라서 sql에서 셀에 데이터가 null로 들어가있는것도 표현해주기 위함인듯 합니다. 그런데 사용자는 null이 false와 어짜피 같다고 생각하기
때문에 필터의 디폴트를 바꿔줄 필요가 있습니다.

```C#
this.GridControl.gridview.SetAutoFilterValue(GridControl.Columns["컬럼이름"], false, AutoFilterCondition.Default);
```

그리고 그리드 컨트롤에 boolean null값이 들어가있는걸 보여주면 안됩니다. (입력하지 않았으면 상황상 false임)
따라서 sql단에서 보내줄때 boolean null은 false로 보내줍시다.

```SQL
SELECT 
        ISNULL(SOMETHING_BOOLEAN, 0)
    FROM SOMETHING_TABLE
```

______________________________________________________________________________________________________

<br>

# 11. 그리드 컨트롤 화면단에 불필요한 0 이 있을때 안보이게 하는 방법

CustomColumnDisplayText 이벤트를 사용합니다.

먼저 이벤트를 등록합니다.

```C#
some_gridControl.CustomColumnDisplayText += Some_gridControl_CustomColumnDisplayText;
```

이벤트를 등록했으면 아래와 같이 이벤트를 정의해줍니다.

```C#
private void Some_gridControl_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
{
    if(e.Value != null && e.Value != DBNull.Value)
    {
        if (e.Value.GetType() != typeof(string) && e.Value.GetType() != typeof(int))
        {
            if ((decimal)e.Value == 0)
            {
                e.DisplayText = "";
            }
        }
    }
}
```
______________________________________________________________________________________________________

<br>

# 12. 그리드 컨트롤 CheckBoxSelector(그리드뷰에 체크박스를 넣어줘서 선택하게 만듬)

먼저 셋팅을 해줍니다.
```C#
this.GridContol.OptionsSelection.MultiSelect = true;
this.GridContol.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
this.GridContol.OptionsSelection.ShowCheckBoxSelectorInGroupRow = DevExpress.Utils.DefaultBoolean.True; //이건 그룹핑할때 쓰는것.. 꼭 필요하지는 않음
```

그리고 만약 체크박스를 클릭했을때 필드값에 저장해야하므로 필드명을 주고싶을땐 아래코드를 추가합니다.

```C#
this.GridContol.OptionsSelection.CheckBoxSelectorField = "SOMETHING_SELLECTED"; 
```

#### 1). 팝업창에서 그리드 컨트롤 CheckBoxSelector 으로 체크한 것들 메인 그리드에 저장해주기

먼저 유의해야할것이 팝업창은 Menuitem이 없으니깐 이벤트 핸들러로 메인뷰의 Menuitem을 불러서 붙여줘야합니다.

컨트롤러의 OnDataSave() 메서드와 ~DataList의 OnDataSave() 를 만들어줘야합니다. 팝업이니깐 ~DataList에 
기존의 OnDataSave()가 있을테니 이름을 잘 바꿔주세요

컨트롤의 OnDataSave 가 DataList 의 OnDataSave을 호출하는 방식입니다.
______________________________________________________________________________________________________

<br>

# 13. 그리드뷰의 특정 컬럼을 수정하면 전체 데이터가 수정됨( ex: 모든 로우셀이 boolean체크박스인데 하나 체크하고 다른거 체크하면 전체가 풀려야함)

아래 코드를 보면 그리드뷰의 SetRowCellValue메서드를 사용했습니다. row는 로우 핸들러이고, 두번째 파라미터값으로 그리드컨트롤의 컬럼을 주어야합니다. 세번째 파라미터값으로는 그 컬럼에 주고싶은 값을 넣어주면 됩니다. 결국 그리드컨트롤의 모든 로우셀에 값을 주려면 그리드컨트롤의 처음 인자부터 마지막까지 foreach 문으로 읽어서 하나씩 넣어줘야합니다. 따로 메서드가 있는건 아닙니다. 아래 예시는 값으로 false를 주었는데 그리드컨트롤의 셀 형식이 boolean이었기 때문입니다. 여튼 아래와같이 코딩해주세요.

```C#
private void Grid_Something_CellValueChanging(object sender, CellValueChangedEventArgs e)
{
    ISOMETHINGModel item = this.Grid_Something.GetFocusedRow<ISOMETHINGModel>();
    if(e.Column.FieldName == "SOMETHING_COL_NAME")
    {
        if (Grid_Something.SelectedRowsCount > 0)
            foreach (int row in Enumerable.Range(0, this.Grid_Something.DefaultView.RowCount))
            {
                this.Grid_Something.DefaultView.SetRowCellValue(row, Grid_Something.Columns["SOMETHING_COL_NAME"], false);
            }
    }
}
```
______________________________________________________________________________________________________

<br>

# 14. 컨트롤단에서 밴디드그리드뷰던 그냥 그리드뷰던 visible false주기

사실 .Visible값을 false로 주면 보이지 않는 간단한 코드입니다.

```C#
((BandedGridView)Grid_Something.DefaultView).Bands["SOMETHING_COL_NAME"].Visible = false;
```

그런데 아래와 같이 밴디드뷰는 부모컬럼 아래 자식컬럼이 있습니다. 따라서 자식컬럼을 보이지 않게하고 싶다면 아래와같이 명시적으로 할당해주어야 합니다. 기억해주세요.

```C#
((BandedGridView)Grid_Something.DefaultView).Bands["SOMETHING_COL_NAME"].Children["SOMETHING_CHILDREN_COL_NAME"].Visible = false;
```

______________________________________________________________________________________________________



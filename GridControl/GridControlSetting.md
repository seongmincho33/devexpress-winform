# 1. GridControl
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
_________________________________________________________________________
#### 1. 컬럼 클릭 방지

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
#### 2. 셀값 바뀌면 다른셀값도 세팅해주는 방법
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
```
위의 코드를 보면 선택한 그리드뷰의 로우를 GetFocusedRow\<T>() 메서드를 사용해서 item에 붙여주었다. 
```C#
if(e.Column.FieldName == "SOMETHING_ID")
```
여기 if문에서 파라미터 e는 바뀐 셀값의 컬럼을 리턴한다. 
________________________________________________________________________________________________
#### 3. 셀의 크기와 컬럼헤더(밴디드뷰면 밴드 헤더) 크기 셀의 색을 바꿔줄 수 있다.
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

_____________________________________________________________________________________
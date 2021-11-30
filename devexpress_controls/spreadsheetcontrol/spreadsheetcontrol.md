# SpreadSheetControl (DevExpress.XtraSpreadsheet.SpreadsheetControl)

<img src="../../img/spreadsheetcontrol_img/spreadsheetcontrol_example001.png" width="800" heigth="800" />

<br />

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

_________________________________________________________________________
<br>
<br>
<br>

# 1. WorkBook과 WorkSheet
스프레드시트 컨트롤의 도큐먼트 가 워크북이고 워크북 안에 여러개의 워크시트가 있는 개념입니다. 마치 엑셀에서 시트 하나에 여러개의 탭이 있는것과 같은것입니다. 
_________________________________________________________________________
<br>
<br>
<br>

# 2. SpreadSheetControl의 BeginUpdate()와 EndUpdate()

먼저 BeginUpdate()는 스프레드시트 컨트롤의 시각적 업데이트를 막습니다. EndUpdate()가 호출되기 전까지는 시각적 업데이트를 막아줘야합니다. 이렇게 감싸주게 된다면 여러가지 수행을 하는 스프레드시트의 성능을 올려줄 수 있습니다(속도가 좋아짐).

```C#
this.SpreadsheetControl_Something.BeginUpdate();

//스프레드 시트 컨트롤 내용및 수정입력

this.SpreadsheetControl_Something.EndUpdate();
```

또는 아래와 같이 EndUpdate()문을 try finally문으로 감싸주어서 exception 에러가 나와도 실행되게끔 만들어 줄 수 있습니다.

```C#
this.SpreadsheetControl_Something.BeginUpdate();
try
{
    //스프레드 시트 컨트롤 내용및 수정입력
}
finally 
{
    this.SpreadsheetControl_Something.EndUpdate();
}
```

아래 코드는 하나의 워크북에 하나의 워크시트를 가지고 어떤 작업을 할때 셋팅해주는 코드입니다. 

```C#
this.SpreadsheetControl_Something.BeginUpdate();
IWorkbook workbook_Something = this.SpreadsheetControl_Something.Document;
workbook_Something.Unit = DevExpress.Office.DocumentUnit.Millimeter;
int count = workbook_Something.Worksheets.Count;
for (int i = count - 1; i >= 1; i--)
{
    workbook_Something.Worksheets.RemoveAt(i);
}
Worksheet worksheet_Lease = workbook_Something.Worksheets[0];

//워크시트 내용 및 수정 입력

this.SpreadsheetControl_Something.EndUpdate();
```

위 내용들은 SpreadSheetControl에 대해서만 말하고있습니다. WorkBook도 마찬가지로 BeginUpdate()와 EndUpdate()가 있습니다.
_________________________________________________________________________
<br>
<br>
<br>

# 3. 위크시트의 범위를 정하고 값과 셀크기등을 조절하는 방법 (FromLTRB)

워크시트의 내용을 수정하고 채울때 셀의 범위를 정해주어야 합니다(행:row, 열:column). 이걸 정해주는 방법은 2가지 정도가 있습니다. 처음으로는 Cell 프로퍼티를 사용하는 방법입니다. Cell.Value프로퍼티에는 값을 할당할 수 있습니다. 이게 워크시트에 무언가를 쓰는 방법입니다. A1은 엑셀처럼 처음값은 열의 알파벳이고 뒤에 숫자는 행의 로우 핸들러 번호입니다.

```C#
worksheet_Something.Cells["A1"].Value = "Hello, World!";
```

범위를 주어서 값을 여러개 할당할 수도 있습니다.

```C#
worksheet.Range["B10:E10"].Value = "Hello, World!";;
```

두번째로는 FromLTRB()메서드를 사용하는것입니다. 이는 아래와 같이 4개의 인자값을 받습니다. 주의할 점은 워크시트에서 Range를 타고 써야하는것입니다. 이 방법이 편할 수도 있습니다. 이유는 범위를 4개로 주어져서 동적컬럼을 만들때는 수월합니다. 저는 이 방식을 선호하는편입니다.

```C#
CellRange rangeFromLTRB = worksheet_Something.Range.FromLTRB(시작 열, 시작 행[top], 끝 열, 끝 행[bottom]);
```

아래 코드는 또다른 방식들입니다. 여러가지 방법으로 셀에 값을 할당할 수 있습니다.

```C#
//출처 데브익스프레스
using DevExpress.Spreadsheet;
// ...

Workbook workbook = new Workbook();
Worksheet worksheet = workbook.Worksheets[0];

// A range that includes cells from the top left cell (A1) to the bottom right cell (B5).
CellRange rangeA1B5 = worksheet["A1:B5"];

// A rectangular range that includes cells from the top left cell (C4) to the bottom right cell (E7).
CellRange rangeC4E7 = worksheet.Range["C4:E7"];

// The C4:E7 cell range located in the "Sheet3" worksheet.
CellRange rangeSheet3C4E7 = workbook.Range["Sheet3!C4:E7"];

// A range that contains a single cell (E7).
CellRange rangeE7 = worksheet.Range["E7"];

// A range that includes the entire column A.
CellRange rangeColumnA = worksheet.Range["A:A"];

// A range that includes the entire row 5.
CellRange rangeRow5 = worksheet.Range["5:5"];

// A minimal rectangular range that includes all listed cells: C6, D9 and E7.
CellRange rangeC6D9E7 = worksheet.Range.Parse("C6:D9:E7");

// A rectangular range whose left column index is 0, top row index is 0, 
// right column index is 3 and bottom row index is 2. This is the A1:D3 cell range.
CellRange rangeA1D3 = worksheet.Range.FromLTRB(0, 0, 3, 2);

// A range that includes the intersection of two ranges: C5:E10 and E9:G13. 
// This is the E9:E10 cell range.
CellRange rangeE9E10 = worksheet.Range["C5:E10 E9:G13"];

// Create a defined name for the D20:G23 cell range.
worksheet.DefinedNames.Add("Range_Name", "Sheet1!$D$20:$G$23");
// Access a range by its defined name.
CellRange rangeD20G23 = worksheet.Range["Range_Name"];

CellRange rangeA1D4 = worksheet["A1:D4"];
CellRange rangeD5E7 = worksheet["D5:E7"];
CellRange rangeRow11 = worksheet["11:11"];
CellRange rangeF7 = worksheet["F7"];

// Create a complex range via the Range.Union method.
CellRange complexRange1 = worksheet["B7:C9"].Union(rangeD5E7);

// Create a complex range via the IRangeProvider.Union method.
CellRange complexRange2 = worksheet.Range.Union(new CellRange[] { rangeRow11, rangeA1D4, rangeF7 });

// Create a complex range from multiple cell ranges separated by commas.
CellRange complexRange3 = worksheet["D15:F18, G19:H20, I21"];

// Fill the ranges with different colors.
complexRange1.FillColor = Color.LightBlue;
complexRange2.FillColor = Color.LightGreen;
complexRange3.FillColor = Color.LightPink;

// Use the Areas property to get access to a complex range's component.
complexRange2.Areas[2].Borders.SetOutsideBorders(Color.DarkGreen, BorderLineStyle.Medium);
```

<br />
<br />

## 값 할당

<br />
<br />

### 자동변환

<br />

워크시트의 셀에 값을 넣어줄때 SetValueFromText를 사용하면 값을 자동으로 형식을 변환해서 셀에 넣어줍니다. 반대로 NumberFormat프로퍼티처럼 이런값에 형식을 미리 지정해줄 수도 있습니다.

```C#
// 출처 데브익스프레스
// Add data of different types to cells.
worksheet.Cells["B1"].SetValueFromText("28-Jul-20 5:43PM"); // DateTime
worksheet.Cells["B2"].SetValueFromText("3.1415926536"); // double
worksheet.Cells["B3"].SetValueFromText("Have a nice day!"); // string
worksheet.Cells["B4"].SetValueFromText("#REF!"); // error
worksheet.Cells["B5"].SetValueFromText("true"); // Boolean
worksheet.Cells["B6"].SetValueFromText("3.40282E+38"); // float
worksheet.Cells["B7"].SetValueFromText("2147483647"); // int32
worksheet.Cells["B8"].NumberFormat = "d-mmm-yy h:mm";
worksheet.Cells["B8"].SetValueFromText("28-Jul-20 5:43PM", true); // DateTime with a custom format
worksheet.Cells["B9"].SetValueFromText("=SQRT(25)"); // formula
```

### 함수 할당

<br />

아래와 같이 셀 범위에 Formula 프로퍼티에 스트링값으로 함수를 작성해서 할당해주면 엑셀과 같은 함수의 효과를 볼 수 있습니다. 여러가지 함수 작성법이 있는데 이는 Devexpress의 명세를 참고해서 만들면 될것 같습니다. 

[참고링크1 nested formulas](https://docs.devexpress.com/WindowsForms/15410/controls-and-libraries/spreadsheet/examples/formulas/how-to-use-functions-and-nested-functions-in-formulas)

[참고링크2 formulas syntax](https://docs.devexpress.com/WindowsForms/13811/controls-and-libraries/spreadsheet/spreadsheet-formulas)
```C#
private void SetColumnSum(Worksheet worksheet, int col_set, int row_set, int row_start, int row_end)
{
    //row_start 부터 row_end까지 위에서부터 아래로 값을 더합니다.
    worksheet.Cells[row_set, col_set].Formula =
            "=SUM ("
            + worksheet.Cells[row_start, col_set].GetReferenceA1()
            + ":"
            + worksheet.Cells[row_end, col_set].GetReferenceA1()
            + ")";
}
```

<br />
<br />

## 셀 형식

<br />
<br />

### 셀 보더

<br />

아래와같이 Border.SetAllBorders() 메서드를 사용합니다. 두개의 인자를 받습니다. 처음은 색이고 두번째는 보더의 스타일입니다. 

```C#
worksheet.Range.FromLTRB(col_left, top_row, col_right, bottom_row).Borders.SetAllBorders(Color.Black, BorderLineStyle.Thin);
```

### 셀 사이즈

<br />

셀 사이즈는 RowHeight 와 ColumnWidth 프로퍼티에 값을 할당해서 조절합니다. RowHeigth는 셀의 높이고, ColumnWidth는 셀의 너비 입니다.

```C#
worksheet.Range.FromLTRB(col_left, top_row, col_right, bottom_row).RowHeigth = 10; //셀 높이 10으로 할당
worksheet.Range.FromLTRB(col_left, top_row, col_right, bottom_row).ColumnWidth = 10; //셀 너비 10으로 할당
```

### 셀 폰트 사이즈

<br />

셀 폰트사이즈는 말 그대로 셀 안의 값의 크기 입니다. 셀 사이즈랑 셀 폰트사이즈랑 다르니 유의해야 합니다. Font.Size프로퍼티에 값을 할당하면 폰트크기를 조절할 수 있습니다. Font.Bold프로퍼티에 boolean값을 할당하면 볼드체를 줄수도 있습니다.

```C#
worksheet.Range.FromLTRB(col_left, top_row, col_right, bottom_row).Font.Size = 10;
worksheet.Range.FromLTRB(col_left, top_row, col_right, bottom_row).Font.Bold = true;
```

### 셀 Alignment

<br />

셀의 안의 값의 Alignment값을 주는 건 Alignment.Vertical 프로퍼티와 Alignment.Horizontal 프로퍼티 입니다. 근데 주는 값으로는 좌우 중심 위아래 가 있을텐데 이 속성은 SpreadsheetVertivalAlignment와 SpreadsheetHorizontalAlignment 의 속성으로부터 불러와야 합니다. 임의로 Center나 뭐 그런걸 할당하면 먹지 않습니다. 

```C#
worksheet.Range.FromLTRB(col_left, top_row, col_right, bottom_row).Alignment.Vertical = SpreadsheetVerticalAlignment.
Center
worksheet.Range.FromLTRB(col_left, top_row, col_right, bottom_row).Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center
```

_________________________________________________________________________
<br>
<br>
<br>

# 4. 셀 머징(Merge)

셀을 병합하는 방법입니다. 아래는 메서드로 만들어보았습니다. 인자값으로는 워크시트, 첫번째 컬럼, 탑 로우, 컬럼스팬, 로우스팬 값을 입력 받습니다. 메서드는 만들기 나름이지만 고려해야하는 사항들이 있습니다. 이는 아래 8번 '고려해야하는 사항'에서 설명하겠습니다.

```C#
public bool CellMerge(Worksheet worksheet, int first_column, int top_row, int colSpan = 0, int rowSpan = 0)
{
    bool isResult = true;
    if (rowSpan == 0 && colSpan == 0)
        return true;
    try
    {
        if (rowSpan != 0 || colSpan != 0)
        {
            Range range = worksheet.Range.FromLTRB(first_column, top_row, colIndex + colSpan, rowIndex + rowSpan);
            worksheet.ActiveView.ShowGridlines = true;
            worksheet.MergeCells(range);
        }
    }
    catch
    {
        isResult = false;
    }
    return isResult;
}
```
_________________________________________________________________________
<br>
<br>
<br>

# 5. 데이터 테이블을 워크시트에 붙여주는 방법

워크시트에 Import 메서드를 사용합니다. 아래와 같이 사용하면 됩니다. 

```C#
 worksheet_Something.Import(dataTable_Something, true, first_row, first_column);  
```
_________________________________________________________________________
<br>
<br>
<br>

# 6. 컬럼 더하기 기능 만들기

```C#
private void SetColumnSum(Worksheet worksheet, int col_set, int row_set, int row_start, int row_end)
{
//row_start 부터 row_end까지 위에서부터 아래로 값을 더합니다.
worksheet.Cells[row_set, col_set].Formula =
        "=SUM ("
        + worksheet.Cells[row_start, col_set].GetReferenceA1()
        + ":"
        + worksheet.Cells[row_end, col_set].GetReferenceA1()
        + ")";
}
```
_________________________________________________________________________
<br>
<br>
<br>

# 7. 스프레드 시트의 모든 0들을 지워주기

아래 코드는 SpreadSheetControl과 WorkBook의 BeginUpdate() 와 EndUpdate() 사이에 넣어주시면됩니다. 

```C#
worksheet_Something.ActiveView.ShowZeroValues = false;//모든 0들을 지워줍니다.
```

_________________________________________________________________________
<br>
<br>
<br>

# 8. 동적컬럼 테이블 붙일때 고려사항
먼저 SetOrdinal() 메서드에 대해서 알아두어야 합니다. 이유는 동적컬럼가지고 있는 데이터 테이블을 스프레드시트에 붙여주어야 하는데 그대로 붙여준다면 정말 편하고 좋겠지만 현실은 여기있어야 할 컬럼이 끝에 있고 끝에있는컬럼은 앞으로 가야하고 뭐 이런식이라서 컬럼도 명시적으로 옮겨주어야합니다. 

또한 고려해야할 사항은 동적컬럼이 BandedGridview일때 입니다. 이러면 컬럼 부모는 셀 병합을 해주어야하고 자식들 컬럼들은 따로 분리해서 List<DataColumn>형식으로 따로 객체를 생성하는것이 좋은것 같습니다. 아래는 예시입니다. 받아오는 데이터테이블은 컬럼이름이 sql에서 정의한대로거나 마음대로일텐데 이를 for 문이나 foreach문을 사용해서 Regex(정규식)을 통해 좀 나눠줄 필요가 있습니다. 

```C#
//동적 컬럼 이름 바꿔주기: 중복되는 이름을 허용해야하므로 caption을 사용합니다. 
string pattern = @"[[0-9]년|[0-9]월]";
string pattern2 = @"[Q]";
string pattern3 = @"[P]";
List<DataColumn> col_qty = new List<DataColumn>();
List<DataColumn> col_price = new List<DataColumn>();
List<DataColumn> col_total = new List<DataColumn>();
foreach (DataColumn col in dt_main.Columns)
{
    if (Regex.IsMatch(col.ColumnName, pattern))
    {
        if (Regex.IsMatch(col.ColumnName, pattern2))
        {
            //QTY
            col.Caption = "수량";
            col_qty.Add(col);
        }
        else if ((Regex.IsMatch(col.ColumnName, pattern3)))
        {
            //PRICE
            col.Caption = "금액";
            col_price.Add(col);
        }
        else
        {
            //QTY, PRICE 합계
            col.Caption = "합계";
            col_total.Add(col);
        }
    }
}
```

근데 문제는 동적컬럼을 다 셀에 셋팅해주고 SetOrdinal을 시전하면 문제가 됩니다. SetOrdinal은 데이터 테이블의 컬럼 인덱스를 바꿔주는 것입니다. Cell과는 전혀 상관이 없습니다. 따라서 SetOrdinal로 데이터 테이블의 컬럼들을 미리 정리 한 뒤에 foreach문으로 셀에 붙여주시면 됩니다. 
_________________________________________________________________________
<br>
<br>
<br>

# 9. 이름이 애매한 컬럼의 전체 로우들을 색칠해주기

컬럼 이름이 애매하면 Regex(정규식)으로 맞춤으로 찾아서 범위를 선택합니다. 셀에 색을 할당받는 파라미터는 FillColor입니다.

```C#
 //선택한 연월이 마지막 연월과 동일하다면 노란색으로 색칠해 줍니다.
string date_to_yellow = Regex.Replace(worksheet_Something.Range.FromLTRB(???, ???, ???, ???).Value.ToString(), @"\s", "");
string compare_date = Regex.Replace($"{this.Date_Year_Month.Date.Year.ToString()}년 {this.Date_Year_Month.Date.Month.ToString()}월", @"\s", "");
if (String.Equals(date_to_yellow, compare_date, StringComparison.OrdinalIgnoreCase))
{
    worksheet_Something.Range.FromLTRB(???, ???, ???, ???).FillColor = Color.Yellow; // 색 할당
}
```
_________________________________________________________________________
<br>
<br>
<br>

# 10. 워크시트의 위치에 대한 변수를 고려해야할 사항들에 관해서.
일단 모든 데이터가 화면의 (0, 0) 에 서부터 시작해서 보여지는것은 아닙니다. 어떤 테이블은 저기있고 어떤 테이블은 여기있고 항상 위치는 달라질 수 있고 수정될수 있습니다. 그때그때 바꿔줘야하는 테이블도 있습니다. 동적테이블은 데이터의 개수에 따라서 컬럼이 늘어나는 피벗테이블입니다. 만약 피벗테이블 옆에 다른 테이블을 놓았다고 한다면 서로 겹칠수도 있습니다. 위치선정을 변수로 주어야 하는데 전역은 지양해야합니다. 

또한 변수를 파라미터값으로 넘길때 규칙을 정하는편이 이롭습니다. 별거 아니지만 메서드를 만들때 고려해야할 사항 두가지를 생각해 보았습니다.

1. 메서드(자작) 파라미터 순서
2. colspan, rowspan 사용여부

예를들어 1번을 설명하면, 아래와 같은 워크시트의 셀에 함수를 넣어본다고 가정하겠습니다. 함수가중요한게 아니고 Cell프로퍼티의 처음 인자는 로우를 받고 다음에 컬럼값을 받는다는것입니다. 

```C#
worksheet.Cells[row_set, col_set].Formula =
                    "=SUM ("
                    + worksheet.Cells[row_start, col_set].GetReferenceA1()
                    + ":"
                    + worksheet.Cells[row_end, col_set].GetReferenceA1()
                    + ")";
```

반면 아래의 FromLTRB메서드를 사용했다고 가정하면 처음인자로 컬럼을 주고 두번째로 로우값을 줍니다. 이렇게 메서드 마다 처음과 나중에 받는 인자의 값이 순서가 다릅니다. 만약 개인이 만든 메서드가 규칙을 정하지 않는다면 줄때마다 메서드파라미터 순서를 외우거나 일일히 확인해야합니다. 이렇게 devexpress 메서드는 파라미터 순서를 바꿀 순 없지만 개인이 만든 메서드는 파라미터 순서를 통일해서 만들어야합니다. 

```C#
worksheet.Range.FromLTRB(first_col, top_row, first_col, bottom_row).Formula =
                    "=SUM ("
                    + worksheet.Range.FromLTRB(first_col, top_row, first_col, bottom_row).GetReferenceA1()
                    + ":"
                    + worksheet.Range.FromLTRB(first_col, top_row, first_col, bottom_row).GetReferenceA1()
                    + ")";
```

2번의 rowspan과 colspan에 대해서 입니다. 아래 코드를 보면 자작메서드를 만들때 colspan과 rowspan을 받습니다. 이 방식은 처음받는 파라미터 두개는 시작셀에 관한 위치값이고 span은 늘려주기위한 파라미터입니다. 

```C#
public bool CellMerge(Worksheet worksheet, int first_column, int top_row, int colSpan = 0, int rowSpan = 0)
{
    bool isResult = true;
    if (rowSpan == 0 && colSpan == 0)
        return true;
    try
    {
        if (rowSpan != 0 || colSpan != 0)
        {
            Range range = worksheet.Range.FromLTRB(first_column, top_row, colIndex + colSpan, rowIndex + rowSpan);
            worksheet.ActiveView.ShowGridlines = true;
            worksheet.MergeCells(range);
        }
    }
    catch
    {
        isResult = false;
    }
    return isResult;
}
```

아래와 같이 만들때 span이 아니라 명시적으로 끝나느 셀의 위치값을 넣어주게 만들 수도 있습니다. 방법은 중요하지 않지만 위 두 규칙을 일관성있게 지키는것이 나중에 헷갈리지 않을 수 있습니다. 

```C#
public bool CellMerge(Worksheet worksheet, int first_column, int top_row, int end_col, int bottom_row)
{
    bool isResult = true;
    if (first_column < end_col && top_row > bottom_row)
        return true;
    try
    {
        Range range = worksheet.Range.FromLTRB(first_column, top_row, end_col, bottom_row);
        worksheet.ActiveView.ShowGridlines = true;
        worksheet.MergeCells(range);
    }
    catch
    {
        isResult = false;
    }
    return isResult;
}
```
_________________________________________________________________________
<br>
<br>
<br>

# 11. RibbonControl
Devexpress 18.2 는 지원하는것으로 나오지만 참조를 걸어도 안뜰때가 있습니다. 이럴땐 UI뷰어로 가서 강제로 붙여주고 속성으로 스프레드시트를 선택하는 부분이 생길수도~ 안생길수도~ 있는데 알잘딱으로 다가 감으로 붙이세요. 그럼 20000
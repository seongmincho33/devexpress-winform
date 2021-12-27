# DateEditControl

<img src="../../img/dateeditcontrol_img/DateEditControl_vista.png" width="400" height="400"/>
<img src="../../img/dateeditcontrol_img/DateEditControl.png" width="400" height="400"/>

1. [DateEdit. 사용자 달력클릭시 일어나는 이벤트](1-DateEdit-사용자-달력클릭시-일어나는-이벤트)
2. [DateTime.MinValue 와 (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue 의 차이](DateTimeMinValue-와-DateTimeSystemDataSqlTypesSqlDateTimeMinValue-의-차이)
3. [EditDate 달력 일 클릭시 컬럼에 자동으로 날짜값 넣어주기](EditDate-달력-일-클릭시-컬럼에-자동으로-날짜값-넣어주기)
4. [EditDate 달력에 연월 선택시 그리드뷰에 주말만 표시해주기 (셀값에 칠하기)](EditDate-달력에-연월-선택시-그리드뷰에-주말만-표시해주기-셀값에-칠하기)
5. [DateEdit의 .MinValue를 sql에 넘겨주지 못하므로 해결하는 두번째 방법](DateEdit의-MinValue를-sql에-넘겨주지-못하므로-해결하는-두번째-방법)
6. [DateEdit 달력을 사용자로부터 연월만 받아오고 연월만 텍스트를 달력컬럼에 붙여주기](DateEdit-달력을-사용자로부터-연월만-받아오고-연월만-텍스트를-달력컬럼에-붙여주기)



_________________________________________________________________________
<br>

# 1. DateEdit. 사용자 달력클릭시 일어나는 이벤트

DateEdit 형 객체에서 DateTimeChanged 이벤트에 콜백메서드를 등록해주어야 합니다. 

```C#
public void SetGridControl()
{
    this.DateEditSomething.DateTimeChanged += Date_Transport_DateTimeChanged; // 여기가 이벤트 등록하는곳
}

```

_________________________________________________________________________
<br>


# 2. DateTime.MinValue 와 (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue 의 차이


DateTime.MinValue 는 달력 clear누르면 반환하는 값입니다. Devexpress 달력 컨트롤이 그렇게 되어있습니다. DateEditSomething.DateTime 의 리턴값이라 보면 됩니다. 근데 이는 sql파라미터로 넘어가지 않습니다. 이유는 00001년 01월01인 값이기 때문입니다.그래서 최소값으로 1753년대가 최소값으로 세팅된 sql은 위의값을 받지 못합니다. 따라서 (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue 로 변환시켜 주어야 합니다. 

아래 코드는 개인적으로 만들어본 FilterDate() 메서드입니다. 이런식으로 값이 DateTime.MinValue일 경우에는 바꿔줘야합니다.

```C#
private DateTime FilterDate(DateTime filterDate)
{
    if(filterDate == DateTime.MinValue)
    {
        filterDate = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
    }
    return filterDate;
}
```

_________________________________________________________________________
<br>

# 3. EditDate 달력 일 클릭시 컬럼에 자동으로 날짜값 넣어주기

_________________________________________________________________________
<br>

# 4. EditDate 달력에 연월 선택시 그리드뷰에 주말만 표시해주기 (셀값에 칠하기)

먼저 세팅에서 그리드뷰에 이벤트를 걸어줍니다. .GridView.RowCellStyle 이 이벤트는 화면이 보여질때 Fire해줍니다. 즉 보여질때 연산되는것입니다.

```C#
private void SetGridControl()
{
    this.Grid_Something.RowCellStyle += Grid_Something_RowCellStyle;
    this.Date_Something.DateTime = DateTime.Today;
}
```

그리고 윤년까지 고려해서 해당 연, 월의 모든 일의 요일을 리스트 형식으로 반환하는 메서드를 작성합니다.

```C#
private List<DayOfWeek> DateTime_Weeks(int year, int month, DateEdit Date_Something)
{
    int[] days_In_Month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    if ((year % 4 == 0) && (year % 100 != 0) || (year % 400 == 0))
    {
        days_In_Month[1] = 29;
    }

    List<DayOfWeek> dateTime_Weeks = new List<DayOfWeek>();
    for (int i = 1; i < days_In_Month[month - 1] + 1; i++)
    {
        dateTime_Weeks.Add((new DateTime(year, month, i)).DayOfWeek);
    }

    if (days_In_Month[Date_Something.DateTime.Month - 1] == 30)
    {
        dateTime_Weeks.Add(DayOfWeek.Monday);
    }
    else if (days_In_Month[Date_Something.DateTime.Month - 1] == 28)
    {
        dateTime_Weeks.Add(DayOfWeek.Monday);
        dateTime_Weeks.Add(DayOfWeek.Monday);
        dateTime_Weeks.Add(DayOfWeek.Monday);
    }
    else if (days_In_Month[Date_Something.DateTime.Month - 1] == 29)
    {
        dateTime_Weeks.Add(DayOfWeek.Monday);
        dateTime_Weeks.Add(DayOfWeek.Monday);
    }

    return dateTime_Weeks;
}
```

이벤트 콜백 메서드를 작성합니다. 필드의 데이터값에 주말이면 토요일은 파란색 일요일은 빨강으로 칠해줍니다. 위의 메서드를 사용합니다.

```C#
private void DefaultView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
{
    List<DayOfWeek> dateTime_Weeks = DateTime_Weeks(this.Date_Something.DateTime.Year, this.Date_Something.DateTime.Month, this.Date_Something);

    if (Date_Something.DateTime != DateTime.MinValue)
    {
        for (int i = 0; i < 31; i++)
        {
            if (e.Column.FieldName == $"DAY_{i + 1}")
            {
                if (dateTime_Weeks[i] == DayOfWeek.Sunday)
                {
                    e.Column.AppearanceHeader.BackColor = Color.LavenderBlush;
                    e.Appearance.BackColor = Color.LavenderBlush;
                }
                else if (dateTime_Weeks[i] == DayOfWeek.Saturday)
                {
                    e.Column.AppearanceHeader.BackColor = Color.LightCyan;
                    e.Appearance.BackColor = Color.LightCyan;
                }
                //e.Column.AppearanceHeader.BackColor = (dateTime_Weeks[i] == DayOfWeek.Sunday || dateTime_Weeks[i] == DayOfWeek.Saturday) ? Color.Red : Color.Transparent;
                //e.Appearance.BackColor = (dateTime_Weeks[i] == DayOfWeek.Sunday || dateTime_Weeks[i] == DayOfWeek.Saturday) ? Color.LavenderBlush : Color.Transparent;
                //this.Grid_Labor.DefaultView.Appearance.HeaderPanel.BackColor = (dateTime_Weeks[i] == DayOfWeek.Sunday || dateTime_Weeks[i] == DayOfWeek.Saturday) ? Color.Red : Color.Transparent;
            }
        }
    }
}
```

_________________________________________________________________________
<br>

# 5. DateEdit의 .MinValue를 sql에 넘겨주지 못하므로 해결하는 두번째 방법

추후 추가 예정

_________________________________________________________________________
<br>

# 6. DateEdit 달력을 사용자로부터 연월만 받아오고 연월만 텍스트를 달력컬럼에 붙여주기

아래 코드를 추가하면 됩니다. 간단한 작업이라 설명X

```C#
private void SetGridControl()
{
    this.Date_Something.Properties.Mask.EditMask = "Y";
    this.Date_Something.Properties.Mask.EditMask = "yyyy/MM";
    this.Date_Something.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.Date_Something.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearView;
}
```

_________________________________________________________________________
<br>
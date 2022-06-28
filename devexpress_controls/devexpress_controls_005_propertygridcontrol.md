# PropertyGridControl

![img](../img/devexpress_img/propertygridcontrol/001.png)

- [PropertyGridControl](#propertygridcontrol)
  - [1. 간단한 예제](#1-간단한-예제)
  - [2. PropertyDescriptionControl](#2-propertydescriptioncontrol)
  - [3. 프로퍼티 컨틀롤로 속성값 변경](#3-프로퍼티-컨틀롤로-속성값-변경)
  - [4. 카테고리(Category) 속성의 Caption을 변경하는 방법](#4-카테고리category-속성의-caption을-변경하는-방법)

<hr />
<br />

## 1. 간단한 예제 

아래와 같이 먼저 간단한 모델을 하나 만듭니다. Product클래스 입니다.

```C#
//Create a business object.
public class Product
{
    public Product(string name, string category, float price, string color)
    {
        Name = name; Category = category; Price = price; Color = color;
    }
    public string Name { get; set; }
    public string Category { get; set; }
    public float Price { get; set; }
    public string Color { get; set; }
}
```

폼을 하나 생성하고 initSampleData()메서드로 그리드 컨트롤에 데이터소스를 바인딩해줍니다. 그리드의 포커스된 로우가 바뀔때 호출하는 이벤트를 등록해줍니다. 이렇게 되면 그리드의 항목을 누를때마다 PropertyGridControl에 해당에 맞는 프로퍼티를 보여주게 됩니다. (Product)gridView.GetRow(gridView1.FocusedRowHandle)를 SelectedObject프로퍼티에 할당했는데요 Object[] 형식이어서 왠만한 형식의 데이터소스는 다 받습니다. (Product)클래스로 다운

```C#
public partial class Form2 : Form
{
    public Form2()
    {
        InitializeComponent();
        SetGridControl();
    }

    private void SetGridControl()
    {
        //Bind the lookup editor to a data source.
        this.gridControl1.DataSource = this.initSampleData();                                    
        this.gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
        this.propertyDescriptionControl1.PropertyGrid = this.propertyGridControl1;
    }

    //Assign the selected object to the Property Grid.    
    private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
    {
        GridView gridView = sender as GridView;
        //프로퍼티 그리드에 데이터를 등록해줍니다. 
        propertyGridControl1.SelectedObject = (Product)gridView.GetRow(gridView1.FocusedRowHandle);
    }

    //Create a list of products.
    public List<Product> initSampleData()
    {
        List<Product> result = new List<Product>();
        result.Add(new Product("FC GTX FE", "Fullcover GPU Blocks", 95.80f, "esd"));
        result.Add(new Product("eefa", "qasdas", 95821.0f, "23rrr"));
        result.Add(new Product("qwqE", "zzz GPU q", 95.80f, "gth"));
        result.Add(new Product("FgrC jy FE", "eqwegfj GPU jytr", 95.80f, "xx"));
        //. . .
        return result;
    }
}
```

결과는 아래사진과 같습니다.

![img](../img/devexpress_img/propertygridcontrol/002.png)

<hr />
<br />

## 2. PropertyDescriptionControl



<hr />
<br />

## 3. 프로퍼티 컨틀롤로 속성값 변경

<hr />
<br />

## 4. 카테고리(Category) 속성의 Caption을 변경하는 방법

![img](../img/devexpress_img/propertygridcontrol/003.png)

아래 코드 예시와 같이 속성으로 Category를 줄 수 있습니다. 이렇게 속성으로 준 Category는 Devexpress 컨트롤이 잡아내서 제목으로 사용하던 뭘로 사용하던 할겁니다. 근데 이러한 속성값을 변경해서 보여줘야하는경우가 있습니다. 근데 속성값을 수정할 수 없으므로 보여지는 Viewer부분의 Caption을 변경하는것이 차선책입니다. 

```C#
private double _Lx_mm;
[Category("1. Foundation Information"), DisplayName(" 3) Lx (mm)"), Description("")]
public double Lx_mm
{
    get
    {
        return Math.Round(_Lx_mm, 3);
    }
    set
    {
        _Lx_mm = value;
    }
}
```

아래와 같이 PropertyGridview에서 사용한다고 가정할때 Category 속성에 접근해서 Caption을 변경합니다. 

```C#
foreach (CategoryRow category in propDataView.Rows)
{
    string caption = category.Properties.Caption;
    if (category.Visible == true)
    {
        if(int.TryParse(caption.Substring(0, 1), out int n))
            category.Properties.Caption = i++ + caption.Substring(1);
    }
}
```
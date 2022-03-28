# 컨트롤 공통속성

목차

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

<hr />
<br />

## 1. ApplicationSettings

이 속성은 모든 컨트롤에 있는 속성인데 사용자가 값을 바꾸면 셋팅값으로 저장했다가 다시 불러오는 기능입니다. 쉽게 설명하자면 윈도우 사이즈를 사용자가 줄이거나 늘렸을때 프로그램을 끄고 다시키면 사용자가 설정한 값을 다시 불러오는 기능입니다. 물론 이건 하나의 예시고 다른용도입니다. 아래는 예시입니다.

먼저 프로젝트에 셋팅값 변수를 생성해 주어야 합니다. 프로젝트를 오른쪽 클릭한 후 속성을 눌러주세요.

![img](../img/devexpress_img/properties/001.png)

프로젝트의 셋팅값 변수를 할당합니다. 여기에 이름과 형식 법위 값이 중요한데 값이 없으면 nullexception을 냅니다. 형식에 따라서 ApplicationSettings 하위에 PropertyBinding 속성에 바인딩 할 수 있는 속성이 달라집니다.

![img](../img/devexpress_img/properties/002.png)

메인 폼 컨트롤의 속성으로 들어가면 ApplicationsSettings 하위에 PropertyBinding속성 컬렉션이 있습니다. 눌러줍니다.

![img](../img/devexpress_img/properties/003.png)

아래와 같이 팝업이 뜨면서 바인딩할 변수를 선택해 줄 수 있습니다. 위에서 작성한 WindowLocation이 보이죠? 이걸 인제 선택할 수 있습니다. 

![img](../img/devexpress_img/properties/004.png)

이로써 사용자가 프로그램을 실행하고 사용할 때 프로그램의 위치를 기억해서 프로젝트의 WindowLocation 변수에 값을 저장했다가 다시 프로그램을 실행시키면 그 위치로 생성됨을 알 수 있습니다. 

이와 달리 코드단에서도 더 많은 기능을 수행할 수 있습니다. 이벤트를 사용해서 초기 셋팅값을 주는 방식입니다. 아래는 예제 코드입니다. 

```C#
public partial class Form1 : Form
{     
    public Form1()
    {
        InitializeComponent();
        SetControls();
    }

    private void SetControls()
    {
        this.Load += FormMain_Load; //Form1의 Load 이벤트 핸들러에게 FormMain_Load를 등록합니다.          
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
        // Set window location
        if (Settings.Default.WindowLocation != null)
        {
            this.Location = Settings.Default.WindowLocation;
        }

        // Set window size
        if (Settings.Default.WindowSize != null)
        {
            this.Size = Settings.Default.WindowSize;
        }
        this.FormClosing += FormMain_FormClosing;
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Copy window location to app settings
        Settings.Default.WindowLocation = this.Location;

        // Copy window size to app settings
        if (this.WindowState == FormWindowState.Normal)
        {
            Settings.Default.WindowSize = this.Size;
        }
        else
        {
            Settings.Default.WindowSize = this.RestoreBounds.Size;
        }

        // Save settings
        Settings.Default.Save();
    }
}
```

위와같이 컨트롤마다 컨트롤 생성 이벤트 핸들러에 위의 FormMain_Load, FormMain_FormClosing이벤트를 등록해주는 방법이 있습니다만 그러면 컨트롤 개수마다 
커스텀한 이벤트들을 작성해 주어야 하므로 코드양이 매우 길어질 수 있습니다. 따라서 모든 컨트롤에는 속성값으로 ApplicationSettings와 하위에 
PropertyBinding 속성을 제공합니다. 여기에 필요한 셋팅값을 줄 수 있습니다. 

<hr />
<br />

## 2. DataBindings

데이터 바인딩은 컨트롤단에서 사용자가 값을 바꾸면 연결된 데이터 모델/db도 자동으로 수정되게 만드는 기능입니다. Binding이란 뜻이 우리나라 말로 "묶다"라는 의미를 가지고 있습니다. 즉 컨트롤과 데이터를 한데로 묶어서 연결시킨다~ 라는 개념입니다. 모델이나 db등에 데이터가 바뀐다면 자연스럽게 묶여버린 컨트롤에도 값이 바뀌겠죠. 

가령 예를 들어 Devexpress의 TextEdit 컨트롤과 모델클래스를 바인딩 해보겠습니다. 아래 평범한 모델 Person.cs를 생성합니다.

```C#
public class Person 
{
    public string Name {get;set;}
    public DateTime DateOfBirth {get;set;}
}
```

그리고 나서 TextEdit컨트롤에 (ControlBindingsCollection)Databinding 프로퍼티의 Add메서드를 호출해줍니다. 첫 인자는 컨트롤의 속성(프로퍼티) 두번째 인자는 모델, 세번째 인자는 모델의 속성 (프로퍼티)을 차례대로 넣어줍니다.

```C#
this.textEdit1.DataBindings.Add("Text", person, "Name");
```

그런데 이렇게만 하면 사용자가 값을 아무리 바꾸어도 모델의 값이 바뀌지 않습니다. 이유는 바인딩은 되었지만 UI에게 값이 바뀌었다고 알려주지 않았기 때문입니다(속성값이 변경되었음을 클라이언트에게 알려줌). 모델에 INotifyPropertyChanged 인터페이스를 상속받게 하고 UI에게 값이 바뀌었다고 알려줘야 합니다. 참으로.. 이게 뭔상황인지.. 그래서 Person.cs를 아래와 같이 고쳐줘야 합니다. 

```C#
public class Person : INotifyPropertyChanged
{
    private string name;
    public string Name
    {
        get { return name; }
        set
        {
            name = value;
            NotifyPropertyChanged("name");
        }
    }
    private DateTime dateOfBirth;
    public DateTime DateOfBirth
    {
        get { return dateOfBirth; }
        set
        {
            dateOfBirth = value;
            NotifyPropertyChanged("dateOfBirth");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(String info)
    {
        var handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(info));
        }
    }
}
```

또한 바인딩의 Add메서드의 아규먼트 formattingEnabled와 updateMode에 각각 true와 DataSourceUpdateMode.OnPropertyChanged를 넣어줘야 합니다. 

```C#
this.textEdit1.DataBindings.Add("Text", person, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
```

보면 여기서 DateOfBirth도 있는데 상식적으로 생각해보면 컨트롤에서 TextEdit은 입력값을 하나만 받아서 속성으로 "Text"라는것이 있는데 이거는 이미 Person클래스의 Name과 바인딩 했습니다. 그러면 DatOfBirth도 바인딩을 하고싶어도 못합니다. 다른 컨트롤을 사용해야합니다. 아니면 TextEdit의 컨트롤을 하나더 생성해서 DateOfBirth와 바인딩 해도 됩니다. 

근데 DataSource는 컬렉션을 줄 수도 있습니다. 하나의 MSDN으로 부터 가져온 예제를 보면 알 수 있습니다. 먼저 폼에 TextEdit 2개를 만들고 Button하나를 생성합니다. 그리고 아래와 같이 State.cs클래스를 하나 만들어봅니다. 

```C#
public class State
{
    private string stateName;
    public string Name
    {
        get { return stateName; }
    }

    private string stateCapital;
    public string Capital
    {
        get { return stateCapital; }
    }

    public State(string name, string capital)
    {
        stateName = name;
        stateCapital = capital;
    }
}
```

아래와 같이 코드를 작성하면 버튼을 누를때마다 ArrayList에 있는 State모델들을 하나씩 꺼내서 보여주게 됩니다. 여기서 중요한점은 DataBinding 프로퍼티에게 DataSource를 넘길때 컬랙션 스타일을 넘겨도 된다는 것입니다. 

```C#
public partial class Form1 : Form
{
    ArrayList states;
    BindingSource bindingSource1;

    public Form1()
    {
        InitializeComponent();
        SetTextEditAndBox();
    }

    private void SetTextEditAndBox()
    {              
        states = new ArrayList();
        states.Add(new State("California", "Sacramento"));
        states.Add(new State("Oregon", "Salem"));
        states.Add(new State("Washington", "Olympia"));
        states.Add(new State("Idaho", "Boise"));
        states.Add(new State("Utah", "Salt Lake City"));
        states.Add(new State("Hawaii", "Honolulu"));
        states.Add(new State("Colorado", "Denver"));
        states.Add(new State("Montana", "Helena"));

        bindingSource1 = new BindingSource();
        bindingSource1.DataSource = states;

        this.textEdit1.DataBindings.Add("Text", bindingSource1, "Name");
        this.textEdit2.DataBindings.Add("Text", bindingSource1, "Capital");

        this.button1.Click += button1_Click;        
    }

    private void button1_Click(object sender, EventArgs e)
    {
        // If items remain in the list, remove the first item.
        if (states.Count > 0)
        {
            states.RemoveAt(0);

            // Call ResetBindings to update the textboxes.
            bindingSource1.ResetBindings(false);
        }
    }
}
```

위의 코드 또한 INotifyPropertyChanged State.cs에 부여하지 않았기 때문에 컨트롤에서 값을 바꾸어도 모델값은 바뀌지 않습니다. 아래는 완전히 정리한 예제 입니다. 아래의 NotifyPropertyChanged의 구현이 위와는 다릅니다. [CallerMemberName]속성을 파라미터에 부여했는데요. 이는 호출한 프로퍼티의 멤버이름을 자동으로 가져옵니다. 따라서 NotifyPropertyChanged(stateName);이렇게 하지 않고 NotifyPropertyChanged();만 프로퍼티 안에서 호출해주면 자동으로 stateName이 파라미터값으로 들어가게 됩니다. 

```C#
public class State : INotifyPropertyChanged
    {
        private string stateName;
        public string Name
        {
            get { return stateName; }
            set 
            {
                if(value != stateName)
                {
                    stateName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string stateCapital;

        public string Capital
        {
            get { return stateCapital; }
            set
            {
                if(value != stateCapital)
                {
                    stateCapital = value;
                    NotifyPropertyChanged();
                }                    
            }
        }

        public State(string name, string capital)
        {
            stateName = name;
            stateCapital = capital;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }    
    }


public partial class Form1 : Form
{
    ArrayList states;
    BindingSource bindingSource1;

    public Form1()
    {
        InitializeComponent();
        SetTextEditAndBox();
    }

    private void SetTextEditAndBox()
    {              
        states = new ArrayList();
        states.Add(new State("California", "Sacramento"));
        states.Add(new State("Oregon", "Salem"));
        states.Add(new State("Washington", "Olympia"));
        states.Add(new State("Idaho", "Boise"));
        states.Add(new State("Utah", "Salt Lake City"));
        states.Add(new State("Hawaii", "Honolulu"));
        states.Add(new State("Colorado", "Denver"));
        states.Add(new State("Montana", "Helena"));

        bindingSource1 = new BindingSource();
        bindingSource1.DataSource = states;

        //바인딩 파라미터로 true 와 DataSourceUpdateMode.OnPropertyChanged를 넘겨줘야 합니다. 
        this.textEdit1.DataBindings.Add("Text", bindingSource1, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
        this.textEdit2.DataBindings.Add("Text", bindingSource1, "Capital", true, DataSourceUpdateMode.OnPropertyChanged);

        this.button1.Click += button1_Click;        
    }

    private void button1_Click(object sender, EventArgs e)
    {
        // If items remain in the list, remove the first item.
        if (states.Count > 0)
        {
            states.RemoveAt(0);

            // Call ResetBindings to update the textboxes.
            bindingSource1.ResetBindings(false);
        }
    }
}
```

<hr />
<br />

## 3. AllowDrop

컨트롤에 데이터를 끌어서 놓을 수 있는지 여부를 나타냅니다(Boolean값). 아래는 예제 입니다. 아무 Form을 생성하고 거기에 AllowDrop을 true로 설정합니다. 그리고 관련된 이벤트를 작성합니다. 이미지 파일을 폼에 드래그 앤 드롭할 수 있게됩니다. 컨트롤 클래스이기 때문에 Control클래스를 상속받는 모든 컨트롤에 속성값으로 존재합니다.

```C#
private Image picture;
private Point pictureLocation;

public Form1()
{
   // Enable drag-and-drop operations and 
   // add handlers for DragEnter and DragDrop.
   this.AllowDrop = true;
   this.DragDrop += new DragEventHandler(this.Form1_DragDrop);
   this.DragEnter += new DragEventHandler(this.Form1_DragEnter);
}

protected override void OnPaint(PaintEventArgs e)
{
   // If there is an image and it has a location, 
   // paint it when the Form is repainted.
   base.OnPaint(e);
   if(this.picture != null && this.pictureLocation != Point.Empty)
   {
      e.Graphics.DrawImage(this.picture, this.pictureLocation);
   }
}

private void Form1_DragDrop(object sender, DragEventArgs e)
{
   // Handle FileDrop data.
   if(e.Data.GetDataPresent(DataFormats.FileDrop) )
   {
      // Assign the file names to a string array, in 
      // case the user has selected multiple files.
      string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
      try
      {
         // Assign the first image to the picture variable.
         this.picture = Image.FromFile(files[0]);
         // Set the picture location equal to the drop point.
         this.pictureLocation = this.PointToClient(new Point(e.X, e.Y) );
      }
      catch(Exception ex)
      {
         MessageBox.Show(ex.Message);
         return;
      }
   }

   // Handle Bitmap data.
   if(e.Data.GetDataPresent(DataFormats.Bitmap) )
   {
      try
      {
         // Create an Image and assign it to the picture variable.
         this.picture = (Image)e.Data.GetData(DataFormats.Bitmap);
         // Set the picture location equal to the drop point.
         this.pictureLocation = this.PointToClient(new Point(e.X, e.Y) );
      }
      catch(Exception ex)
      {
         MessageBox.Show(ex.Message);
         return;
      }
   }
   // Force the form to be redrawn with the image.
   this.Invalidate();
}

private void Form1_DragEnter(object sender, DragEventArgs e)
{
   // If the data is a file or a bitmap, display the copy cursor.
   if (e.Data.GetDataPresent(DataFormats.Bitmap) || 
      e.Data.GetDataPresent(DataFormats.FileDrop) ) 
   {
      e.Effect = DragDropEffects.Copy;
   }
   else
   {
      e.Effect = DragDropEffects.None;
   }
}
```
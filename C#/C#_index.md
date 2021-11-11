# C# 

## 1. [프로퍼티](#1-프로퍼티)
## 2. [delegate(델리게이트)](#2-delegate델리게이트)
1. 일반적인경우
2. delegate 사용
3. 언제 사용하지
4. 어디서 본듯한 delegate
5. 이벤트
6. MS 제공 델리게이트
    - Action<T> delegate
    - Func<T> delegate
    - Predicate<T> delegate
## 3. [링큐](#3-링큐)
1. 링큐패드5 (LINQPad5 .netFrameWork)

<hr>
<br>
<br>
<br>
<br>

# 1. 프로퍼티

<br>

<hr>
<br>
<br>
<br>
<br>

# 2. delegate(델리게이트)

<br>
<br>
<br>

## 1). 일반적인경우

일반적인 함수 호출은 이런식일 것입니다. Main메서드에서 Add 함수를 가져옵니다. 

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateSample001
{
    class Program
    {
        static void Main(string[] args)
        {
            Add(1, 2);

            Console.ReadKey();
        }

        static void Add(int value1, int value2)
        {
            Console.WriteLine($"더하기 : {value1 + value2}");
        }
    }
}

```

실행결과 :

<img src="../img/C_img/c_delegate_일반적인경우.png" width="300" height="100"/>

<br>
<br>
<br>

## 2). delegate 사용

delegate 를 사용해보겠습니다. delegate는 클래스 안이나 밖에 상관없이 선언이 가능합니다. DeleCalculation을 선언해보겠습니다. 

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateSample001
{
    class Program
    {
        //델리게이트 선언 
        public delegate void DeleCalculation(int v1, int v2);

        static void Main(string[] args)
        {
           DeleCalculation Calc;
           Calc = Add;
           Calc += Multi;
           Calc(1, 2);

           Console.ReadKey();
        }

        static void Add(int value1, int value2)
        {
            Console.WriteLine($"더하기 : {value1 + value2}");
        }

        static void Multi(int v1, int v2)
        {
            Console.WriteLine($"곱하기 : {v1 * v2}");
        }
    }
}
```

실행결과 :

<img src="../img/C_img/c_delegate_use.PNG" width="300" height="100"/>

<br>
<br>
<br>

## 3). 언제 사용하지

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateSample001
{
    class Program
    {
        //델리게이트 선언 
        public delegate void DeleCalculation(int v1, int v2);

        static void Main(string[] args)
        {
            Run(Add);
            Run(Multi);

            Console.ReadKey();
        }

        static void Add(int value1, int value2)
        {
            Console.WriteLine($"더하기 : {value1 + value2}");
        }

        static void Multi(int v1, int v2)
        {
            Console.WriteLine($"곱하기 : {v1 * v2}");
        }

        static void Run(Action<int, int> deleValue)
        {
            deleValue(1, 2);
        }
    }
}
```

실행결과 :

<img src="../img/C_img/c_delegate_use_when.PNG" width="300" height="100"/>

<br>
<br>
<br>

## 4). 어디서 본듯한 delegate

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateSample001
{
    class Program
    {
        //델리게이트 선언 
        public delegate void DeleCalculation(int v1, int v2);

        static void Main(string[] args)
        {
            EventHandler Calc = null;
            Calc += txtBox_Clicked;
            Calc(new object(), new EventArgs());

            Console.ReadKey();
        }

        static void Add(int value1, int value2)
        {
            Console.WriteLine($"더하기 : {value1 + value2}");
        }

        static void Multi(int v1, int v2)
        {
            Console.WriteLine($"곱하기 : {v1 * v2}");
        }

        static void Run(Action<int, int> deleValue)
        {
            deleValue(1, 2);
        }

        static void txtBox_Clicked(object s, EventArgs e)
        {
            Console.WriteLine($"이벤트 처리");
        }
    }
}
```

실행결과 :

<img src="../img/C_img/c_delegate_use_where.PNG" width="300" height="100"/>

<br>
<br>
<br>

## 5). 이벤트

먼저 솔루션 하위 프로젝트 구조입니다. MyButtonClass프로젝트를 delegateSample002가 참조하고있습니다.

<img src="../img/C_img/c_delegate_event_1.PNG" width="400" height="300"/>

아래는 폼 클래스입니다.

```C#
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace delegateSample002
{
    public partial class Form1 : Form
    {
        MyButtonClass.Button btn001 = new MyButtonClass.Button();
        public Form1()
        {
            InitializeComponent();
            btnTest01.Click += BtnTest01_Click;
            
            btn001.Clicked += Btn001_Clicked;
            btn001.Clicked2 += Btn001_Clicked2;
        }

        private void BtnTest01_Click(object sender, EventArgs e)
        {
            lstBox.Items.Add("클릭 이벤트 처리");
            btn001.OnClick();

            btn001.CallBackTest01(Add);
            btn001.CallBackTest01((v1, v2) => { lstBox.Items.Add($"{v1} + {v2}"); });
            btn001.CallBackTest01(delegate (int v1, int v2) { lstBox.Items.Add($"{v1} + {v2}"); });
            btn001.CallBackTest02(Add, 2, 2);
            btn001.CallBackTest02((v1, v2) => { lstBox.Items.Add($"{v1} + {v2}"); }, 2, 2);
            btn001.CallBackTest02(delegate (int v1, int v2) { lstBox.Items.Add($"{v1} + {v2}"); }, 2, 2);
        }

        private void Btn001_Clicked(object sender, EventArgs e)
        {
            lstBox.Items.Add("내 버튼 클릭 이벤트 처리");
        }

        private void Btn001_Clicked2(object arg1, EventArgs arg2)
        {
            lstBox.Items.Add("내 버튼 클릭 이벤트 처리2");
        }

        private void Add(int v1, int v2)
        {
            lstBox.Items.Add($"{v1} + {v2}");
        }
    }
}

```

<br>

MyButtonClass 네임스페이스 하위 Button.cs입니다. 

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyButtonClass
{
    public class Button
    {
        public event EventHandler Clicked;

        public delegate void DeleCall(int v1, int v2);
        public event Action<object, EventArgs> Clicked2;
        public void OnClick()
        {
            if (Clicked != null)
                Clicked(this, new EventArgs());

            if (Clicked2 != null)
                Clicked2(this, new EventArgs());
        }

        public void CallBackTest00(DeleCall delCall)
        {
            delCall(1, 2);
        }

        public void CallBackTest01(Action<int, int> delCall)
        {
            delCall(1, 2);
        }

        public void CallBackTest02(Action<int, int> delCall, int v1, int v2)
        {
            delCall(v1, v2);
        }
    }
}

```

실행 결과 :

<img src="../img/C_img/c_delegate_event_2.PNG" width="800" height="450"/>

## 6). MS 제공 델리게이트

<hr>
<br>
<br>
<br>
<br>

# 3. 링큐

<br>


























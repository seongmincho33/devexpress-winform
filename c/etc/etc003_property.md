# 프로퍼티

프로퍼티에 대해서 정말 여러가지 설명이 있습니다. 책에도 잘 설명되어있어요. 하지만 이걸 결국엔 언제 써야하고 왜 써야하는지가 어렵습니다. 단순히 필드값은 외부로부터 보호되어야하므로 메서드를 통해서 필드값에 접근해야하고, 프로퍼티는 이 메서드를 너무 많이 쓰니깐 코드줄을 줄여주기 위해서 .NET이 도입한것으로 알게됩니다. 근데 이것으로는 설명이 부족하다고 생각합니다. 따라서 조사좀 해봤습니다 'ㅁ';;;

<br />

## 1. 프로퍼티 인터페이스

중요한 차이점은 인터페이스는 프로퍼티를 가질 수 있지만 필드는 가질 수 없다는 것입니다. 이건 프로퍼티가 클래스의 공개 인터페이스를 정의하는데 사용되는 한편, 필드는 클래스의 비공개 내부작업에서 사용된다는 것입니다. 

<br />

## 2. 프로퍼티 이벤트

프로퍼티를 사용하면 속성 값이 변경될때(PropertyChangedEvent라고도 함) 또는 취소(Cancelation)을 지원하도록 값이 변경되기전에 이벤트를 발생 시킬 수 있습니다. 이건 필드에서는 불가능합니다. 아래는 예시입니다.

```C#
public class Person 
{
    private string _name;

    public event EventHandler NameChanging;     
    public event EventHandler NameChanged;

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            OnNameChanging();
            _name = value;
            OnNameChanged();
        }
    }

    private void OnNameChanging()
    {       
        NameChanging?.Invoke(this,EventArgs.Empty);       
    }

    private void OnNameChanged()
    {
        NameChanged?.Invoke(this,EventArgs.Empty);
    }
}
```

<br />

## 3. 프로퍼티로 필드값 읽고 쓰고의 허락범위 정하기

데이터 테이블을 예로 들어보겠습니다(MSDN거 아님 주의!). 행의 _count는 읽기만 가능하고 열의 _caption은 읽고 쓰기가 가능합니다. 당연히 이렇게 만들어야 하겠지만 필드로는 만들 수가 없습니다!

```C#
public class DataTable
{
    public class Rows
    {       
       private string _count;        

       // This Count will be accessable to us but have used only "get" ie, readonly
       public int Count
       {
           get
           {
              return _count;
           }       
       }
    } 

    public class Columns
    {
        private string _caption;        

        // Used both "get" and "set" ie, readable and writable
        public string Caption
        {
           get
           {
              return _caption;
           }
           set
           {
              _caption = value;
           }
       }       
    } 
}
```

<br />

## 4. 프로퍼티그리드(PropertyGrid)의 프로퍼티

아마 Button을 윈폼에서 만들고 사용해봤을 겁니다. Button의 프로퍼티는 PropertyGrid에 "Text", "Name"처럼 사용자가 설정할 수 있다는것도 알거에요. 우리가 버튼을 드래그하고 드롭한뒤, 설정을 클릭하면 자동으로 버튼 클래스를 찾아서 안에있는 프로퍼티를 PropertyGrid에 붙여줍니다. 필드는 public이어도 보이지 않아요. 아래 예제를 봐주세요. PropertyGrid에는 "Text"와 "Name"이 보일것입니다. 그런데 "SomeProperty"프로퍼티는 보이지 않을거에요. 왜냐고요? 프로퍼티는 attribute를 받습니다. "[Browsable(False)]" 가 false일때 보이지 않게 됩니다. 

```C#
public class Button
{
    private string _text;        
    private string _name;
    private string _someProperty;

    public string Text
    {
        get
        {
           return _text;
        }
        set
        {
           _text = value;
        }
   } 

   public string Name
   {
        get
        {
           return _name;
        }
        set
        {
           _name = value;
        }
   } 

   [Browsable(false)]
   public string SomeProperty
   {
        get
        {
           return _someProperty;
        }
        set
        {
           _someProperty= value;
        }
   } 
}
```

<br />

## 5. 프로퍼티만이 Binding Source에 사용될 수 있습니다.

Binding Source는 코드줄을 줄여주는데 도움을 줘요. 근데 필드는 허락되지 않습니다. 

<br />

## 6. 디버깅 모드

값을 저장하기 위해 필드를 사용한다고 생각해봅시다. 언젠가는 우리가 디버깅할때 필드값이 null인지 확인해야 합니다. 근데 1000줄이상되면 찾기가 힘들어요..;; 이럴때 프로퍼티를 사용합니다. 프로퍼티 안에 디버그 모드를 넣을 수 있어요. 

```C#
public string Name
{
    // Can set debug mode inside get or set
    get
    {
        return _name;
    }
    set
    {
        _name = value;
    }
}
```

# 4. 기타

## 1. 프로퍼티를 Call-By-Reference로 던지기(Passing properties by reference in C#)

클래스 인스턴스의 프로퍼티의 값을 메서드의 인자로 넘겨서 값을 바꿔주는 방법을 생각해보겠습니다. 사실 ref로는 되질 않습니다. 속성값은 즉 프로퍼티는 메서드 파라미터로 넘길시 그냥 깊은복사가 일어나 버립니다. 그래서 어찌해야하나 생각해보면 여러가지 방법이 있습니다. 이 예제의 출처는 스택오버플로우입니다. [링크](https://stackoverflow.com/questions/1402803/passing-properties-by-reference-in-c-sharp)

아래 코드는 잘못된 예 입니다. 프로퍼티값을 ref로 Call-by-Reference할 수 없습니다.  이 문제를 해결하는 방식을 취하겠습니다.
```C#
GetString(inputString, ref Client.WorkPhone)

private void GetString(string inValue, ref string outValue)
{
    if (!string.IsNullOrEmpty(inValue))
    {
        outValue = inValue;
    }
}
```

따라서 아래 예제들과 같은 방식을 취해야 합니다. 

<br />

아래 코드는 그저 value를 리턴합니다. 이건 원하는 목표가 아닐것입니다. 
#### 1. Return Value

```C#
string GetString(string input, string output)
{
    if (!string.IsNullOrEmpty(input))
    {
        return input;
    }
    return output;
}

void Main()
{
    var person = new Person();
    person.Name = GetString("test", person.Name);
    Debug.Assert(person.Name == "test");
}
```

<br />

아래 코드는 델리게이트를 사용했습니다.
#### 2. Delegate

```C#
void GetString(string input, Action<string> setOutput)
{
    if (!string.IsNullOrEmpty(input))
    {
        setOutput(input);
    }
}

void Main()
{
    var person = new Person();
    GetString("test", value => person.Name = value);
    Debug.Assert(person.Name == "test");
}

```

<br />

링큐를 사용했습니다.
#### 3. LINQ Expression

```C#
void GetString<T>(string input, T target, Expression<Func<T, string>> outExpr)
{
    if (!string.IsNullOrEmpty(input))
    {
        var expr = (MemberExpression) outExpr.Body;
        var prop = (PropertyInfo) expr.Member;
        prop.SetValue(target, input, null);
    }
}

void Main()
{
    var person = new Person();
    GetString("test", person, x => x.Name);
    Debug.Assert(person.Name == "test");
}
```

<br />

리플렉션을 사용했습니다.
#### 4. Reflection

```C#
void GetString(string input, object target, string propertyName)
{
    if (!string.IsNullOrEmpty(input))
    {
        var prop = target.GetType().GetProperty(propertyName);
        prop.SetValue(target, input);
    }
}

void Main()
{
    var person = new Person();
    GetString("test", person, nameof(Person.Name));
    Debug.Assert(person.Name == "test");
}
```


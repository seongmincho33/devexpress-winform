# 레코드(Records)

지난 C# 7.2의 변화는 대체로 "값 형식"을 대표하는 struct 에 대한 개선 사항이 주를 이뤘습니다. 하지만 읽기 전용구조체나 메서드의 매개변수에 in 변경자를 추가하는 것의 세부적인 사항에는 "struct이기 때문에 발생하는 방어 복사본 문제"가 관련돼어 초보개발자는 이해하기 힘들다고 합니다. 그에 반해 class의 경우 그런 부작용이 없는 데다 기본적으로 대다수의 개발자가 struct 보다는 class를 사용하는 것이 더 일반적입니다.

그런데 class기반한 값 형식을 구현하는 것은 그만큼의 번거로움을 수반합니다. 예를 들어, X-Y좌표를 보관하는 Point 타입을 처음에는 간략하게 다음과 같은 식으로 정의해 시작할 수 있습니다. 

```C#
public class Point 
{
    public int x;
    public int y;
}
```

하지만 이 클래스를 사용하는 코드가 늘어나면서 값을 비교하거나 Dictionary등의 자료 구조에 키값으로 사용하고 싶다면 참조 주솟값을 기본으로 사용하는 class의 단점을 보완하기 위해 Equals와 GetHashCode메서드를 재정의하게 됩니다. 

```C#
public override int GetHashCode() 
{
    return X ^ Y;
}

public override bool Equals(object obj)
{
    return this.Euals(obj as Point);
}

public virtual bool Equals(Pointer other) 
{
    if (object.ReferenceEquals(other, null)
    {
        return fals;
    }

    return (this.X == other.X && this.Y == other.Y);
}
```

여기서 더 나아가 Point 인스턴스에 대한 값 비교를 Equlas보다 더 직관적인 ==, != 연산자를 사용하고 싶어지면 이것들도 Equals 메서드의 기준에 맞게 추가해야 합니다. 

```C#

```
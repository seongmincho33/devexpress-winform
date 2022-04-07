# 5. 힙 

1. 박싱/언박싱
2. 가비지 컬렉터
3. 전체 가비지 컬렉팅
4. 대용량 객체 힙
5. 자원 해제
6. 종료자

<hr />
<br />
<br />
<br />

## 박싱/언박싱

값 형식을 참조 형식으로 변환하는 것을 박싱(boxing)이라고 하며, 그 반대를 언박싱(unboxing)이라고 합니다. 이런 변환 과정은 object타입과 System.ValueType을 상속받은 값 형식의 인스턴스를 섞어 쓰는 경우에 발생합니다. 

```C#
static void Main(string[] args) 
{
    int a = 5;

    object obj = a; //박싱 : 값 형식인 int를 참조형식인 object에 대입

    int b = (int)obj; //언박싱 : 참조 형식인 object를 값 형식인 int에 대입
}
```

그런데 박싱과 언박싱이 별도로 언급될 만큼 중요한 이유는 뭘까요? 위의 코드가 실행되는 과정을 천천히 짚어보겠습니다.

1. int a = 5; 코드에서 a는 지역변수 입니다. 따라서 스택 메모리에 5라는 값이 들어갑니다. 
2. object obj = a; 코드에서 obj는 지역 변수고 스택에 할당됩니다. 하지만 object가 참조형이기 때문에 힙에도 메모리가 할당되고 변수 a의 값이 들어갑니다. 즉, 박싱이 발생한 것입니다. obj 지역변수는 힙에 할당된 주소를 가리킵니다. 
3. int b = (int)obj; 코드에서 b는 지역변수입니다. 따라서 스택 메모리에 b영역이 있고, 힙 메모리에 있는 값을 스택 메모리로 복사합니다. (언박싱)

보다시피 값 형식을 object로 형변환하는 것은 힙에 메모리를 할당하는 작업을 동반합니다. 이와 유사한 경우가 메서드에 인자를 전달할 때 발생합니다. 

```C#
static void Main(string[] args)
{
    int a = 5;
    int b = 6;


    int c = GetMaxValue(a, b);
}

private static int GetMaxValue(object v1, object v2) 
{
    int a = (int)v1;
    int b = (int)v2;

    if(a >= b)
    {
        return a;
    }

    return b;
}
```

GetMaxValue의 v1, v2매개변수는 object참조형이므로 힙에 메모리를 할당하고, 전달된 a, b의 값을 복사합니다. 박싱이 발생한 것입니다. 만약 GetMaxValue의 v1, v2매개변수가 int형이 었다면 스택의 값 복사만으로 끝날 수 있는 문제였지만 박싱으로 인해 관리 힙을 사용하게 됬고, 이는 GC에게 일을 시키게 만듭니다. 즉, 박싱이 빈번할 수록 GC는 바빠지고 프로그램의 수행 성능으 ㄴ그만큼 떨어집니다. 따라서 박싱을 과다하게 발생시킬 수 있는 코드는 최대한 줄여야 합니다.

닷넷 프레임워크의 BCL중에서도 박싱으로 인한 성능 손실을 없애기 위한 노력의 흔적을 어렵니 않게 찾아볼 수 있습니다. 지금까지 자주 쓰이던 Console.WriteLIne메서드가 한가지 좋은 사례입니다. Console.WriteLine은 다음과 같은 다양한 타입의 매개변수를 받도록 정의돼 있습니다.

```C#
public static void WriteLine(bool value);
public static void WriteLine(char value);
public static void WriteLine(decimal value);
public static void WriteLine(double value);
public static void WriteLine(float value);
public static void WriteLine(int value);
............
..........
.....
..
.
//등등등
```



# 1. 시간
    
1. System.DateTime
2. System.TimeSpan
3. System.Diagnostics.Stopwatch

<hr />
<br />
<br />
<br />

## 1. System.DateTime

DateTime 은 struct로 정의된 값 형식입니다. 자주 사용되는 생성자는 다음과 같습니다. 

```C#
public DateTime(int year, int month, int day);
public DateTime(int year, int month, int day, int hour, int minute, int second);
public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond);
```

속성중에서는 Now와 Year, Month, Day, Hour, Minute, Second, Millisecond 가 자주 사용됩니다. 

```C#
public static DateTime Now { get; }

public int Year { get; }
public int Month { get; }
public int Day { get; }
public int Hour { get; }
public int Minute { get; }
public int Second { get; }
public int Millisecond { get; }
```

정적 속성인 Now를 통해 현재 날짜/시간을 알아낼 수 있습니다. 아래는 예제입니다.

```C#
DateTime now = DateTime.Now;
Console.WriteLine(now);

DateTime dayForChildren = new DateTime(now.Year, 5, 5);
Console.WriteLine(dayForChildren);
```

1초(Second)는 1,000밀리초(Millisecond)인데, 이보다 정밀도가 더 높은 시간 값이 필요하다면 Ticks 속성을 이용하면 됩니다. 이 값은 1년 1월 1일 12시 자정을 기준으로 현재까지 100나노초 간격으로 흐른 숫자값입니다. 즉, 1밀리초의 10,000분의 1에 해당하는 정밀도입니다. 다음은 메서다가 실행되는데 걸린 시간을 Ticks를 이용해 계산하는 예제입니다.

```C#
static void Main(string[] args)
{
    DateTime before = DateTime.Now;
    Sum();
    DateTime after = DateTime.Now;

    long gap = after.Ticks - before.Ticks;
    Console.WriteLine("Total Ticks: " + gap);
    Console.WriteLine("Millisecond: " + (gap / 10000));
    Console.WriteLine("Second: " + (gap /10000 / 1000));
}

private static long Sum() 
{
    long sum = 0;

    for(int i = 0; i < 1000000; i++)
    {
        sum += 1;
    }

    return sum;
}
```

시간을 이야기하면서 협정 세계시(UTC : Universal Time, Coordinated)를 빼놓을 수 없습니다. UTC는 예전의 그리니치 평균시 (GMT: Greenwich Mean Time)를 제치고 근래에 새롭게 "세계표준시"로 인정받고 있습니다. 하지만 GMT와 UTC의 시간차가 초의 소수점 아래에 있기 때문에 일반인 입장에서는 크게 영향이 없습니다. 지구의 자전으로 인해 시간차가 발생하는 지역은 "시간대(Time Zone)"를 두어 상대적인 차이를 조정합니다. 따라서 영국의 그리니치 천문대가 위치한 경도 0도를 0시로 정하고 동쪽으로 날짜 분기선(International Date Line)까지 시간대가 증가하고, 서쪽으로는 날짜 분기선까지 시간대가 감소합니다. 영국의 동쪽에 위치하고 날짜 분기선 이전에 있는 대한민국은 시간대가 UTC + 9 에 해당합니다. 즉, 영국이 0 시일때 대한민국은 9시를 가리키고 있는 것입니다. 이 때문에 UTC + 9를 한국 표준시 (KST : Korea Standard Time)라고도 합니다.

시간대가 반영된 것을 지역시간(local time)이라고 합니다. 영국의 경우 UTC와 지역 시간이 동일하지만 시간대가 UTC +9 인 대한민국은 영국이 한밤중일 때 지역 시간은 오전 9시가 됩니다. 따라서 영국을 제외하고는 거의 모든 나라에서 시간을 나타낼 때 UTC인지 지역시간인지를 명시해야만 정확한 시간을 알 수 있습니다. 닷넷의 DateTime 타입은 이 구분을 열거형 타입인 Kind속성으로 알려줍니다.

|열거형 값|설명|
|--|--|
|Unspecified|어떤 형식인지 지정되지 않은 시간|
|Utc|동시간의 그리니치 천문대 시간|
|Local|시간대를 반영한 지역시간|

```C#
class Program
{
    static void Main(string[] args)
    {
        DateTime now = DateTime.Now;
        Console.WriteLine(now + ": " + now.Kind);

        DateTime utcNow = DateTime.UtcNow;
        Console.WriteLine(utcNow + ": " + utcNow.Kind);

        DateTime worldcup2002 = new DateTime(2002, 5, 31);
        Console.WriteLine(worldcup2002 + ": " + worldcup2002.Kind);

        worldcup2002 = new DateTime(2002, 5, 31, 0, 0, 0, DateTimeKind.Local);
        Console.WriteLine(worldcup2002 + ": " + worldcup2002.Kind);

        long javaMillis = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000;
        Console.WriteLine(".NET UTC to Java UTC: " + javaMillis);
    }
}
```

한국의 경우, DateTime.Now 는 지역 시간을 반환하기 때문에 UTC + 9 에 해당하는 값을 가지고 있지만, DateTime.UtcNow는 지금 현재의 그리니치 천문대의 시간을 반환하므로 9시간 뺀 값을 출력합니다. 반면 첫 번째 worldcup2002변수에 담긴 2002년 5월 31일 의 경우 시간대가 정해지지 않았으므로 이 값은 지구의 경도마다 제각기 해석될 수 있는 여지를 남깁니다. 따라서 DateTime 타입의 인스턴스를 생성자를 통해 직접 만들 때는 반드시 그 시간이 UTC기준인지, 지역 시간 기준인지를 명시하는것을 권장합니다.

자바스크립트나 자바 플랫폼과 관련해서 호환상의 이유로 알아둬야 할 용어가 하나 더 있습니다. 닷넷의 DateTime은 시간의 기준값이 1년 1월 1일이지만, 유닉스 및 자바 관련 플랫폼에서는 1970년 1월 1일을 기준으로 합니다. 그 시간을 가리켜 Epoch Time 이라고 하고 다른 말로는 Unix Time, Unix Timestamp, POSIX time 이라고도 합니다. 예를 들어, 자바에서는 System.currentTimeMillis 메서드를 제공하는데, 이 메서드는 현재 시간을 UTC기준의 밀리초 단위로 Epoch 시간 이후로 흐른 값을 반환합니다. 

```JAVA
//자바코드
System.println(System.currentTimeMillis()); //출력 : 1361077426483
```

닷넷의 경우 밀리초 단위의 시간은 다음과 같이 구할 수 있습니다.

```C#
Console.WriteLine(DateTime.UtcNow.Ticks / 10000); // 출력 : 63496674226482
```

그런데 각 코드에 대한 출력값을 보면 너무 심한 차이를 보입니다. 바로 그 차이가 닷넷의 기준시간인 1년 1월 1일과 자바의 기준시간인 1970년 1월 1일 사이의 시간 간격에 해당하는 밀리초입니다. 따라서 자바/자바스크립트에서 구한 밀리초 값을 닷넷에서 구한 값과 정상적으로 비교하려면 다음과 같이 1970년에 해당하는 고정 밀리초값을 빼야 합니다.

```C#
long javaMillis = (DateTime.UtcNow.Ticks - 621355968000000000) / 10000;
```

<br />
<br />
<br />

## 2. System.TimeSpan

DateTime 타입에 대해 사칙 연산 중에서 유일하게 허용되는 것이 "빼기"입니다. 그리고 빼기의 연산 결과 값은 2개의 DateTime 사이의 시간 간격을 나타내는 TimeSpan으로 나옵니다. 

```C#
class Program
{
    static void Main(string[] args)
    {
        DateTime endOfYear = new DateTime(DateTime.Now.Year, 12, 31);
        DateTime now = DateTime.Now;

        Console.WriteLine("오늘 날짜: " + now);

        TimeSpan gap = endOfYear - now;
        Console.WriteLine("올해의 남은 날짜: " + gap.TotalDays);
    }
}
```

TotalDays 말고도, TotalHours, TotalMilliseconds, TotalMinutes, TotalSeconds등의 속성을 통해 손쉽게 해당하는 단위의 시간 간격을 알 수 있습니다.

<br />
<br />
<br />

## 3. System.Diagnostics.Stopwatch

시간차에 대해 DateTime과 TimeSpan을 쓰는 것도 가능하지만, 닷넷에서는 더 정확한 시간차 계산을 위해 Stopwatch 타입을 제공합니다. 

```C#
using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch st = new Stopwatch();
        st.Start();
        Sum();
        st.Stop();

        //st.ElapsedTicks 속성은 구간 사이에 흐른 타이머의 틱(tick) 수
        Console.WriteLine("Total Ticks: " + st.ElapsedTicks);

        //st.ElapsedMilliseconds 속성은 구간 사이에 흐른 시간을 밀리초로 반환
        Console.WriteLine("Millisecond: " + (st.ElapsedMilliseconds));

        //밀리초로 흐른 시간을 초로 환산
        Console.WriteLine("Second: " + (st.ElapsedMilliseconds / 1000));

        //Stop.Frequency 속성이 초당 흐른 틱수를 반환하므로,
        //ElapsedTicks에 대해 나눠주면 초 단위의 시간을 잴 수 있습니다. 
        Console.WriteLine("Second: " + (st.ElapsedTicks / Stopwatch.Frequency));
    }

    private static long Sum()
    {
        long sum = 0;
        for (int i = 0; i < 1000000; i++)
        {
            sum += i;
        }
        return sum;
    }
}
```

실제로 Stopwatch 타입은 코드의 특정 구간에 대한 성능을 측정할 때 자주 사용됩니다. 

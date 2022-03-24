# 2. 문자열 처리 

1. System.String
2. System.Text.StringBuilder
3. System.Text.Encoding
4. System.Text.RegularExpressions.Regex

<hr />
<br />
<br />
<br />

## 1. System.String

문자열 처리는 대부분 string 타입에서 제공됩니다. 자주 사용되는 메서드를 정리하면 다음과 같습니다.

|맴버|유형|설명|
|--|--|--|
|Contains|인스턴스메서드|인자로 전달된 문자열을 포함하고 있는지 여부를 true/false 로 반환|
|EndsWith|인스턴스메서드|인자로 전달된 문자열로 끝나는지 여부를 true/false 로 반환|
|Format|인스턴스메서드|형식에 맞는 문자열을 생성해 반환|
|GetHashCode|인스턴스메서드|문자열의 해시값을 반환|
|IndexOf|인스턴스메서드|문자 또는 문자열을 포함하는 경우 그 위치를 반환하고 없으면 -1을 반환|
|Replace|인스턴스메서드|첫 번째 인자의 문자 또는 문자열을 두 번째 인자의 값으로 치환된 문자열을 반환|
|Split|인스턴스메서드|주어진 문자 또는 문자열을 구분자로 나뉜 문자열의 배열을 반환|
|StartsWith|인스턴스메서드|인자로 전달된 문자열로 시작하는지 여부를 true/false로 반환|
|Substring|인스턴스메서드|시작과 길이에 해당하는 만큼의 문자열을 반환|
|ToLower|인스턴스메서드|문자열을 소문자로 변환해서 반환|
|ToUpper|인스턴스메서드|문자열을 대문자로 변환해서 반환|
|Trim|인스턴스메서드|문자열의 앞뒤에 주어진 문자가 있는 경우 삭제한 문자열을 반환, 문자가 지정되지 않으면 기본적으로 공백 문자를 제거해서 반환|
|Length|인스턴스 속성|문자열의 길이를 정수로 반환|
|!=|정적 연산자|문자열이 같지 않다면 true를 반환|
|==|정적 연산자|문자열이 같다면 true를 반환|
|인덱서[]|인스턴스 속성|주어진 정수 위치에 해당하는 문자를 반환|

아래는 이 중에서 Format메서드를 제외하고 각 메서드를 사용한 예제입니다.

```C#
using System;

class Program
{
    static void Main(string[] args)
    {
        string txt = "Hello World";
        Console.WriteLine(txt + " Contains(\"Hello\"): " + txt.Contains("Hello"));
        Console.WriteLine(txt + " Contains(\"Halo\"): " + txt.Contains("Halo"));
        Console.WriteLine();

        Console.WriteLine(txt + " EndsWith(\"World\"): " + txt.EndsWith("World"));
        Console.WriteLine(txt + " EndsWith(\"ello\"): " + txt.EndsWith("ello"));
        Console.WriteLine();

        Console.WriteLine(txt + " GetHashCode(): " + txt.GetHashCode());
        Console.WriteLine("Hello GetHashCode(): " + "Hello".GetHashCode());
        Console.WriteLine();

        Console.WriteLine(txt + " IndexOf(\"World\"): " + txt.IndexOf("World"));
        Console.WriteLine(txt + " IndexOf(\"Halo\"): " + txt.IndexOf("Halo"));
        Console.WriteLine();

        Console.WriteLine(txt + " Replace(\"World\", \"\"): " + txt.Replace("World", ""));
        Console.WriteLine(txt + " Replace('o', 't'): " + txt.Replace('o', 't'));
        Console.WriteLine();

        Console.Write(txt + " Split('o'): ");
        OutputArrayString(txt.Split('o'));

        Console.Write(txt + " Split(' '): ");
        OutputArrayString(txt.Split(' '));
        Console.WriteLine();

        Console.WriteLine(txt + " StartsWith(\"Hello\"): " + txt.StartsWith("Hello"));
        Console.WriteLine(txt + " StartsWith(\"ello\"): " + txt.StartsWith("ello"));
        Console.WriteLine();

        Console.WriteLine(txt + " Substring(1): " + txt.Substring(1));
        Console.WriteLine(txt + " Substring(2, 3): " + txt.Substring(2, 3));
        Console.WriteLine();

        Console.WriteLine(txt + " ToLower(): " + txt.ToLower());
        Console.WriteLine(txt + " ToUpper(): " + txt.ToUpper());
        Console.WriteLine();

        Console.WriteLine("\" Hello World \" Trim(): " + " Hello World ".Trim());
        Console.WriteLine(txt + " Trim('H'): " + txt.Trim('H'));
        Console.WriteLine(txt + " Trim('d'): " + txt.Trim('d'));
        Console.WriteLine(txt + " Trim('H', 'd'): " + txt.Trim('H', 'd'));
        Console.WriteLine();

        Console.WriteLine(txt + " Length: " + txt.Length);
        Console.WriteLine("Hello Length: " + "Hello".Length);
        Console.WriteLine();

        Console.WriteLine("Hello != World: " + ("Hello" != "World"));
        Console.WriteLine("Hello == World: " + ("Hello" == "World"));
        Console.WriteLine("Hello == HELLO: " + ("Hello" == "HELLO"));
        Console.WriteLine();
    }

    private static void OutputArrayString(string[] arr)
    {
        foreach (string txt in arr)
        {
            Console.Write(txt + ", ");
        }

        Console.WriteLine();
    }
}
```

영문자를 다루면서 빠질 수 없는 사항이 바로 대소문자 구분입니다. 위 표에 나열된 메서드 가운데 대소문자 구분의 오버로드 버전을 제공하는 메서드로 EndsWith, IndexOf, StartsWith가 있습니다. 이 메서드들은 각각 StringComparison열거형 인자를 추가로 받을 수 있습니다. 이 인자를 생략하면 기본적으로 대소문자 구분을 하고, 대소문자 구분을 하고 싶지 않다면 StringComparison, OrdinalIgnoreCase인자를 함께 전달하면 됩니다. 

```C#
using System;

class Program
{
    static void Main(string[] args)
    {
        {
            string txt = "Hello World";

            Console.WriteLine(txt + " EndsWith(\"WORLD\"): " + txt.EndsWith("WORLD", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine();

            Console.WriteLine(txt + " IndexOf(\"WORLD\"): " + txt.IndexOf("WORLD", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine();

            Console.WriteLine(txt + " StartsWith(\"HELLO\"): " + txt.StartsWith("HELLO", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine();
        }

        {
            string txt = "Hello";
            Console.WriteLine(txt + " == HELLO: " + (txt == "HELLO")); // 출력 결과: False
            Console.WriteLine(txt + " == HELLO: " + txt.Equals("HELLO",
            StringComparison.OrdinalIgnoreCase)); // 출력 결과: True
            Console.WriteLine();
        }
    }
}
```
# 3. 링큐

링큐 이전의 코딩에서는 List와 Array형의 자료에서 데이터를 쉽게 뽑아낼수 없었습니다. 이를 좀더 쉽게 뽑아내기 위한아이디어에서 링큐가 탄생했습니다. 링큐는 SQL의 쿼리문과 정말 비슷하게 만들어졌습니다. 따라서 유의할점이 몇가지가 있는데 쿼리문이랑 비슷하다고 해서 쿼리문처럼 쓰면 오류가 나는 부분이 몇군데가 있습니다. 

가령 링큐는 원래 UPDATE라는것이 없습니다. 엄밀히 SQL 에서도 UPDATE는 SELECT 한것중에서 UPDATE하는것이고 DELETE도 마찬가지입니다. JOIN 할때도 ON 뒤에 오는 구문에서 컬럼을 비교해주는데 조인할 컬럼과 조인될 컬럼의 순서가 중요하지 않습니다. 반면 링큐에서는 반드시 조인'될' 컬럼이 먼저오고 그다음 '=' 연산자를 사용할 수 없기 때문에 equals를 사용한뒤 조인'할' 컬럼을 써주어야 합니다. 

<br />

## 처음세팅

처음 셋팅입니다. 3개의 클래스 Product, Order, ProductNOrder는 자료형으로 (테이블과 같음) 쓸 데이터클래스이고 Program은 메인메서드를 가지고 있습니다. 아래코드 다음에 있는 예시들을 실행시키려면 메인메서드에 넣어주기만 하면 됩니다. 

```C#
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public int? ProductId { get; set; }
    public int? Price { get; set; }
}

public class ProductNOrder
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int OrderId { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        //products
        List<Product> products = new List<Product>();
        products.Add(new Product() { ProductId = 1, Name = "Product1", Price = 25 });
        products.Add(new Product() { ProductId = 2, Name = "Product2", Price = 15 });
        products.Add(new Product() { ProductId = 3, Name = "Product3", Price = 20 });

        //orders
        List<Order> orders = new List<Order>();
        orders.Add(new Order() { OrderId = 1, ProductId = 1, Price = 25 });
        orders.Add(new Order() { OrderId = 2, ProductId = 2, Price = 10 });
        orders.Add(new Order() { OrderId = 3, ProductId = 3, Price = 15 });
        orders.Add(new Order() { OrderId = 4, ProductId = null, Price = null });


        //여기에 링큐 구문을 넣어주세요


        Console.ReadKey();
    }
}
```
<br />
<br />
<br />

## 1. 기본사용

아래 코드는 아주 기본적인 사용방법입니다. 

```C#
#region 기본사용001
    var result01 = from p in products
                    where p.Price > 15
                    orderby p.Price ascending
                    select p;

    //값 확인
    Console.WriteLine("값 확인");
    foreach (var item in result01)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
#endregion 기본사용001
```
실행 결과 :
```
값 확인
3 : Product3 : 20
1 : Product1 : 25
```
위의 실행결과를 보면 Product3가 먼저 찍히고 다음에 Product1이 오는것을 알 수 있습니다. 이는 orderby절에서 price에 대한 ascending 효과를 주었기 때문입니다. 따라서 result01 은 IEnumerable<> 인터페이스가 아니라 IOrderedEnumerable<> 인터페이스 형식을 띄게 됩니다.

링큐절을 사용할때 반환값이 IEnumerable<> 일지 IOrderedEnumberanle<> 일지 등등 은 orderby나 select 에서 결정되는데 코딩하다보면 무슨형식이 반환될지는 잘 모를 수 있습니다. 따라서 링큐로 리턴하는값을 받을때는 'var' 로 선언하는게 이롭습니다. 물론 명시적으로 지정할 수 있지만 상당히 복잡해지는 결과를 낼 수 있을것 같습니다.

<br />
<br />
<br />

## 2. LINQ 확장메서드 사용

``` C#
#region Linq 확장메서드사용
    var resultMethod = products.Where(p => p.Price > 15).OrderBy(p => p.Price);
    //값 확인
    Console.WriteLine("값 확인");
    foreach (var item in resultMethod)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
#endregion Linq 확장메서드사용
```

실행 결과 :

```
값 확인
3 : Product3 : 20
1 : Product1 : 25
```

똑같은 값이 나오는데 다른점은 확장메서드를 사용했다는 점입니다. (확장메서드에 관해서 설명-->추후 추가예정)

<br />
<br />
<br />

## 3. 기본사용 CROSS JOIN

아래는 from절 두개를 사용해서 두개의 리스트의 크로스 조인을 실행해보는 예제입니다. cross조인은 두개의 테이블중 하나의 테이블의 각각의 요소에 다른테이블의 모든데이터를 조인한다~ 라는 것입니다. 

```C#
#region 기본사용 cross join
    var result02 = 
                    from p in products
                    from o in orders
                    select new { p, o };
    //값 확인
    Console.WriteLine("값 확인");
    foreach (var item in result02)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.o.OrderId}");
    }
#endregion 기본사용 cross join
```

실행 결과 :

```
값 확인
1 : Product1 : 1
1 : Product1 : 2
1 : Product1 : 3
1 : Product1 : 4
2 : Product2 : 1
2 : Product2 : 2
2 : Product2 : 3
2 : Product2 : 4
3 : Product3 : 1
3 : Product3 : 2
3 : Product3 : 3
3 : Product3 : 4
```

위의 실행결과를 보시면 cross join 이 도출된다는 것을 알 수 있습니다. 그럼 만약 from p in products 와 from o in orders 의 순서를 바꾸면 어떻게 될까 실행해 보았습니다.

```C#
#region 기본사용 cross join
    var result02 = 
                    from o in orders
                    from p in products
                    select new { p, o };
    //값 확인
    Console.WriteLine("값 확인");
    foreach (var item in result02)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.o.OrderId}");
    }
#endregion 기본사용 cross join
```

실행결과 :

```
값 확인
1 : Product1 : 1
2 : Product2 : 1
3 : Product3 : 1
1 : Product1 : 2
2 : Product2 : 2
3 : Product3 : 2
1 : Product1 : 3
2 : Product2 : 3
3 : Product3 : 3
1 : Product1 : 4
2 : Product2 : 4
3 : Product3 : 4
```

이 결과로 부터 알 수 있는것은 result02를 반환할때 from 의 순서에 따라서 다른 결과의 crossjoin 오더를 만든다는 것입니다. 처음에 온 from 절의 모든요소들을 처음부터 하나씩 뽑아서 두번째 from 절의 데이터 전부를 계속 조인하는 방식인것을 알 수 있습니다. 

<br />
<br />
<br />

## 4. 기본사용 INNER JOIN

from 절 다음에 바로 join 절을 쓴다면 LEFT JOIN 이 아니라 기본적으로 INNER JOIN 이 됩니다. 그리고 여기서 주의할 점이 있는데 sql의 쿼리문과는 다르게 on 절에서는 '='기호를 사용할 수 없습니다. equals로 대체해서 사용해야합니다. 더 주의할점은 on 절의 테이블들의 join 순서입니다. 무조건 from 절 에서 가져온 테이블이 먼저 선언되고 equals를 사용한뒤 join 절 에서 가져온 테이블을 선언해야 합니다.... 'ㅁ'!! 아래의 예시에서 그것을 확인할 수 있습니다. 만약 순서가 다르다면 에러를 냅니다. 

```C#
#region 기본사용 inner join
    var result04 = 
                from p in products
                join o in orders on p.ProductId equals o.ProductId
                where o.Price > 10
                select new { p, o };


    Console.WriteLine("값 확인");
    foreach (var item in result04)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.o.Price}");
    }
#endregion 기본사용 inner join
```

실행 결과 :

```
값 확인
1 : Product1 : 25
3 : Product3 : 15
```

<br />
<br />
<br />

## 5. LEFT JOIN

LEFT JOIN 같은 경우는 INTO 문을 사용해야합니다. 여기서 SQL과 다른 문법인데요. 보통 SQL에서 INTO문은 SELECT 와 함께 쓰는것을 알 수 있습니다. LINQ에서 INTO는 JOIN문을 INNER JOIN 에서 GROUP JOIN으로 만들어주는 역할을 합니다. 그리고 DefaultIfEmpty() 메서드를 사용했기 때문에 GROUP JOIN 이 아니라 LEFT JOIN 이 됩니다. 

LINQ 에서는 다른 OUTER JOIN들은 지원하지 않습니다. LEFT JOIN만 지원할 뿐입니다. 외부조인은 기준데이터의 모든데이터를 조인결과에 포함합니다. 기준데이터중에 연결할 데이터와 일치하는 데이터가 없다면 그 부분은 빈값이 들어갑니다. 

(빈값 cf : MSSQL같은경우는 이 값이 NULL 인데 다른 DB에서는 0 일수도 있습니다. LINQ 에서는 NULL입니다.)

```C#
#region left join
    var result05 = 
                    from p in products
                    join o in orders on p.ProductId equals o.ProductId into g
                    from f in g.DefaultIfEmpty()
                    select new { p, f };

    Console.WriteLine("값 확인");
    foreach (var item in result05)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.f?.OrderId}");
    }
#endregion left join
```

실행결과 :

```
값 확인
1 : Product1 : 1
2 : Product2 : 2
3 : Product3 : 3
```

위의 코드를 보면 item.f?.OrderId이 있는데 여기서 "?" 연산자는 ? 앞의값이 null 이 아니라면 ? 뒤의 연산을 실행하라는 뜻입니다. 조인하다보면 값이 null인 경우 혹은 정의되지 않는경우가 있을겁니다. 만약 item.f가 null값이라면 item.f.OrderId 는 에러를 낼겁니다. 따라서 item.f?.OrderId로 정의해줍니다. 

DefaultIfEmpty() 메서드는 확장메서드로서 값이 입력되지않거나 없을때 해야할 예외처리를 해줍니다. 기본값처리를 해주기때문에 메서드 함수안에 값을 넣으면 그 값을 기본값으로 처리합니다. 값을 주지 않으면 0을 기본값으로 반환합니다. 

그런데 위와같은 방법은 여러개의 테이블을 LEFT JOIN 할때 명확하지 않습니다. 위와같은 방식의 문법은 너무 복잡하게 만들어서 여러개의 테이블을 조인할때 햇갈리게 만듭니다. 따라서 아래와 같이 확장메서드를 섞어서 사용한다면 좀더 사람의 눈에 명확하게 들어옵니다.

```C#
#region left join
    var result05 = 
                    from p in products
                    from o in orders
                        .Where(o => o.ProductId == p.ProductId)
                        .DefaultIfEmpty();

    Console.WriteLine("값 확인");
    foreach (var item in result05)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.f?.OrderId}");
    }
#endregion left join
```


<br />
<br />
<br />

## 6. GROUP JOIN

INTO문이 있는걸 GROUP JOIN입니다. GROUP JOIN 은 기준 데이터에 연결데이터가 계층적으로 붙습니다. 따라써 처음 foreach문에서 result06을 돌리면 기준데이터가 연결데이터 수 만큼 돌지 않고 기준데이터 개수만큼 돌게 됩니다. 즉 하나의 기준데이터에 여러개의 견결데이터가 계층적으로 붙습니다. 기준데이터에 연결데이터가 하나도 붙지않으면 데이터가 나타나지 않기 때문에 그룹으로 묶이는점 빼고는 INNER JOIN 이랑 같다고 보면 됩니다. 

```C#
#region group join
    var result06 = 
                    from p in products
                    join o in orders on p.ProductId equals o.ProductId into g
                    select new { p, g };
    Console.WriteLine("값 확인");
    foreach (var item in result06)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.p.Price}");
        foreach (var item2 in item.g)
        {
            Console.WriteLine($"\t{item2.OrderId} : {item2.ProductId} : {item2.Price}");
        }
    }
#endregion group join
```

실행 결과 :

```
값 확인
1 : Product1 : 25
        1 : 1 : 25
2 : Product2 : 15
        2 : 2 : 10
3 : Product3 : 20
        3 : 3 : 15
```

첫번째 foreach문 을 돌렸을때 기준데이터 개수만큼 나오고 연결데이터는 계층적으로 구성됨을 확인할 수 있었습니다. 

<br />
<br />
<br />

## 7. INNER JOIN MULTI 조건

```C#
#region inner join multi조건
    var result07 = 
                    from p in products
                    join o in orders on new { ProductId = p?.ProductId, Price = p?.Price } equals new { ProductId = o?.ProductId, Price = o?.Price }
                    where o.Price > 10
                    select new { p, o };
    Console.WriteLine("값 확인");
    foreach (var item in result07)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.o.Price}");
    }
#endregion inner join multi조건
```

실행 결과 : 

```
값 확인
1 : Product1 : 25
```

<br />
<br />
<br />

## 8. Dictionary LINQ

```C#
#region dictionary linq
    Dictionary<int, List<Order>> dic = new Dictionary<int, List<Order>>();
    List<Order> l1 = new List<Order>();
    l1.Add(new Order() { OrderId = 1, ProductId = 1, Price = 10 });
    l1.Add(new Order() { OrderId = 2, ProductId = 1, Price = 25 });
    dic.Add(1, l1);
    List<Order> l2 = new List<Order>();
    l2.Add(new Order() { OrderId = 3, ProductId = 2, Price = 20 });
    dic.Add(2, l2);

    var result08 = 
                    from d in dic
                    from i in d.Value
                    where i.Price >= 20
                    group i by d.Key into g
                    select g;
    foreach (var item in result08)
    {
        Console.WriteLine($"{item.Key}");
        foreach (var item2 in item)
        {
            Console.WriteLine($"\t{item2.OrderId} : {item2.ProductId} : {item2.Price}");
        }
    }
#endregion dictionary linq
```

실행 결과 : 

```
1
        2 : 1 : 25
2
        3 : 2 : 20
```

<br />
<br />
<br />

## 9. LINQ를 이용한 UPDATE001

```C#
#region linq를 이용한 update001
    var result09 = 
                    from p in products
                    where p.Price > 15
                    select p;
    //값 확인
    Console.WriteLine("값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
    //값 변경
    foreach (var item in result09)
    {
        item.Price += 100;
    }

    Console.WriteLine("변경후 값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
#endregion linq를 이용한 update001
```

실행 결과 : 

```
값 확인
1 : Product1 : 25
2 : Product2 : 15
3 : Product3 : 20
변경후 값 확인
1 : Product1 : 125
2 : Product2 : 15
3 : Product3 : 120
```

<br />
<br />
<br />

## 10. LINQ를 이용한 UPDATE002

```C#
#region linq를 이용한 update002
    값 확인
    Console.WriteLine("값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
    //값 변경
    (
        from p in products
        where p.Price > 15
        select p
    ).ToList().ForEach(x => x.Price += 100);

    Console.WriteLine("변경후 값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
#endregion linq를 이용한 update002
```

실행 결과 : 

```
값 확인
1 : Product1 : 25
2 : Product2 : 15
3 : Product3 : 20
변경후 값 확인
1 : Product1 : 125
2 : Product2 : 15
3 : Product3 : 120
```

<br />
<br />
<br />

## 11. LINQ를 이용한 DELETE001

```C#
#region linq를 이용한 delete001
    값 확인
    Console.WriteLine("값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
    //products.RemoveAll(x => x.Price > 15);

    //값 변경
    (
        from p in products
        where p.Price > 15
        select p
    ).ToList().ForEach(x => { products.Remove(x); });

    Console.WriteLine("변경후 값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
#endregion linq를 이용한 delete001
```

실행 결과 : 

```
값 확인
1 : Product1 : 25
2 : Product2 : 15
3 : Product3 : 20
변경후 값 확인
2 : Product2 : 15
```

<br />
<br />
<br />

## 12. LINQ를 이용한 DELETE002

```C#
#region linq를 이용한 delete002
    값 확인
    Console.WriteLine("products 값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
    Console.WriteLine("orders 값 확인");
    foreach (var item in orders)
    {
        Console.WriteLine($"{item.OrderId} : {item.ProductId} : {item.Price}");
    }

    //값 변경
    var result10 = (
                        from p in products
                        join o in orders on p.ProductId equals o.ProductId
                        where p.Price > 15
                        select new { p, o }
                    ).ToList();

    Console.WriteLine("검색된 products 값 확인");
    foreach (var item in result10)
    {
        Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.p.Price}");
    }
    Console.WriteLine("검색된 orders 값 확인");
    foreach (var item in result10)
    {
        Console.WriteLine($"{item.o.OrderId} : {item.o.ProductId} : {item.o.Price}");
    }

    result10.ForEach(x => { products.Remove(x.p); orders.Remove(x.o); });
    //변경된 결과
    Console.WriteLine("변경후 값 확인");
    foreach (var item in products)
    {
        Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
    }
    Console.WriteLine("orders 값 확인");
    foreach (var item in orders)
    {
        Console.WriteLine($"{item.OrderId} : {item.ProductId} : {item.Price}");
    }
#endregion linq를 이용한 delete002
```

실행 결과 : 

```
products 값 확인
1 : Product1 : 25
2 : Product2 : 15
3 : Product3 : 20
orders 값 확인
1 : 1 : 25
2 : 2 : 10
3 : 3 : 15
4 :  :
검색된 products 값 확인
1 : Product1 : 25
3 : Product3 : 20
검색된 orders 값 확인
1 : 1 : 25
3 : 3 : 15
변경후 값 확인
2 : Product2 : 15
orders 값 확인
2 : 2 : 10
4 :  :
```
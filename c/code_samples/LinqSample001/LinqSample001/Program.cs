using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSample001
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { ProductId = 1, Name = "Product1", Price = 25 });
            products.Add(new Product() { ProductId = 2, Name = "Product2", Price = 15 });
            products.Add(new Product() { ProductId = 3, Name = "Product3", Price = 20 });

            List<Order> orders = new List<Order>();
            orders.Add(new Order() { OrderId = 1, ProductId = 1, Price = 25 });
            orders.Add(new Order() { OrderId = 2, ProductId = 1, Price = 10 });
            orders.Add(new Order() { OrderId = 3, ProductId = 2, Price = 15 });
            orders.Add(new Order() { OrderId = 4, ProductId = null, Price = null });

            #region 기본사용001
            //var result01 = from p in products
            //               where p.price > 15
            //               orderby p.price ascending
            //               select p;

            ////값 확인
            //console.writeline("값 확인");
            //foreach (var item in result01)
            //{
            //    console.writeline($"{item.productid} : {item.name} : {item.price}");
            //}
            #endregion 기본사용001

            #region Linq 확장메서드사용
            //var resultMethod = products.Where(p => p.Price > 15).OrderBy(p => p.Price);
            ////값 확인
            //Console.WriteLine("값 확인");
            //foreach (var item in resultMethod)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            #endregion Linq 확장메서드사용

            #region 기본사용 cross join
            //var result02 =
            //       from o in orders
            //       from p in products
            //       select new { p, o };
            ////값 확인
            //Console.WriteLine("값 확인");
            //foreach (var item in result02)
            //{
            //    Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.o.OrderId}");
            //}
            #endregion 기본사용 cross join

            #region 기본사용 inner join
            //var result04 = from p in products
            //               join o in orders on p.ProductId equals o.ProductId
            //               where o.Price > 10
            //               select new { p, o };


            //Console.WriteLine("값 확인");
            //foreach (var item in result04)
            //{
            //    Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.o.Price}");
            //}
            #endregion 기본사용 inner join

            #region left join
            var result05 = 
                            from p in products
                           join o in orders on p.ProductId equals o.ProductId into g
                           from f in g.DefaultIfEmpty()
                           select new { p, g };

            Console.WriteLine("값 확인");
            foreach (var item in result05)
            {
                Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.g}");
            }

            #endregion left join

            #region group join
            var result06 = from p in products
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

            #region inner join multi조건
            //var result07 = from p in products
            //               join o in orders on new { ProductId = p?.ProductId, Price = p?.Price } equals new { ProductId = o?.ProductId, Price = o?.Price }
            //               where o.Price > 10
            //               select new { p, o };
            //Console.WriteLine("값 확인");
            //foreach (var item in result07)
            //{
            //    Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.o.Price}");
            //}
            #endregion inner join multi조건

            #region dictionary linq
            //Dictionary<int, List<Order>> dic = new Dictionary<int, List<Order>>();
            //List<Order> l1 = new List<Order>();
            //l1.Add(new Order() { OrderId = 1, ProductId = 1, Price = 10 });
            //l1.Add(new Order() { OrderId = 2, ProductId = 1, Price = 25 });
            //dic.Add(1, l1);
            //List<Order> l2 = new List<Order>();
            //l2.Add(new Order() { OrderId = 3, ProductId = 2, Price = 20 });
            //dic.Add(2, l2);

            //var result08 = from d in dic
            //               from i in d.Value
            //               where i.Price >= 20
            //               group i by d.Key into g
            //               select g;
            //foreach (var item in result08)
            //{
            //    Console.WriteLine($"{item.Key}");
            //    foreach (var item2 in item)
            //    {
            //        Console.WriteLine($"\t{item2.OrderId} : {item2.ProductId} : {item2.Price}");
            //    }
            //}
            #endregion dictionary linq

            #region linq를 이용한 update001
            //var result09 = from p in products
            //               where p.Price > 15
            //               select p;
            ////값 확인
            //Console.WriteLine("값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            ////값 변경
            //foreach (var item in result09)
            //{
            //    item.Price += 100;
            //}

            //Console.WriteLine("변경후 값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}

            #endregion linq를 이용한 update001

            #region linq를 이용한 update002
            //값 확인
            //Console.WriteLine("값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            ////값 변경
            //(from p in products
            // where p.Price > 15
            // select p).ToList().ForEach(x => x.Price += 100);

            //Console.WriteLine("변경후 값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            #endregion linq를 이용한 update002

            #region linq를 이용한 delete001
            //값 확인
            //Console.WriteLine("값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            ////products.RemoveAll(x => x.Price > 15);

            ////값 변경
            //(from p in products
            // where p.Price > 15
            // select p).ToList().ForEach(x => { products.Remove(x); });

            //Console.WriteLine("변경후 값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            #endregion linq를 이용한 delete001

            #region linq를 이용한 delete002
            //값 확인
            //Console.WriteLine("products 값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            //Console.WriteLine("orders 값 확인");
            //foreach (var item in orders)
            //{
            //    Console.WriteLine($"{item.OrderId} : {item.ProductId} : {item.Price}");
            //}

            ////값 변경
            //var result10 = (from p in products
            //                join o in orders on p.ProductId equals o.ProductId
            //                where p.Price > 15
            //                select new { p, o }).ToList();
            //Console.WriteLine("검색된 products 값 확인");
            //foreach (var item in result10)
            //{
            //    Console.WriteLine($"{item.p.ProductId} : {item.p.Name} : {item.p.Price}");
            //}
            //Console.WriteLine("검색된 orders 값 확인");
            //foreach (var item in result10)
            //{
            //    Console.WriteLine($"{item.o.OrderId} : {item.o.ProductId} : {item.o.Price}");
            //}

            //result10.ForEach(x => { products.Remove(x.p); orders.Remove(x.o); });
            ////변경된 결과
            //Console.WriteLine("변경후 값 확인");
            //foreach (var item in products)
            //{
            //    Console.WriteLine($"{item.ProductId} : {item.Name} : {item.Price}");
            //}
            //Console.WriteLine("orders 값 확인");
            //foreach (var item in orders)
            //{
            //    Console.WriteLine($"{item.OrderId} : {item.ProductId} : {item.Price}");
            //}
            #endregion linq를 이용한 delete002

            Console.ReadKey();
        }
    }

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
}


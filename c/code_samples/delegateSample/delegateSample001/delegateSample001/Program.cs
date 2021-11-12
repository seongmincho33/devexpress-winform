using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegateSample001
{
    class Program
    {
        public delegate void DeleCalculation(int v1, int v2);

        #region 일반적인 경우
        //static void Main(string[] args)
        //{
        //    Add(1, 2);

        //    Console.ReadKey();
        //}
        #endregion 일반적인 경우

        #region delegate 사용

        //static void Main(string[] args)
        //{
        //    DeleCalculation Calc;
        //    Calc = Add;
        //    Calc += Multi;
        //    Calc(1, 2);

        //    Console.ReadKey();
        //}

        #endregion delegate 사용

        #region 언제사용하지
        //메서드의 parameter로 메서드를 넘겨주고 싶을때
        //static void Main(string[] args)
        //{
        //    Run(Add);
        //    Run(Multi);

        //    Console.ReadKey();
        //}
        #endregion 언제사용하지

        #region 어디서 본듯한 delegate

        static void Main(string[] args)
        {
            EventHandler Calc = null;
            Calc += txtBox_Clicked;
            Calc(new object(), new EventArgs());

            Console.ReadKey();
        }


        #endregion 어디서 본듯한 delegate

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

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace delegateSample001
//{
//    class Program2
//    {
//        public delegate void DeleCalculation(int v1, int v2);
//        static void Main(string[] args)
//        {
//            Console.WriteLine("인자 두개를 입력하세요");
//            string[] input_value = Console.ReadLine().Split();
//            Console.WriteLine("무슨연산을 하시고싶으신가요?\n(더하기 : Add, 곱하기 : Multi)");
//            string input_calculate = Console.ReadLine();

//            if(input_calculate == "Add")
//            {
//                Calculator(Add, input_value);
//            }
//            else if(input_calculate == "Multi")
//            {
//                Calculator(Multi, input_value);
//            }

//            Console.ReadKey();
//        }

//        static void Calculator(DeleCalculation deleCalculate, string[] input_value)
//        {
//            Console.WriteLine("계산기를 실행시켜줍니다.");
//            deleCalculate(Int32.Parse(input_value[0]), Int32.Parse(input_value[1]));
//        }

//        static void Add(int value1, int value2)
//        {
//            Console.WriteLine($"더하기 : {value1 + value2}");
//        }

//        static void Multi(int value1, int value2)
//        {
//            Console.WriteLine($"곱하기 : {value1 * value2}");
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace DezeroMath
{
    public class MathFactory
    {        
        public Variable<Matrix<double>> Square(Variable<Matrix<double>> matrix)
        {
            return new Square<Matrix<double>>().Call(matrix);
        }

        public Variable<Matrix<double>> exp(Variable<Matrix<double>> matrix)
        {
            return new Exp<Matrix<double>>().Call(matrix);
        }
    }
    public class Variable<T> where T : Matrix<double>
    {
        private T _data;
        public T Data
        {
            get { return _data; }
            set { _data = value; }
        }

        private T _gradient;
        public T Gradient
        {
            get { return _gradient; }
            set { _gradient = value; }
        }
        //public delegate Variable<T> CreatorEventHandler(Variable<T> input);
        //public event CreatorEventHandler Creator;

        private Function<T> _creator;
        public Function<T> Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        public Variable(T data)
        {
            this.Data = data;
            this.Gradient = default;
            this.Creator = default;
        }

        public void Backward()
        {
            #region 재귀방식 
            //var f = this.Creator; //1. 함수를 가져온다.
            //if(f != null)
            //{
            //    var x = f.Input; //2. 함수의 입력을 가져온다.
            //    x.Gradient = f.Backward(this.Gradient); //3. 함수의 BackWard메서드를 호출한다.
            //    x.Backward(); // 하나 앞 변수의 backward메서드를 호출한다(재귀)
            //}
            #endregion

            if(this.Gradient == null)
            {
                this.Gradient = (T)this.Data.Clone();
                for (int i = 0; i < this.Gradient.RowCount; i++)
                {
                    for (int j = 0; j < this.Gradient.ColumnCount; j++)
                    {
                        this.Gradient[i, j] = 1;
                    }
                };
            }

            Stack<Function<T>> funcs = new Stack<Function<T>>();
            funcs.Push(this.Creator);

            while(funcs.Count > 0)
            {
                var f = funcs.Pop();
                var x = f.Input;
                var y = f.Output;
                x.Gradient = f.Backward(y.Gradient);

                if(x.Creator != null)
                {
                    funcs.Push(x.Creator);
                }
            }
        }
  
    }

    public abstract class Function<T> where T : Matrix<double>
    {
        public Variable<T> Input;
        public Variable<T> Output;
        public Variable<T> Call(Variable<T> input)
        {
            var x = input.Data;
            var y = this.Forward(x);
            var output = new Variable<T>(y);
            output.Creator = this; // 출력 변수에 창조자를 설정한다.
            this.Input = input;
            this.Output = output; //출력 저장

            return output;
        }
        public abstract T Forward(T x);
        public abstract T Backward(T gy);
    }

    public class Square<T> : Function<T> where T : Matrix<double>
    {      
        public override T Forward(T x)
        {
            //dynamic y = Math.Pow(Convert.ToDouble(x), 2);            
            dynamic y = x.Clone();
            for(int i = 0; i < y.RowCount; i++)
            {
                for(int j = 0; j < y.ColumnCount; j++)
                {
                    y[i, j] = Math.Pow(y[i, j], 2);
                }
            }

            return y;
        }

        public override T Backward(T gy)
        {
            dynamic x = base.Input.Data;
            dynamic gx = 2 * x * gy;
            return gx;
        }
    }

    public class Exp<T> : Function<T> where T : Matrix<double>
    {
        public override T Forward(T x)
        {
            //dynamic y = Math.Exp(Convert.ToDouble(x));

            dynamic y = x.Clone();
            for (int i = 0; i < y.RowCount; i++)
            {
                for (int j = 0; j < y.ColumnCount; j++)
                {
                    y[i, j] = Math.Exp(y[i, j]);
                }
            }

            return y;
        }

        public override T Backward(T gy)
        {
            var x = base.Input.Data;
            //dynamic gx = Math.Exp(x) * gy;
            dynamic gx = Matrix.Exp(x) * gy;
            return gx;
        }
    }
}

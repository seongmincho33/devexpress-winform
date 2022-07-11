using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DezeroSharp
{
    public class Variable
    {
        private int[ , ] _data;
        public int[ , ] Data { get { return _data; } set { _data = value; } }
        public Variable(int[ , ] data)
        {
            this._data = data;
        }
    }

    //public class Function
    //{
    //    public Function(Variable input)
    //    {
    //        int[,] x = input.Data;
    //        int[,] y = null;
            
    //        Math.m

    //        foreach (var v in x)
    //        {
    //            Math.Pow(v, 2);
    //        }
            

    //        return y;
    //    }
    //}

    public static class ArrayExtensions
    {
        public static void Set<T>(this Array array, T defaultValue)
        {
            int[] indicies = new int[array.Rank];

            SetDimension<T>(array, indicies, 0, defaultValue);
        }

        private static void SetDimension<T>(Array array, int[] indicies, int dimension, T defaultValue)
        {
            for (int i = 0; i <= array.GetUpperBound(dimension); i++)
            {
                indicies[dimension] = i;

                if (dimension < array.Rank - 1)
                    SetDimension<T>(array, indicies, dimension + 1, defaultValue);
                else
                    array.SetValue(defaultValue, indicies);
            }
        }
    }
}

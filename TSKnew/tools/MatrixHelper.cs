using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKnew.tools
{
    public static class MatrixHelper
    {
        public static double[,] ToMatrix(double[][] arr2)
        {
            var result = new double[arr2.Length, arr2[0].Length];
            for (int i = 0; i < arr2.Length; i++)
            {
                for (int j = 0; j < arr2[0].Length; j++)
                {
                    result[i, j] = arr2[i][j];
                }
            }
            return result;
        }

        internal static double[,] CreateMatrixA(List<List<double>> wSets, List<List<double>> xSet)
        {
            double[][] a = new double[xSet.Count][];
            for (int setIndex = 0; setIndex < xSet.Count; setIndex++)
            {
                a[setIndex] = createMatrixALine(wSets[setIndex], xSet[setIndex]);
            }
            return MatrixHelper.ToMatrix(a);
        }

        private static double[] createMatrixALine(List<double> w, List<double> x)
        {
            var str = new List<double>();

            for (int i = 0; i < w.Count; i++)
            {
                str.Add(w[i]);
                for (int j = 0; j < x.Count; j++)
                {
                    str.Add(w[i] * x[j]);
                }
            }
            return str.ToArray();
        }
    }
}

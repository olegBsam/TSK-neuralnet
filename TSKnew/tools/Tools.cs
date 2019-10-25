using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKnew.tools
{
    public static class Tools
    {
        public static double[][] CreateArr2(int i, int j)
        {
            var result = new double[i][];
            for (int k = 0; k < i; k++)
            {
                result[k] = new double[j];
            }
            return result;
        }

        private static Random rnd = new Random();
        public static Random Rnd { get { return rnd;  } }

        public static double[] GetRandomArray(int size) =>
             (new double[size])
                .Select(o => rnd.NextDouble())
                .ToArray();

        internal static double[] GetW_(double[][] v)
        {
            double[] w = new double[v.Length];
            double sum = v.Select(t1 => t1.Aggregate((p, o) => p *= o)).Sum();
            for (int i = 0; i < w.Length; i++)
            {
                w[i] = v[i].Aggregate((p, o) => p *= o) / sum;
            }
            return w;
        }

        internal static double Kronecker(int k, int h) =>
            k == h ? 1.0 : 0.0;

        internal static double Gauss(double x, double b, double c, double s, double r = 0) =>
            Math.Pow((x - c) / s, 2 * b + r);
        
    }
}

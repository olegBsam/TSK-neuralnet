using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKnew.tools
{
    public static class NeuronsInitTools
    {
        public static Tuple<List<double[]>, double[]> GetInitRBFParams(Dictionary<double, List<double[]>> xSet, int n)
        {
            if (xSet.Keys.Count == 2)
            {
                var center1 = new double[n];
                var center2 = new double[n];
                var center3 = new double[n];
                var sigma = new double[n];

                for (int i = 0; i < n; i++)
                {
                    center1[i] = xSet.First().Value.Select(o => o[i]).Average();
                    center3[i] = xSet.Last().Value.Select(o => o[i]).Average();
                    center2[i] = (center1[i] + center3[i]) / 2.0;
                    sigma[i] = Math.Abs(center1[i] - center2[i]);
                }
                return new Tuple<List<double[]>, double[]>(new List<double[]>() { center1, center2, center3 }, sigma);
            }
            else throw new Exception("FirstLayerNeuronExt: InitCentrs");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKnew.neuronet.layers
{
    public class SecondLayer
    {
        public int M { get; private set; }
        public int N { get; private set; }
        public SecondLayer(int m, int n) { M = m; N = n; }

        public double[] Calculate(double[][] nonLinearWeight) =>
            nonLinearWeight
                .Select(o => o
                    .Aggregate((p, x) => p *= x))
                .ToArray();

    }
}

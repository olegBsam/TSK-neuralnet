using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKnew.neuronet.neurons
{
    public class ThirdLayersNeuron
    {
        public double[] P { get; set; }

        public ThirdLayersNeuron(int n)
        {
            P = tools.Tools.GetRandomArray(n + 1);
        }

        public double Calculate(double w, double[] x) =>
            CalculateY(x) * w;

        public double CalculateY(double[] x) =>
            tools.NeuronsCalculate.CalculateY(x, P);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSKnew.neuronet.neurons;

namespace TSKnew.tools
{
    public static class NeuronsCalculate
    {
        public static double CalculateY(double[] x, double[] p)
        {
            double result = p[0];
            for (int i = 0; i < x.Length; i++)
            {
                result += x[i] * p[i + 1];
            }
            return result;
        }

        public static double CalculateMu(double x, double c, double sigma, double b) =>
            1.0 / (1.0 + Math.Pow((x - c) / sigma, 2 * b));

        public static double CalculateL(List<double> x, FirstLayerNeuron neuron, int withoutIndex = -1) =>
            x
               .Select((t1, t2) =>
                    withoutIndex == t2 ? 1 : neuron.Calculate(t2, t1))
               .Aggregate((t3, t4) => t3 *= t4);

        public static double CalculateM(List<double> x, FirstLayerNeuron[] neurons) =>
            neurons
                .Select(t => x
                    .Select((t1, t2) => t.Calculate(t2, t1))
                    .Aggregate((t3, t4) => t3 *= t4))
                .Sum();

        private static double dEdParam(int h, int p, List<double> x, FirstLayerNeuron[] firstLayerNeurons, int r, int m, ThirdLayersNeuron[] thirdLayersNeurons, double error)
        {
            double sum = 0;
            for (int k = 0; k < m; k++)
            {
                sum += thirdLayersNeurons[k].CalculateY(x.ToArray()) * tools.NeuronsCalculate.dWdParam(h, p, k, x, firstLayerNeurons, r) * error;
            }
            return sum;
        }
        private static double dWdParam(int h, int p, int k, List<double> x, FirstLayerNeuron[] neurons, int r)
        {
            var delta = tools.Tools.Kronecker(k, h);
            var l = tools.NeuronsCalculate.CalculateL(x, neurons[h]);
            var m = tools.NeuronsCalculate.CalculateM(x, neurons);
            var b = neurons[h].BList[p];
            var s = neurons[h].SList[p];
            var c = neurons[h].CList[p];

            var v1 = (delta * m - l) / Math.Pow(m, 2) * tools.NeuronsCalculate.CalculateL(x, neurons[h], p);
            var v2 = (((2.0 * b) / s) * tools.Tools.Gauss(x[p], b, c, s, r)) / Math.Pow(1.0 + tools.Tools.Gauss(x[p], b, c, s), 2);
            var v3 = v1 * v2;
            return v1 * v2;
        }

        public static double CorrectParam(double lastValue, double learningCoef, double error, List<double> x, int h, int p, FirstLayerNeuron[] firstLayerNeurons, ThirdLayersNeuron[] thirdLayersNeurons, int r, int m) =>
            lastValue - learningCoef * tools.NeuronsCalculate.dEdParam(h, p, x, firstLayerNeurons, r, m, thirdLayersNeurons, error);

    }
}

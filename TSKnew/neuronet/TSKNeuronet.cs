using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSKnew.neuronet.layers;
using TSKnew.neuronet.neurons;
using Accord.Math;

using LearningSet = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<double, System.Collections.Generic.List<double>>>;
namespace TSKnew.neuronet
{
    public class TSKNeuronet
    {
        private FirstLayer firstLayer;
        private SecondLayer secondLayer;
        private ThirdLayer thirdLayer;
        private FourthLayer fourthLayer;

        public int M { get; private set; }
        public int N { get; private set; }

        double LearningCoef { get; set; } = 0.2;

        public TSKNeuronet(int n, Dictionary<double, List<double[]>> xSet, int m)
        {
            M = m; N = n;
            firstLayer = new FirstLayer(n, xSet);
            secondLayer = new SecondLayer(m, n);
            thirdLayer = new ThirdLayer(m, n);
            fourthLayer = new FourthLayer();
        }

        public double Calculation(double[] x)
        {
            var wij = firstLayer.Calculate(x);
            var w = secondLayer.Calculate(wij);
            var y = thirdLayer.Calculate(w, x);
            var result = fourthLayer.Calculate(w, y);
            return result;
        }

        public void Learning(LearningSet learningSet, int iterationCount)
        { 
            for (int epoch = 0; epoch < iterationCount; epoch++)
            {

                var _wSets = learningSet
                    .Select(t1 => tools.Tools.GetW_(firstLayer
                        .Calculate(t1.Value
                            .ToArray()))
                        .ToList())
                    .ToList();

                var y = FirstLearningStep(_wSets, learningSet);
                SecondLearningStep(y, learningSet);
            }           
        }

        public int Test(LearningSet testSet)
        {
            var results = new List<double[]>();
            try
            {
                for (int i = 0; ; i++)
                {
                    results.Add(new double[] { Calculation(testSet[i].Value.ToArray()), testSet[i].Key });
                }
            }
            catch { }

            return results.Where(t1 => Math.Abs(t1[1] - t1[0]) > 0.5).Count();
        }

        private double[] FirstLearningStep(List<List<double>> _wSets, List<KeyValuePair<double, List<double>>> xSet)
        {
            var a = tools.MatrixHelper.CreateMatrixA(_wSets, xSet.Select(t1 => t1.Value).ToList());
            var d = xSet.Select(t1 => t1.Key).ToArray();
            var newP = a.PseudoInverse().Dot(d);
            thirdLayer.SetP(newP);
            return  a.Dot(newP);
        }

        private void SecondLearningStep(double[] y, LearningSet xSet)
        {
            for (int setIndex = 0; setIndex < y.Length; setIndex++)
            {
                (var newCenters, var newSigmas) = CorrectParams(y[setIndex], xSet[setIndex].Key, xSet[setIndex].Value, firstLayer.Neurons);

                firstLayer.SetCenters(newCenters);
                firstLayer.SetSigmas(newSigmas);
            }
        }

        private (List<List<double>>, List<List<double>>) CorrectParams(double y, double d, List<double> x, FirstLayerNeuron[] neurons)
        {
            var newCenters = new List<List<double>>();
            var newSigmas = new List<List<double>>();

            for (int h = 0; h < neurons.Length; h++)
            {
                newCenters.Add(new List<double>());
                newSigmas.Add(new List<double>());
                for (int p = 0; p < neurons[h].CList.Length; p++)
                {
                    newCenters.Last().Add(tools.NeuronsCalculate.CorrectParam(neurons[h].CList[p], LearningCoef, y - d, x, h, p, firstLayer.Neurons, thirdLayer.Neurons, -1, M));
                    newSigmas.Last().Add(tools.NeuronsCalculate.CorrectParam(neurons[h].SList[p], LearningCoef, y - d, x, h, p, firstLayer.Neurons, thirdLayer.Neurons, 0, M));
                }
            }
            return (newCenters, newSigmas);
        }
    }
}

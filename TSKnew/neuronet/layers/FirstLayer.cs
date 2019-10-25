using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSKnew.neuronet.neurons;
using TSKnew.tools;

namespace TSKnew.neuronet.layers
{
    public class FirstLayer
    {
        public FirstLayerNeuron[] Neurons { get; set; }

        public FirstLayer(int n, Dictionary<double, List<double[]>> xSet)
        {
            Neurons = new FirstLayerNeuron[3];
            var initParams = NeuronsInitTools.GetInitRBFParams(xSet, n);

            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i] = new FirstLayerNeuron(n);
                Neurons[i].CList = initParams.Item1[i];
                Neurons[i].SList = initParams.Item2;
            }
        }

        public double[][] Calculate(double[] x)
        {
            var result = Tools.CreateArr2(Neurons.Length, x.Length);

            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < Neurons.Length; j++)
                {
                    result[j][i] = Neurons[j].Calculate(i, x[i]);
                }
            }

            return result;
        }

        internal void SetSigmas(List<List<double>> newSigmas)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                for (int j = 0; j < Neurons[i].SList.Length; j++)
                {
                    Neurons[i].SList[j] = newSigmas[i][j];
                }
            }
        }

        internal void SetCenters(List<List<double>> newCenters)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                for (int j = 0; j < Neurons[i].CList.Length; j++)
                {
                    Neurons[i].CList[j] = newCenters[i][j];
                }
            }
        }
    }
}

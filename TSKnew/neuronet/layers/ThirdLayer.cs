using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSKnew.neuronet.neurons;

namespace TSKnew.neuronet.layers
{
    public class ThirdLayer
    {
        public ThirdLayersNeuron[] Neurons { get; set; }

        public ThirdLayer(int m, int n)
        {
            Neurons = new ThirdLayersNeuron[m];
            for (int i = 0; i < m; i++)
            {
                Neurons[i] = new ThirdLayersNeuron(n);
            }
        }

        public double Calculate(double[] w, double[] x)
        {
            double res = 0;
            for (int i = 0; i < w.Length; i++)
            {
                res += Neurons[i].Calculate(w[i], x);
            }
            return res;
        }

        internal void SetP(double[] newP)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                var vectorP = new double[Neurons[i].P.Length];
                Array.Copy(newP, i * Neurons[i].P.Length, vectorP, 0, Neurons[i].P.Length);
                Neurons[i].P = vectorP;
            }
        }
    }
}

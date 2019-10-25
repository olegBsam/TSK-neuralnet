using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKnew.neuronet.neurons
{
    public class FirstLayerNeuron
    {

        public double[] CList { get; set; } 
        public double[] BList { get; set; }
        public double[] SList { get; set; }

        public FirstLayerNeuron(int n)
        {
            CList = new double[n];
            BList = new double[n];
            BList = BList.Select(o => 1.0).ToArray();
            SList = new double[n];
        }

        internal double Calculate(int compIndex, double x) =>
            tools.NeuronsCalculate.CalculateMu(x, CList[compIndex], SList[compIndex], BList[compIndex]);
    }
}

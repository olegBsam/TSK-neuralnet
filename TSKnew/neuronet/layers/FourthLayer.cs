using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSKnew.neuronet.layers
{
    public class FourthLayer
    {
        public double Calculate(double[] w, double f1) => f1 / w.Sum();
    }
}

using dvincija_zadaca_1.DiverApp.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Algorithm
{
    public abstract class AlgorithmFactory
    {
        public abstract DiveAlgorithmProduct createAlgorithm(string type);
    }
}

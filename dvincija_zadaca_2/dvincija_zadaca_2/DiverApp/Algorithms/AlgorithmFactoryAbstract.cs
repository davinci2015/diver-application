using dvincija_zadaca_1.DiverApp.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Algorithms
{
    public abstract class AlgorithmFactoryAbstract
    {
        public abstract DiveAlgorithmProductAbstract createAlgorithm(string type);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp.Algorithms.ConcreteProducts;

namespace dvincija_zadaca_1.DiverApp.Algorithms
{
    public class DiveAlgorithmFactory : AlgorithmFactoryAbstract
    {
        public override DiveAlgorithmProductAbstract createAlgorithm(string type)
        {
            switch(type)
            {
                case "AlgMaksUron":
                    return new MaxDepthAlg();
                case "AlgIstaKategUron":
                    return new PartnerAtSameLevelAlg();
                case "AlgSlucUron":
                    return new RandomAlg();
                default:
                    Console.WriteLine("Warning\n{0} algorithm not found! Using AlgMaksUron instead\n", type);
                    return new MaxDepthAlg();
            }
        }
    }
}

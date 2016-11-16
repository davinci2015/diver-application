using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp.Algorithm.ConcreteProducts;

namespace dvincija_zadaca_1.DiverApp.Algorithm
{
    public class DiveAlgorithmFactory : AlgorithmFactory
    {
        public override DiveAlgorithmProduct createAlgorithm(string type)
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

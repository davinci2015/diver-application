using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Algorithm
{
    public class DiveAlgorithmFactory : AlgorithmFactory
    {
        public override DiveAlgorithmProduct createAlgorithm(string type)
        {

            switch(type)
            {
                case "MaxDepthAlg":
                    Console.WriteLine("Max --------------------------------");
                    return new MaxDepthAlg();
                case "PartnerAtSameLevelAlg":
                    return new PartnerAtSameLevelAlg();
                case "RandomAlg":
                    Console.WriteLine("Random --------------------------------");
                    return new RandomAlg();
                default:
                    return new MaxDepthAlg();
            }
            
        }
    }
}

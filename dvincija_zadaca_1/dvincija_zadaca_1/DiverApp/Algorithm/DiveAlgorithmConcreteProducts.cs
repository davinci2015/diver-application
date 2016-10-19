using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Algorithm
{
    public class MaxDepthAlg : DiveAlgorithmProduct
    {
        public override List<PairHelper> GetDivePairs(List<Diver> divers, int numOfDivers)
        {
            List<PairHelper> pairList = new List<PairHelper>();

            // Sort diver by depth determined by certification
            divers = divers.OrderBy(x => x.certificate.depth).ToList();

            int start = 0;
            int end = divers.Count() - 1;

            while(numOfDivers > 0)
            {
                if (numOfDivers % 2 == 0)
                {
                    numOfDivers -= 2;
                    pairList.Add(new PairHelper (new List<Diver>() { divers[start++], divers[end--] } ));
                }
                else
                {
                    numOfDivers -= 3;
                    pairList.Add(new PairHelper(new List<Diver>() { divers[start], divers[start + 1], divers[end--] }));
                    start += 2;
                }
            }

            return pairList;
        }
    }
    public class PartnerAtSameLevelAlg : DiveAlgorithmProduct
    {
        public override List<PairHelper> GetDivePairs(List<Diver> diver, int numOfDivers)
        {
            throw new NotImplementedException();
        }
    }
    public class RandomAlg : DiveAlgorithmProduct
    {
        public override List<PairHelper> GetDivePairs(List<Diver> diver, int numOfDivers)
        {
            throw new NotImplementedException();
        }
    }
}

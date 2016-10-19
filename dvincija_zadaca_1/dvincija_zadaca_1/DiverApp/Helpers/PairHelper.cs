using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Helpers
{
    public class PairHelper
    {
        public int maxDepth;
        public List<Diver> diverPair = new List<Diver>();

        public PairHelper(List<Diver> divers, int maxDiveDepth) 
        {
            this.diverPair = divers;
            this.maxDepth = maxDiveDepth;
            int[] depthArr = divers.Select(x => x.certificate.depth).ToArray();
            this.CalculateDepth(depthArr);
        }

        private void CalculateDepth(int[] depthArr)
        {
            int depthForDivers;
            int maxInterval = depthArr.SelectMany((a) => depthArr.Select((b) => Math.Abs(a - b))).Max();

            if (maxInterval >= 20)
            {
                depthForDivers = depthArr.Min() + 10;
            }
            else
            {
                depthForDivers = depthArr.Max() == 40 ? 40 : depthArr.Max() + 10;
            }
                
            maxDepth = maxDepth < depthForDivers ? maxDepth : depthForDivers;
        }
    }
}

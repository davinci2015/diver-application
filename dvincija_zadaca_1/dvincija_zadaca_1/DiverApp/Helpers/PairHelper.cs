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

        public PairHelper(List<Diver> divers) 
        {
            this.diverPair = divers;
            int[] depthArr = divers.Select(x => x.certificate.depth).ToArray();
            this.CalculateDepth(depthArr);
        }

        private void CalculateDepth(int[] depthArr)
        {
            int depth;
            int maxInterval = depthArr.SelectMany((a) => depthArr.Select((b) => Math.Abs(a - b))).Max();

            if (maxInterval >= 20)
            {
                depth = depthArr.Min() + 10;
            }
            else
            {
                depth = depthArr.Max() == 40 ? 40 : depthArr.Max() + 10;
            }
                
            this.maxDepth = depth;
        }
    }
}

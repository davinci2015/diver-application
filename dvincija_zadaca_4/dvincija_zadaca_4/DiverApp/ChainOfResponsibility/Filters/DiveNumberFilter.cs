using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_4.DiverApp.Main;

namespace dvincija_zadaca_4.DiverApp.ChainOfResponsibility.Filters
{
    public class DiveNumberFilter : IFilterChain
    {
        IFilterChain nextFilter;
        public IFilterChain NextChain { set { nextFilter = value; } }
        public void FilterDivers(List<Diver> divers, int numOfDiversToRemove)
        {
            int max = divers.Max(x => x.NumOfDives);
            List<Diver> diversToRemove = divers.Where(x => x.NumOfDives == max).ToList();

            foreach (Diver diverToRemove in diversToRemove)
            {
                if (numOfDiversToRemove-- > 0) divers.Remove(diverToRemove);
                else break;
            }
            
            if (numOfDiversToRemove > 0)
                nextFilter.FilterDivers(divers, numOfDiversToRemove);
        }
    }
}

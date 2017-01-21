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

        /// <summary>
        /// Filter divers by number of dives
        /// Divers with less dives will not be deleted.
        /// </summary>
        /// <param name="divers">List of divers</param>
        /// <param name="numOfDiversToRemove">Number of divers to delete</param>
        public void FilterDivers(List<Diver> divers, int numOfDiversToRemove)
        {
            // Find maximum number of dives per diver
            int max = divers.Max(x => x.NumOfDives);

            // Create list of divers that have max number of dives
            List<Diver> diversToRemove = divers.Where(x => x.NumOfDives == max).ToList();

            foreach (Diver diverToRemove in diversToRemove)
            {
                if (numOfDiversToRemove-- > 0) divers.Remove(diverToRemove);
                else break;
            }

            // If there are more divers that need to be deleted then go to next filter/chain
            if (numOfDiversToRemove > 0)
                nextFilter.FilterDivers(divers, numOfDiversToRemove);
        }
    }
}

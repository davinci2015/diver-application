using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_4.DiverApp.Main;

namespace dvincija_zadaca_4.DiverApp.ChainOfResponsibility.Filters
{
    public class OldestDiveFilter : IFilterChain
    {
        IFilterChain nextFilter;
        public IFilterChain NextChain { set { nextFilter = value; } }

        /// <summary>
        /// Remove divers with most recent dive date 
        /// and keep those with oldest dive date
        /// </summary>
        /// <param name="divers">List of divers</param>
        /// <param name="numOfDiversToRemove">Number of divers to remove</param>
        public void FilterDivers(List<Diver> divers, int numOfDiversToRemove)
        {
            // Find most recent date
            DateTime mostRecentDiveDate = divers.Max(x => x.DiveList.Max(y => y.dateTime));

            for (int i = divers.Count() - 1; i >= 0; i--)
            {
                // Check if diver have dive with most recent date 
                // if he have then delete him
                bool hasDive = divers[i].DiveList.Exists(x => x.dateTime == mostRecentDiveDate);
                if (hasDive && numOfDiversToRemove-- > 0)
                    divers.Remove(divers[i]);
            }

            // If there are more divers that need to be deleted then go to next filter/chain 
            if (numOfDiversToRemove > 0)
                nextFilter.FilterDivers(divers, numOfDiversToRemove);
        }
    }
}

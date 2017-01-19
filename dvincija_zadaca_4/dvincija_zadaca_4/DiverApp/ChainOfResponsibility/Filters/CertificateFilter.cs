using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_4.DiverApp.Main;

namespace dvincija_zadaca_4.DiverApp.ChainOfResponsibility.Filters
{
    public class CertificateFilter : IFilterChain
    {
        IFilterChain nextFilter;
        public IFilterChain NextChain { set { nextFilter = value; } }

        /// <summary>
        /// Filter divers by certificate category.
        /// Divers with highest category will be deleted.
        /// </summary>
        /// <param name="divers">List of divers</param>
        /// <param name="numOfDiversToRemove">Number of divers to delete</param>
        public void FilterDivers(List<Diver> divers, int numOfDiversToRemove)
        {
            // Find max category
            int max = divers.Max(x => x.certificate.AbsoluteLevel);

            // Find divers with max category
            List<Diver> diversToRemove = divers.Where(x => x.certificate.AbsoluteLevel == max).ToList();

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

using dvincija_zadaca_4.DiverApp.ChainOfResponsibility.Filters;
using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.ChainOfResponsibility
{
    public class DiverFilterChain
    {
        IFilterChain filterFirst;
        public DiverFilterChain()
        {
            // Init the chain
            filterFirst = new DiveNumberFilter();

            OldestDiveFilter filterSecond = new OldestDiveFilter();
            CertificateFilter filterThird = new CertificateFilter();

            // Set next chain responsibility
            filterFirst.NextChain  = filterSecond;
            filterSecond.NextChain = filterThird;
            filterThird.NextChain  = filterFirst;
        }


        /// <summary>
        /// Pass list of divers through filter chain
        /// </summary>
        /// <param name="diverList">List of divers for filtering</param>
        /// <param name="numOfDiversToRemove">Number of divers to remove</param>
        public void FilterDivers(List<Diver> diverList, int numOfDiversToRemove)
        {
            filterFirst.FilterDivers(diverList, numOfDiversToRemove);
        }

    }
}

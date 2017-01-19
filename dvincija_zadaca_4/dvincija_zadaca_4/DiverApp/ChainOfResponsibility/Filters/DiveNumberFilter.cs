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
        public void FilterDivers(List<Diver> divers)
        {
            Console.WriteLine("Filtriram po broju urona");

            // TODO 
            // Filtriraj po broju urona

            // Ukoliko treba još filtrirati postavi
            // nextFilter.FilterDivers(divers);
        }

        public void SetNextChain(IFilterChain chain)
        {
            this.nextFilter = chain;
        }
    }
}

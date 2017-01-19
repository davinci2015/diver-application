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
        public void FilterDivers(List<Diver> divers)
        {
            throw new NotImplementedException();
        }

        public void SetNextChain(IFilterChain chain)
        {
            this.nextFilter = chain;
        }
    }
}

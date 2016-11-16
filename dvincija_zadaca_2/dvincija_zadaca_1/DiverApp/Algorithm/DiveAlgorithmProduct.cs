using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Algorithm
{
    public abstract class DiveAlgorithmProduct
    {
        public abstract List<PairHelper> GetDivePairs(List<Diver> diver, DiveSchedule diveSchedule);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Helpers
{
    public static class Config
    {
        private static Random random;
        private static int maxLoanDays;
        public static Random Random
        {
            get { return random; }
            set { random = value; }
        }

        public static int MaxLoanDays
        {
            get { return maxLoanDays; }
            set { maxLoanDays = value; }
        }
    }
}

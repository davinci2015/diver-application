using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Helpers
{
    public class Validation
    {
        /// <summary>
        /// Validate federation name - has to be SSI | CMAS | BSAC | NAUI
        /// </summary>
        /// <param name="federationName"></param>
        /// <returns>true if valid</returns>
        public static bool ValidateFederationName(string federationName)
        {
            Regex regex = new Regex(@"((^SSI$)|(^CMAS$)|(^BSAC$)|(^NAUI$))");
            return regex.IsMatch(federationName);
        }

        /// <summary>
        /// Validate Diver Category Level 
        /// </summary>
        /// <param name="level">Diver category level</param>
        /// <returns>true if valid</returns>
        public static bool ValidateDiverLevel(string level)
        {
            Regex regex = new Regex(@"(^R[0-5]$)|(^I[1-8]$)");
            return regex.IsMatch(level);
        }
    }
}

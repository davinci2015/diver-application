using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp
{
    public class Certificate
    {
        public string authorizedFederation { get; set; }
        public string name { get; set; }
        public string level { get; set; }
        public int depth { get; set; }
        public int absoluteLevel { get; set; }

        /// <summary>
        /// Certificate constructor
        /// </summary>
        /// <param name="authorizedFederation">Certificate authorized federation name</param>
        /// <param name="name">Certificate name</param>
        /// <param name="level">Certificate level</param>
        /// <param name="depth">Depth determined by certificate level and name</param>
        public Certificate(string authorizedFederation, string name, string level, int depth)
        {
            this.authorizedFederation = authorizedFederation;
            this.name = name;
            this.level = level;
            this.depth = depth;
            this.absoluteLevel = (int)Enum.Parse(typeof(CertificateAbsoluteLevel), level);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class Certificate
    {
        string authorizedFederation { get; set; }
        string name { get; set; }
        string level { get; set; }
        int depth { get; set; }
        public int Depth { get { return depth; } }

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
        }
    }
}

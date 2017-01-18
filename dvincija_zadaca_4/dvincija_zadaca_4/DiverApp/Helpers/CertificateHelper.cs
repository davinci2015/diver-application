using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Helpers
{
    public class CertificateHelper
    {
        private Dictionary<string, int> certifiedDepth = new Dictionary<string, int>()
        {
            { "R0", 0 }, { "R1", 10 }, { "R2", 30 }, { "R3", 40 }, { "R4", 40 }, { "R5", 40 }
        };

        private Dictionary<string, string[]> recreationalCerfiticates = new Dictionary<string, string[]>()
        {
            { "SSI",  new string[] { "Scuba Diver", "Open Water Diver", "Advanced Adventure", "Diver Stress & Rescue", "Advanced Open Water Diver", "Master Diver" } },
            { "NAUI", new string[] { "Scuba Diver", "Scuba Diver", "Advanced Scuba Diver", "Scuba Rescue Diver", "Master Scuba Diver", "Master Scuba Diver"        } },
            { "BSAC", new string[] { "Ocean Diver", "Ocean Diver", "Ocean Diver", "Sports Diver", "Sports Diver", "Sports Diver"                                   } },
            { "CMAS", new string[] { "One Star Diver", "One Star Diver", "One Star Diver", "Two Star Diver", "Two Star Diver", "Two Star Diver"                    } }
        };

        private Dictionary<string, string[]> professionalCertificates = new Dictionary<string, string[]>()
        {
            { "SSI",  new string[] { "Dive Guide", "Divemaster", "Dive Control Specialist", "Open Water Instructor", "Advanced Open Water Instructor", "Divemaster Instructor", "Dive Control Specialist Instructor", "Instructor Trainer" } },
            { "NAUI", new string[] { "Assistant Instructor", "Divemaster", "Divemaster", "Instructor", "Instructor", "Instructor", "Instructor", "Instructor Trainer" } },
            { "BSAC", new string[] { "Dive Leader", "Dive Leader", "Assistant Open Water Instructor", "Open Water Instructor", "Advanced Instructor", "Advanced Instructor", "Advanced Instructor", "Advanced Instructor" } },
            { "CMAS", new string[] { "Three Star Diver", "Three Star Diver", "One Star Instructor", "Two Star Instructor", "Two Star Instructor", "Two Star Instructor", "Two Star Instructor" } }
        };

        public string getCertificateName(string federation, string level)
        {
            string certificateName = "";

            if (level[0] == 'R')
                certificateName = recreationalCerfiticates[federation][Int32.Parse(level[1].ToString())];
            else if (level[0] == 'I')
                certificateName = professionalCertificates[federation][Int32.Parse(level[1].ToString()) - 1];
           
            return certificateName;
        }

        public int getDepthDeterminedByCertificate(string level)
        {
            int depth;
            return certifiedDepth.TryGetValue(level, out depth) ? depth : 40;
        }
    }
}

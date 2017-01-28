using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Helpers
{
    public static class Constants
    {
        public static string DRY_SUIT { get { return "Suho odijelo"; } }
        public static string UNDERSUIT { get { return "Pododijelo"; } }
        public static string HOOD { get { return "Kapuljača"; } }
        public static string PHOTOGRAPHER { get { return "Podvodni fotograf"; } }
        public static string NIGHT_DIVE { get { return "Noćno ronjenje"; } }
        public static string EQUIPMENT_NEEDED { get { return "*"; } }
        public static string NEED_OTHER_EQUIPMENT { get { return "+"; } }
        public static string WET_SUIT { get { return "Mokro"; } } 
        public static int TEMPERATURE_BOUNDARY { get { return 15; } }

        public static string FULLY_EQUIPED { get { return "Potpuno opremljen"; } }
        public static string PARTIALLY_EQUIPED { get { return "Djelomično opremljen"; } }
        public static string NOT_EQUIPED { get { return "Nije opremljen"; } }
        public static int NUM_OF_BASIC_EQUIPMENT { get { return 4; } }
    }
}

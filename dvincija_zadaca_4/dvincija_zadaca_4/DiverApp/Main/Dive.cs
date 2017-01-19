using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class Dive
    {
        DateTime date { get; set; }
        string time { get; set; }
        int numOfPhotographers { get; set; }
        public int numOfDiversNeeded { get; private set; }
        public int depth { get; private set; }
        public int temperature { get; private set; }
        public bool isNightDive { get; private set; }
        List<Diver> divers = new List<Diver>();

        public int NumOfDivers { get { return divers.Count(); } }
        public List<Diver> Divers { get { return divers; } }

        public Dive(DateTime date, string time, int depth, int numOfDiversNeeded, int temperature, bool isNightDive, int numOfPhotographers)
        {
            this.date = date;
            this.time = time;
            this.depth = depth;
            this.numOfDiversNeeded = numOfDiversNeeded;
            this.temperature = temperature;
            this.isNightDive = isNightDive;
            this.numOfPhotographers = numOfPhotographers;
        }

        public void AddDiver(Diver diver)
        {
            divers.Add(diver);
        }
    }
}

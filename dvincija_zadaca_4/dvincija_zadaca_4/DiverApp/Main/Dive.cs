using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class Dive
    {
        string date { get; set; }
        string time { get; set; }
        int depth { get; set; }
        public int numOfDiversNeeded { get; set; }
        int temperature { get; set; }
        bool isNightDive { get; set; }
        int numOfPhotographers { get; set; }
        List<Diver> divers = new List<Diver>();

        public int NumOfDivers { get { return divers.Count(); } }
        public int Depth { get { return depth; } }
        public int Temperature { get { return temperature; } }
        public bool IsNightDive { get { return isNightDive; } }

        public Dive(string date, string time, int depth, int numOfDiversNeeded, int temperature, bool isNightDive, int numOfPhotographers)
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

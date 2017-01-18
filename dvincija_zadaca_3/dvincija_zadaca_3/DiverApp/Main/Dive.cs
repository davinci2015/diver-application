using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Main
{
    public class Dive
    {
        int depth { get; set; }
        int temperature { get; set; }
        bool nightDive { get; set; }
        int recording { get; set; }

        public Dive(int depth, int temperature, bool nightDive, int recording)
        {
            this.depth = depth;
            this.temperature = temperature;
            this.nightDive = nightDive;
            this.recording = recording;
        }

        public int GetTemperature()
        {
            return temperature;
        }

        public bool IsNightDive()
        {
            return nightDive;
        }
        public int GetNoOfPhotographers()
        {
            return recording;
        }
    }
}

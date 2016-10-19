using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp
{
    public class DiveSchedule
    {
        public string date { get; set; }
        public string time { get; set; }
        public int maxDepth { get; set; }
        public int numOfDivers { get; set; }
        public List<Diver> divers = new List<Diver>();
        public List<PairHelper> diversGroup;  
        
        public DiveSchedule(string date, string time, int maxDepth, int numOfDivers)
        {
            this.date = date;
            this.time = time;
            this.maxDepth = maxDepth;
            this.numOfDivers = numOfDivers;
        }

        public void setDivers(List<Diver> divers, Random random, int numOfDiversToGenerate)
        {
            // Generate random number array without duplicates 
            // so we don't get same diver twice or more times in same dive
            var randomNumbers = Enumerable.Range(0, divers.Count()).OrderBy(x => random.Next()).Take(numOfDiversToGenerate).ToList();

            for (int i = 0; i < numOfDiversToGenerate; i++)
            {
                this.divers.Add(divers[randomNumbers[i]]);
            }
        }

        public void setDiveGroup(List<PairHelper> diversGroup)
        {
            this.diversGroup = diversGroup;
        }
    }
}

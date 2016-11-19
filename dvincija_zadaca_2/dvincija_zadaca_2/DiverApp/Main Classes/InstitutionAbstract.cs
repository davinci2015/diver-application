using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp.Observer;
using dvincija_zadaca_1.DiverApp.Visitor;

namespace dvincija_zadaca_1.DiverApp.Main_Classes
{
    public abstract class InstitutionAbstract : Observer.Observer
    {
        public List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
        public List<Diver> divers = new List<Diver>();
        public string institutionName { get; set; }

        /// <summary>
        /// Calculate number of dives in institution
        /// </summary>
        /// <returns>Number of dives</returns>
        public int CountNumOfDives()
        {
            return diveSchedule.Count();
        }

        /// <summary>
        /// Calculate average depth for every dive
        /// </summary>
        /// <returns>Average depth</returns>
        public float CalculateAverageDepth()
        {
            int numOfGroups = 0;
            int depthSum = 0;

            foreach (DiveSchedule dive in diveSchedule)
            {
                foreach (PairHelper group in dive.diveGroups)
                {
                    numOfGroups++;
                    depthSum += group.maxDepth;
                }
            }

            return depthSum / (float)numOfGroups;
        }

        public abstract void Update(Subject o);
        public abstract void Accept(InstitutionVisitor visitor);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp.Observer;
using dvincija_zadaca_1.DiverApp.Helpers;

namespace dvincija_zadaca_1.DiverApp
{
    public class Federation : Observer.Observer
    {
        public string federationName { get; set; }
        private List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
        public Federation(string federationName)
        {
            this.federationName = federationName;
        }

        public void Update(Subject subject)
        {
            DiveSchedule dive = subject.getDive();
            int diversNum = dive.diveGroups.Where(x => x.diverPair.Any(z => z.certificate.authorizedFederation == federationName)).Count();
            if(diversNum > 0)
                diveSchedule.Add(dive);
        }
        public int CountNumOfDives()
        {
            return diveSchedule.Count();
        }
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
    }
}

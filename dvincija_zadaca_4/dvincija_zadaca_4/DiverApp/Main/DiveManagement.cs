using dvincija_zadaca_4.DiverApp.ChainOfResponsibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class DiveManagement
    {
        List<Dive> diveList = new List<Dive>();
        List<Diver> diverList = new List<Diver>();
    
        static readonly string drySuit      = "Suho odijelo";
        static readonly string photographer = "Podvodni fotograf";
        static readonly string nightDive    = "Noćno ronjenje";

        int numOfUnderwaterPhotographers = 0;

        public DiveManagement(List<Diver> diverList)
        {
            this.diverList = diverList;
        }

        public void AddDivesToList(string[] divesRaw)
        {
            string[] dive;

            foreach (string d in divesRaw)
            {
                dive = d.Split(';');
            
                // Create instance of dive class
                Dive diveObj = new Dive(DateTime.Parse(dive[0]), dive[1], Int32.Parse(dive[2]), Int32.Parse(dive[3]), Int32.Parse(dive[4]), dive[5] == "1", Int32.Parse(dive[6]));

                // Add dive to list
                diveList.Add(diveObj);
            }
        }

        public void AssignDiversToDive()
        {
            foreach (Dive dive in diveList)
            {
                FilterDivers(dive, diverList);

                // If there are more divers than needed
                if (dive.NumOfDivers > dive.numOfDiversNeeded)
                {
                    int numOfDiversToRemove = dive.NumOfDivers - dive.numOfDiversNeeded;

                    DiverFilterChain filterChain = new DiverFilterChain();
                    filterChain.FilterDivers(dive.Divers, numOfDiversToRemove);
                }

            }
        }

        /// <summary>
        /// Filter diver list by depth, temperature and night dive specialty 
        /// and count how many divers have underwater photography specialty
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="temperature"></param>
        /// <param name="isNightDive"></param>
        private void FilterDivers(Dive dive, List<Diver> diverList)
        {
            int diversNo = diverList.Count();
            foreach (Diver diver in diverList.ToArray())
            {
                // Filter by depth, dry suit and night dive specialty
                if ((diver.certificate.Depth + 10 < dive.depth) ||
                     (dive.temperature < 15 && !diver.CheckIfDiverHasSuperPower(drySuit)) ||
                     (dive.isNightDive && !diver.CheckIfDiverHasSuperPower(nightDive)))    
                    continue;
                
                // Count how many divers has photography specialty
                if (diver.CheckIfDiverHasSuperPower(photographer))
                    numOfUnderwaterPhotographers++;

                dive.AddDiver(diver);
                diver.AddDiveToList(dive);
            }
        }
    }
}

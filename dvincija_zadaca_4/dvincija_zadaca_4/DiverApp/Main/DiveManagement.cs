using dvincija_zadaca_4.DiverApp.ChainOfResponsibility;
using dvincija_zadaca_4.DiverApp.Composite;
using dvincija_zadaca_4.DiverApp.Evictor;
using dvincija_zadaca_4.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class DiveManagement
    {
        private List<Dive> diveList = new List<Dive>();
        private List<Diver> diverList;
        private int numOfUnderwaterPhotographers = 0;
        private EquipmentEvictor equipmentEvictor = new EquipmentEvictor();
        public List<Dive> DiveList { get { return diveList; } }

        public DiveManagement(List<Diver> diverList)
        {
            this.diverList = diverList;
        }

        /// <summary>
        /// Parse dives and add them to list
        /// </summary>
        /// <param name="divesRaw">Array of strings. Each row represents row in file</param>
        public void AddDivesToList(string[] divesRaw)
        {
            string[] dive;

            foreach (string d in divesRaw)
            {
                dive = d.Split(';');

                // Create instance of dive class
                string date = dive[0] + " " + dive[1];
                DateTime diveDateTime = DateTime.ParseExact(date, "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
                Dive diveObj = new Dive(diveDateTime, Int32.Parse(dive[2]), Int32.Parse(dive[3]), Int32.Parse(dive[4]), dive[5] == "1", Int32.Parse(dive[6]));

                // Add dive to list
                diveList.Add(diveObj);
            }
        }

        /// <summary>
        /// Filter and assign divers to dives 
        /// depending on dive conditions
        /// </summary>
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
                if ((diver.certificate.depth + 10 < dive.depth) ||
                     (dive.temperature < 15 && !diver.CheckIfDiverHasSuperPower(Constants.DRY_SUIT)) ||
                     (dive.isNightDive && !diver.CheckIfDiverHasSuperPower(Constants.NIGHT_DIVE)))    
                    continue;
                
                // Count how many divers has photography specialty
                if (diver.CheckIfDiverHasSuperPower(Constants.PHOTOGRAPHER))
                    numOfUnderwaterPhotographers++;

                dive.AddDiver(diver);
                diver.AddDiveToList(dive);
            }
        }

        public void EquipDiversForDive(EquipmentManagement equipmentManagement)
        {
           foreach (Dive dive in diveList)
           {
                // Return equipment
                equipmentEvictor.ReturnEquipment(dive, diverList);

                // If there is enough divers for dive then equip them
                if (dive.NumOfDivers == dive.numOfDiversNeeded)
                    equipmentManagement.AssignEquipmentToDivers(dive);

                // Print each diver equipment in current dive
                Writer.PrintEquipmentPerDiverInDive(dive);

                // Print warehouse stock status
                Writer.PrintWarehouseStatusHeading();
                equipmentManagement.PrintWarehouseStatus();
            }
        }
    }
}

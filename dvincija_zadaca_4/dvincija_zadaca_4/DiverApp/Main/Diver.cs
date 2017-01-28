using dvincija_zadaca_4.DiverApp.Composite;
using dvincija_zadaca_4.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class Diver
    {
        public string name { get; private set; }
        public string birthDate { get; private set; }
        public string federationName { get; private set; }
        public List<string> superPowers { get; private set; }
        public Certificate certificate { get; private set; }
        public string equipmentStatus { get; private set; }
        public bool basicEquipmentAssociation = true;

        public List<Dive> DiveList { get { return diveList; } }
        public List<ConcreteEquipment> EquipmentList { get { return equipmentList; } }
        public int NumOfDives { get { return diveList.Count(); } }

        List<Dive> diveList = new List<Dive>();
        List<ConcreteEquipment> equipmentList = new List<ConcreteEquipment>();

        /// <summary>
        /// Diver constructor
        /// </summary>
        /// <param name="name">diver name</param>
        /// <param name="birthDate">diver birth date</param>
        /// <param name="certificate">diver certificate</param>
        /// <param name="federationName">certificate authorized federation name</param>
        public Diver(string name, string birthDate, Certificate certificate, string federationName)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.certificate = certificate;
            this.federationName = federationName;
        }

        public void AddSuperPowers(List<string> superPowers)
        {
            this.superPowers = superPowers;
        }

        public void AddDiveToList(Dive dive)
        {
            diveList.Add(dive);
        }
        
        public bool CheckIfDiverHasSuperPower(string superPower)
        {
            return superPowers.Contains(superPower);
        }

        public void AddEquipment(ConcreteEquipment item)
        {
            equipmentList.Add(item);
        }

        public bool CheckExistingEquipmentByCategory(string equipmentID)
        {
            return equipmentList.Any(x => x.ID.Remove(x.ID.Length - 2) == equipmentID.Remove(equipmentID.Length - 2) && equipmentID[0] != '0');
        }

        public bool CheckIfDiverHaveEquipment(ConcreteEquipment equipment)
        {
            return equipmentList.Contains(equipment);
        }

        public bool CheckIfDiverNeedUndersuit()
        {
            return equipmentList.Any(x => x.needUndersuit == Constants.NEED_OTHER_EQUIPMENT);
        }

        public bool CheckIfDiverNeedHood()
        {
            return equipmentList.Any(x => x.needHood == Constants.NEED_OTHER_EQUIPMENT);
        }

        /// <summary>
        /// Update diver equipment status
        /// FULLY EQUIPED       - basic equipment + all additional equipment
        /// PARTIALLY EQUIPED   - basic equipment
        /// NOT EQUIPED         - without basic equipment
        /// </summary>
        public void UpdateEquipmentStatus(Dive dive)
        {
            int basicEquipmentCounter = 0;

            // Check if user have all basic equipment
            foreach (ConcreteEquipment equipment in equipmentList)
                if (EquipmentHelper.IsBasicEquipment(equipment))
                    basicEquipmentCounter++;

            // Basic equipment
            if (basicEquipmentCounter == Constants.NUM_OF_BASIC_EQUIPMENT && equipmentList.Count() == Constants.NUM_OF_BASIC_EQUIPMENT)
                equipmentStatus = Constants.PARTIALLY_EQUIPED;

            // Diver is not adequate equiped for dive
            else if (basicEquipmentCounter < Constants.NUM_OF_BASIC_EQUIPMENT)
                equipmentStatus = Constants.NOT_EQUIPED;

            // Basic + additional equipment
            else if (
                // If it's cold dive then check if diver is adequate equipped for cold dive
                (dive.temperature < Constants.TEMPERATURE_BOUNDARY && EquipmentHelper.IsDiverAdequatEquippedForColdDive(equipmentList)) ||

                // If it's dive with temp above 15 and diver is adequate equipped for that dive
                (dive.temperature >= Constants.TEMPERATURE_BOUNDARY && EquipmentHelper.IsDiverAdequateEquippedForWarmDive(equipmentList)) &&

                // If it's night dive then check if diver is adequate equipped for night dive
                ((dive.isNightDive && EquipmentHelper.IsDiverAdequateEquippedForNightDive(EquipmentList)) || !dive.isNightDive)
                )
            {
                equipmentStatus = Constants.FULLY_EQUIPED;
            }
        }
    }
}

using dvincija_zadaca_4.DiverApp.Helpers;
using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Composite
{
    public class ConcreteEquipment : IEquipment
    {
        public string ID { get; private set; }
        public string name { get; private set; }
        string temperature { get; set; }
        string needHood { get; set; }
        string needUndersuit { get; set; }
        string needForNightDive { get; set; }
        string needForRecording { get; set; }
        public int stock { get; private set; }
        public DateTime loanDate { get; private set; }
        public int maxLoanDays { get; private set; }

        public ConcreteEquipment(string ID, string name, string temperature, string needHood, string needUndersuit, string needForNightDive, string needForRecording, int stock)
        {
            this.ID = ID;
            this.name = name;
            this.temperature = temperature;
            this.needForNightDive = needForNightDive;
            this.needForRecording = needForRecording;
            this.needHood = needHood;
            this.needUndersuit = needUndersuit;
            this.stock = stock;
        }

        public IEquipment AddComponent(IEquipment equipment)
        {
            // Leaf can't add component
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if equipment is available in stock
        /// </summary>
        /// <returns>true if it is available</returns>
        private bool IsEquipmentAvailable()
        {
            return stock > 0;
        }

        /// <summary>
        /// Lend equipment to diver
        /// </summary>
        /// <param name="dive"></param>
        /// <param name="diver"></param>
        private void LendEquipment(Dive dive, Diver diver)
        {
            // Add equipment to diver
            diver.AddEquipment(this);

            // Set loan date to dive date
            loanDate = dive.dateTime;

            // Set max loan days
            maxLoanDays = Config.Random.Next(1, Config.MaxLoanDays);

            // Reduce stock
            stock--;
        }

        /// <summary>
        /// Equip diver with adequate equipment for dive
        /// - Hope your eyes won't bleed on this one
        /// </summary>
        /// <param name="diver">Diver that we're equipping</param>
        /// <param name="dive">Dive for which we're equipping diver</param>
        public void EquipDiver(Diver diver, Dive dive)
        {
            // Equip with basic equipment
            if (EquipmentHelper.IsBasicEquipment(this) &&       // Check if it is basic equipment
                IsEquipmentAvailable() &&                       // Is equipment available
                !diver.CheckExistingEquipmentByCategory(ID)     // Check if diver already have similar equipment
                )
            {
                LendEquipment(dive, diver);
            }

            // Equip with dry suits
            else if (dive.temperature < 15 &&                   // Check if it's cold     
                IsEquipmentAvailable() &&                       // Is equipment available
                !diver.CheckExistingEquipmentByCategory(ID) &&  // Check if diver already have similar equipment
                EquipmentHelper.IsDrySuit(this) &&              // Check if it is dry suit
                !diver.CheckIfDiverHaveDrySuit()
                )                        
            {
                LendEquipment(dive, diver);
            }

            // Equip with undersuit
            else if (EquipmentHelper.IsUndersuit(this) &&
                diver.CheckIfDiverHaveDrySuit() &&
                !diver.CheckExistingEquipmentByCategory(ID) && 
                IsEquipmentAvailable()
                )
            {
                LendEquipment(dive, diver);
            }

            // Equip with wet suits
            else if (dive.temperature >= 15 &&
                EquipmentHelper.IsWetSuit(this) &&
                IsEquipmentAvailable() &&
                !diver.CheckExistingEquipmentByCategory(ID)
                )
            {
                LendEquipment(dive, diver);
            }

            // Equip with equipment for night dive
            else if (dive.isNightDive &&
                needForNightDive == Constants.EQUIPMENT_NEEDED &&
                diver.CheckIfDiverHasSuperPower(Constants.NIGHT_DIVE) &&
                IsEquipmentAvailable() &&
                !diver.CheckExistingEquipmentByCategory(ID)
                )
            {
                LendEquipment(dive, diver);
            }

            // Equip with equipment for photographers
            else if (dive.numOfPhotographers > 0 &&
                diver.CheckIfDiverHasSuperPower(Constants.PHOTOGRAPHER) &&
                needForRecording == Constants.EQUIPMENT_NEEDED &&
                !diver.CheckExistingEquipmentByCategory(ID) &&
                IsEquipmentAvailable()
                )
            {
                LendEquipment(dive, diver);
            }
        }
    }
}

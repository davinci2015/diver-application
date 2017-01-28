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
        public string needHood { get; private set; }
        public string needUndersuit { get; private set; }
        string needForNightDive { get; set; }
        string needForRecording { get; set; }
        public int stock { get; private set; }
        public int originalStock { get; private set; }
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
            this.originalStock = stock;
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
        /// When diver returns equipment update stock value
        /// </summary>
        public void ReturnEquipment()
        {
            stock++;
        }

        /// <summary>
        /// Lend equipment to diver
        /// </summary>
        /// <param name="dive"></param>
        /// <param name="diver"></param>
        private void LendEquipment(Dive dive, Diver diver)
        {
            // If diver already have this equipment then just extend loan date
            if (diver.CheckIfDiverHaveEquipment(this))
                SetLoanDate(dive);

            // Else if equipment is available and diver does not have equipment from that category
            else if (IsEquipmentAvailable() && !diver.CheckExistingEquipmentByCategory(ID) && !diver.CheckIfDiverHaveEquipment(this) )
            {
                // Add equipment to diver
                diver.AddEquipment(this);

                // Set loan date
                SetLoanDate(dive);

                // Reduce stock
                stock--;
            }
        }

        private void SetLoanDate(Dive dive)
        {
            // Set loan date to dive date
            loanDate = dive.dateTime;

            // Set max loan days
            maxLoanDays = Config.Random.Next(1, Config.MaxLoanDays);
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
            if (EquipmentHelper.IsBasicEquipment(this))
                LendEquipment(dive, diver);

            // Equip with dry suit
            else if (dive.temperature < Constants.TEMPERATURE_BOUNDARY && EquipmentHelper.IsDrySuit(this) && !diver.CheckIfDiverNeedUndersuit())                        
                LendEquipment(dive, diver);
            
            // Equip with undersuit
            else if (EquipmentHelper.IsUndersuit(this) && diver.CheckIfDiverNeedUndersuit())
                LendEquipment(dive, diver);

            // Equip with wet suit
            else if (dive.temperature >= Constants.TEMPERATURE_BOUNDARY && EquipmentHelper.IsWetSuit(this))
                LendEquipment(dive, diver);
            
            // Equip with hood if needed
            else if (dive.temperature >= 15 && EquipmentHelper.IsHood(this) && diver.CheckIfDiverNeedHood())
                LendEquipment(dive, diver);

            // Equip with equipment for night dive
            else if (dive.isNightDive && needForNightDive == Constants.EQUIPMENT_NEEDED && diver.CheckIfDiverHasSuperPower(Constants.NIGHT_DIVE))
                LendEquipment(dive, diver);

            // Equip with equipment for photographers
            else if (dive.numOfPhotographers > 0 && diver.CheckIfDiverHasSuperPower(Constants.PHOTOGRAPHER) && needForRecording == Constants.EQUIPMENT_NEEDED)
                LendEquipment(dive, diver);
            
            // Additional equipment
            else if (EquipmentHelper.IsAdditionalEquipment(this) && temperature != "#" && dive.temperature < 20 ||
                     EquipmentHelper.IsAdditionalEquipment(this) && temperature == "#")
                LendEquipment(dive, diver);
        }

        public void PrintComponentStatus()
        {
            Writer.PrintEquipment(this);
        }
    }
}

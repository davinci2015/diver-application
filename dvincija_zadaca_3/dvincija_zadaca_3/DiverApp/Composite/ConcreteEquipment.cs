using dvincija_zadaca_3.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Composite
{
    public class ConcreteEquipment : Equipment
    {
        public string ID { get; set; }
        string name { get; set; }
        string temperature { get; set; }
        string needHood { get; set; }
        string needUndersuit { get; set; }
        string needForNightDive { get; set; }
        string needForRecording { get; set; }
        int stock { get; set; }

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

        public Equipment AddComponent(Equipment equipment)
        {
            // Leaf can't add component
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return name;
        }

        public void FindBasicEquipmentForDive(List<ConcreteEquipment> basicEquipment, Dive dive)
        {
            if ( (temperature == "#" && needForRecording == "#" && needForNightDive == "#") ||
                 (dive.GetTemperature() < 15 && (ID.StartsWith("1.1.3.") || ID.StartsWith("1.2.") || ID.StartsWith("1.4.") || ID.StartsWith("1.3.")) ) ||
                 (dive.GetTemperature() >= 15 && (ID.StartsWith("1.1.1.") || ID.StartsWith("1.1.2.") || ID.StartsWith("1.3.")) ) ||
                 (dive.IsNightDive() && needForNightDive == "*") ||
                 (dive.GetNoOfPhotographers() > 0 && needForRecording == "*") 
               )
            {
                basicEquipment.Add(this);
            }
        }

        public void EquipDiver(Diver diver, Dive dive)
        {
            // Equip with basic equipment
            if (diver.IsBasicEquipmentAssociation() == true &&
                temperature == "#" &&
                needForRecording == "#" &&
                needForNightDive == "#" &&
                stock > 0 &&
                !diver.CheckExistingEquipmentByCategory(ID))
            {
                diver.AddEquipment(this);
                stock--;
            }
            // Equip with dry suits
            if (dive.GetTemperature() < 15 &&
                (ID.StartsWith("1.1.3.") || ID.StartsWith("1.2.") || ID.StartsWith("1.4.") || (ID.StartsWith("1.3.") && temperature != "#" && Int32.Parse(temperature) < 15)) &&
                stock > 0 &&
                !diver.CheckExistingEquipmentByCategory(ID))
            {
                diver.AddEquipment(this);
                stock--;
            }
            // Equip with wet suits
            else if (dive.GetTemperature() >= 15 &&
                     (ID.StartsWith("1.1.1.") || ID.StartsWith("1.1.2.") || (ID.StartsWith("1.3.") && temperature != "#" && Int32.Parse(temperature) >= 15)) &&
                     stock > 0 &&
                     !diver.CheckExistingEquipmentByCategory(ID))
            {
                diver.AddEquipment(this);
                stock--;
            }
            // Equip with equipment for night dive
            if (dive.IsNightDive() == true &&
                needForNightDive == "*" &&
                !diver.CheckExistingEquipmentByCategory(ID))
            {
                diver.AddEquipment(this);
                stock--;
            }
            // Equip with equipment for fotographers
            if (diver.CheckIfDiverHasSuperPower("Podvodni fotograf") == true &&
                needForRecording == "*" &&
                !diver.CheckExistingEquipmentByCategory(ID))
            {
                diver.AddEquipment(this);
                stock--;
            }
        }
    }
}

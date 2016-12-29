using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Composite
{
    public class ConcreteEquipment : Equipment
    {
        string ID { get; set; }
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

        public override Equipment AddComponent(Equipment equipment)
        {
            // Leaf can't add component
            throw new NotImplementedException();
        }
    }
}

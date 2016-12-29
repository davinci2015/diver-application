using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Composite
{
    public class CompositeEquipment : Equipment
    {
        string ID { get; set; }
        string name { get; set; }
        private List<Equipment> childEquipments = new List<Equipment>();

        public CompositeEquipment(string ID, string name)
        {
            this.ID = ID;
            this.name = name;
        }

        public override Equipment AddComponent(Equipment equipment)
        {
            childEquipments.Add(equipment);
            return equipment;
        }
    }
}

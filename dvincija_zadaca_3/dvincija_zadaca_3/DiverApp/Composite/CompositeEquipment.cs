using dvincija_zadaca_3.DiverApp.Main;
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
        List<Equipment> childEquipments = new List<Equipment>();
        public CompositeEquipment(string ID, string name)
        {
            this.ID = ID;
            this.name = name;
        }

        public Equipment AddComponent(Equipment equipment)
        {
            childEquipments.Add(equipment);
            return equipment;
        }

        public void EquipDiver(Diver diver, Dive dive)
        {
            foreach (Equipment equipment in childEquipments)
            {
                equipment.EquipDiver(diver, dive);
            }
        }

        public void FindBasicEquipmentForDive(List<ConcreteEquipment> basicEquipment, Dive dive)
        {
            foreach (Equipment equipment in childEquipments)
            {
                equipment.FindBasicEquipmentForDive(basicEquipment, dive);
            }
        }
    }
}

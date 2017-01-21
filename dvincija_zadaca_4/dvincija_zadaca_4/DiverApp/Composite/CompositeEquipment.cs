using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Composite
{
    public class CompositeEquipment : IEquipment
    {
        string ID { get; set; }
        string name { get; set; }
        List<IEquipment> childEquipments = new List<IEquipment>();
        public CompositeEquipment(string ID, string name)
        {
            this.ID = ID;
            this.name = name;
        }

        public IEquipment AddComponent(IEquipment equipment)
        {
            childEquipments.Add(equipment);
            return equipment;
        }

        public void EquipDiver(Diver diver, Dive dive)
        {
            foreach (IEquipment equipment in childEquipments)
                equipment.EquipDiver(diver, dive);
        }
    }
}

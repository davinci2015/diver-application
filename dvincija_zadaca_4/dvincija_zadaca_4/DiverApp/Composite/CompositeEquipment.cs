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
        private string ID { get; set; }
        private string name { get; set; }
        private List<IEquipment> childEquipments = new List<IEquipment>();

        public CompositeEquipment(string ID, string name)
        {
            this.ID = ID;
            this.name = name;
        }

        /// <summary>
        /// Add component to composite
        /// </summary>
        /// <param name="equipment">it can be equipment category (CompositeEquipment) or concrete equipment (ConcreteEquipment)</param>
        /// <returns></returns>
        public IEquipment AddComponent(IEquipment equipment)
        {
            childEquipments.Add(equipment);
            return equipment;
        }

        /// <summary>
        /// Iterate through composite and visit all leafs that are concrete equipment
        /// Equip diver with concrete equipment
        /// </summary>
        /// <param name="diver"></param>
        /// <param name="dive"></param>
        public void EquipDiver(Diver diver, Dive dive)
        {
            foreach (IEquipment equipment in childEquipments)
                equipment.EquipDiver(diver, dive);
        }

        /// <summary>
        /// Used for printing stock status of each equipment
        /// </summary>
        public void PrintComponentStatus()
        {
            foreach (IEquipment equipment in childEquipments)
                equipment.PrintComponentStatus();
        }
    }
}

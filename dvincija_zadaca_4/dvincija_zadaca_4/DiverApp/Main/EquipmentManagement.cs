using dvincija_zadaca_4.DiverApp.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class EquipmentManagement
    {
        CompositeEquipment allEquipment = new CompositeEquipment("0", "Equipment");
        public CompositeEquipment AllEquipment { get { return allEquipment; } }

        /// <summary>
        /// Load equipment categories 
        /// and concrete equipment into composite
        /// </summary>
        /// <param name="equipmentRaw">Array of strings. Each row represents row in file</param>
        public void LoadEquipment(string[] equipmentRaw)
        {
            string[] itemSplit;
            int categoryLevel;
            IEquipment lastNode = null;
            List<IEquipment> lastNodes = new List<IEquipment>();

            foreach (string item in equipmentRaw)
            {
                itemSplit = item.Split(';');

                if (itemSplit.Count() == 2 || itemSplit.Count() == 8)
                {
                    // 0 - Root category
                    // 1 - 1.1, 1.2 etc.
                    // 2 - 1.1.1, 1.1.2. etc.
                    categoryLevel = itemSplit[0].Split('.').Length - 1;

                    // Handle equipment root category
                    if (itemSplit[0].Length == 1 && itemSplit.Count() == 2)
                    {
                        lastNode = allEquipment.AddComponent(new CompositeEquipment(itemSplit[0], itemSplit[1]));
                        lastNodes.Insert(categoryLevel, lastNode);
                    }

                    // Handle child category
                    else if (itemSplit[0].Length > 1 && itemSplit.Count() == 2)
                    {
                        lastNode = lastNodes[categoryLevel - 1].AddComponent(new CompositeEquipment(itemSplit[0], itemSplit[1]));
                        lastNodes.Insert(categoryLevel, lastNode);
                    }

                    // Handle concrete equipment
                    else lastNodes[categoryLevel - 1].AddComponent(new ConcreteEquipment(itemSplit[0], itemSplit[1], itemSplit[2], itemSplit[3], itemSplit[4], itemSplit[5], itemSplit[6], Int32.Parse(itemSplit[7])));
                }
            }
        }

        /// <summary>
        /// Assign equipment to divers and update equipment status
        /// </summary>
        /// <param name="dive">Dive object</param>
        public void AssignEquipmentToDivers(Dive dive)
        {
            foreach (Diver diver in dive.Divers)
            {
                allEquipment.EquipDiver(diver, dive);
                diver.UpdateEquipmentStatus();
            }
        }
    }
}

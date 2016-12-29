using dvincija_zadaca_3.DiverApp.Composite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Main
{
    public class EquipmentManagement
    {
        CompositeEquipment allEquipment = new CompositeEquipment("0", "All");

        public void LoadEquipment(string[] equipmentRaw)
        {
            string[] itemSplit;
            int categoryLevel;
            Equipment lastNode = null;
            List<Equipment> lastNodes = new List<Equipment>();

            foreach (string item in equipmentRaw)
            {
                itemSplit = item.Split(';');

                // 0 - Root category
                // 1 - 1.1, 1.2 etc.
                // 2 - 1.1.1, 1.1.2. etc.
                categoryLevel = itemSplit[0].Split('.').Length - 1;
                
                // Handle equipment root category
                if (itemSplit[0].Length == 1 && itemSplit.Count() == 2)
                {
                    lastNode = allEquipment.AddComponent( new CompositeEquipment(itemSplit[0], itemSplit[1]) );
                    lastNodes.Insert(categoryLevel, lastNode);
                }

                // Handle child category
                else if (itemSplit[0].Length > 1 && itemSplit.Count() == 2)
                {
                    lastNode = lastNodes[categoryLevel - 1].AddComponent(new CompositeEquipment(itemSplit[0], itemSplit[1]));
                    lastNodes.Insert(categoryLevel, lastNode);
                }

                // Handle concrete equipment
                else
                {
                    lastNodes[categoryLevel - 1].AddComponent(new ConcreteEquipment(itemSplit[0], itemSplit[1], itemSplit[2], itemSplit[3], itemSplit[4], itemSplit[5], itemSplit[6], Int32.Parse(itemSplit[7]) ) );
                }
            }
        }
    }
}

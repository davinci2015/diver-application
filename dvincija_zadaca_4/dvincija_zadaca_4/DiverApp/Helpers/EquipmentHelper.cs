using dvincija_zadaca_4.DiverApp.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Helpers
{
    public class EquipmentHelper
    {
        static readonly string[] basicEquipment = new string[] { "Maska", "Disalica", "Peraje", "Boca za ronjenje 12l", "Boca za ronjenje 15l", "Boca za ronjenje 18l" };
        public static string[] BasicEquipment { get { return basicEquipment; } }

        public static bool IsBasicEquipment(ConcreteEquipment equipment)
        {
            return basicEquipment.Any(equipment.name.Contains);
        }

        public static bool IsDrySuit(ConcreteEquipment equipment)
        {
            return equipment.name.Contains(Constants.DRY_SUIT);
        }

        public static bool IsWetSuit(ConcreteEquipment equipment)
        {
            return equipment.name.Contains(Constants.WET_SUIT);
        }

        public static bool IsUndersuit(ConcreteEquipment equipment)
        {
            return equipment.name.Contains(Constants.UNDERSUIT);
        }
    }
}

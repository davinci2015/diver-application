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
        static readonly string[] basicEquipment = new string[]      { "Maska", "Disalica", "Peraje", "Boca za ronjenje 12l", "Boca za ronjenje 15l", "Boca za ronjenje 18l" };
        static readonly string[] additionalEquipment = new string[] { "Kompentator", "Pojas s olovom", "Ronilački kompas", "Ronilački nož", "Ronilačka bova", "Rukavice", "Čizme", "Regulator" };
        static readonly string[] nightDiveEquipment = new string[]  { "Ronilačka svjetiljka", "Ronilačko računalo" };
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
        public static bool IsHood(ConcreteEquipment equipment)
        {
            return equipment.name.Contains(Constants.HOOD);
        }

        public static bool IsAdditionalEquipment(ConcreteEquipment equipment)
        {
            return additionalEquipment.Any(equipment.name.Contains);
        }

        public static bool IsEquipmentForNightDive(ConcreteEquipment equipment)
        {
            return nightDiveEquipment.Any(equipment.name.Contains);
        }

        public static bool IsDiverAdequatEquippedForColdDive(List<ConcreteEquipment> equipmentList)
        {
            bool equippedDrySuit = false;
            bool equippedUndersuit = false;

            equipmentList.ForEach(x =>
            {
                if (IsDrySuit(x)) equippedDrySuit = true;
                if (IsUndersuit(x)) equippedUndersuit = true;
            });

            return equippedDrySuit && equippedUndersuit;
        }

        public static bool IsDiverAdequateEquippedForWarmDive(List<ConcreteEquipment> equipmentList)
        {
            bool equipped = false;
            equipmentList.ForEach(x => { if (IsWetSuit(x)) equipped = true; });
            return equipped;
        }

        public static bool IsDiverAdequateEquippedForNightDive(List<ConcreteEquipment> equipmentList)
        {
            int equipped = 0;
            equipmentList.ForEach(x => { if (IsEquipmentForNightDive(x)) equipped++; });
            return equipped == nightDiveEquipment.Count();
        }
    }
}

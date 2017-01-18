using dvincija_zadaca_3.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Composite
{
    public interface Equipment
    {
        Equipment AddComponent(Equipment equipment);
        void EquipDiver(Diver diver, Dive dive);
        void FindBasicEquipmentForDive(List<ConcreteEquipment> basicEquipment, Dive dive);
    }
}

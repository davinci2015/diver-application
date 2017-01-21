using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Composite
{
    public interface IEquipment
    {
        IEquipment AddComponent(IEquipment equipment);
        void EquipDiver(Diver diver, Dive dive);
    }
}

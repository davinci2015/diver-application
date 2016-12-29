using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Composite
{
    public abstract class Equipment
    {
        public int level;
        public string ID;
        public string name;
        public abstract Equipment AddComponent(Equipment equipment);
    }
}

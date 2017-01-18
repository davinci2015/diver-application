using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Memento
{
    public class Caretaker
    {
        Memento memento;
        public Memento Memento
        {
            get { return memento; }
            set { memento = value; }
        }
    }
}

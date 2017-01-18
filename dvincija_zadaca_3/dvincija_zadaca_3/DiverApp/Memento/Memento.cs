using dvincija_zadaca_3.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Memento
{
    public class Memento
    {
        List<Diver> divers;
        public Memento(List<Diver> divers)
        {
            this.divers = new List<Diver>(divers);
        }

        public List<Diver> Divers
        {
            get { return new List<Diver>(divers); }
        }
    }
}

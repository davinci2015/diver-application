using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Observer
{
    public interface Subject
    {
        void addObserver(Observer o);
        void removeObserver(Observer o);
        void setDive(DiveSchedule dive);
        DiveSchedule getDive();
    }
}

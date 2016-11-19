using dvincija_zadaca_1.DiverApp.Main_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Visitor
{
    public interface InstitutionVisitor
    {
        DiveSchedule Visit(InstitutionAbstract institution);
    }
}

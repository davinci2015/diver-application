using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dvincija_zadaca_1.DiverApp
{
    public class Diver
    {
        public string name { get; set; }
        public string birthDate { get; set; }
        public Certificate certificate;
        public List<DiveSchedule> diveSchedule = new List<DiveSchedule>();

        public Diver(string name, string birthDate, Certificate certificate)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.certificate = certificate;
        }

        public void addDive(DiveSchedule dive)
        {
            diveSchedule.Add(dive);
        }
    }
}

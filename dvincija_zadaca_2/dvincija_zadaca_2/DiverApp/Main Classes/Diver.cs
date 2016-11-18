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
        public string federationName { get; set; }

        public Certificate certificate;
        public List<DiveSchedule> diveSchedule = new List<DiveSchedule>();

        public Diver(string name, string birthDate, Certificate certificate, string federationName)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.certificate = certificate;
            this.federationName = federationName;
        }

        public void addDive(DiveSchedule dive)
        {
            diveSchedule.Add(dive);
        }
    }
}

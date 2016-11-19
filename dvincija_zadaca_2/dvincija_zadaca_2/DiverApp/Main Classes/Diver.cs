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

        /// <summary>
        /// Diver constructor
        /// </summary>
        /// <param name="name">diver name</param>
        /// <param name="birthDate">diver birth date</param>
        /// <param name="certificate">diver certificate</param>
        /// <param name="federationName">certificate authorized federation name</param>
        public Diver(string name, string birthDate, Certificate certificate, string federationName)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.certificate = certificate;
            this.federationName = federationName;
        }

        /// <summary>
        /// Add dive to dive schedule list
        /// </summary>
        /// <param name="dive">dive in which diver participated</param>
        public void AddDive(DiveSchedule dive)
        {
            diveSchedule.Add(dive);
        }
    }
}

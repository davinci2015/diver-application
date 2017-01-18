using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class Diver
    {
        public string name { get; private set; }
        public string birthDate { get; private set; }
        public string federationName { get; private set; }
        public List<string> superPowers;
        public Certificate certificate { get; private set; }
        public bool basicEquipmentAssociation = true;

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
        public void AddSuperPowers(List<string> superPowers)
        {
            this.superPowers = superPowers;
        }

        
        public bool CheckIfDiverHasSuperPower(string superPower)
        {
            return superPowers.Contains(superPower);
        }

        public void ChangeEquipmentAssociationType()
        {
            basicEquipmentAssociation = !basicEquipmentAssociation;
        }
    }
}

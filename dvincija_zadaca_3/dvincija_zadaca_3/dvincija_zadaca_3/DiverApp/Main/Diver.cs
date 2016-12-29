using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Main
{
    public class Diver
    {
        public string name { get; set; }
        public string birthDate { get; set; }
        public string federationName { get; set; }
        private List<string> superPowers;
        private bool basicEquipmentAssociation = true;
        public Certificate certificate;

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

        public List<string> GetSuperPowers()
        {
            return superPowers;
        }

        public bool CheckIfDiverHasSuperPower(string superPower)
        {
            return superPowers.Contains(superPower);
        }
        
        public void ChangeEquipmentAssociationType()
        {
            basicEquipmentAssociation = !basicEquipmentAssociation;
        }
        public bool GetEquipmentAssociationType()
        {
            return basicEquipmentAssociation;
        }
    }
}

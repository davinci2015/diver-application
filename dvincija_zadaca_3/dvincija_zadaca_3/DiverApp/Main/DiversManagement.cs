using dvincija_zadaca_3.DiverApp.Composite;
using dvincija_zadaca_3.DiverApp.Flyweight;
using dvincija_zadaca_3.DiverApp.Helpers;
using dvincija_zadaca_3.DiverApp.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Main
{
    public class DiversManagement
    {
        List<Diver> diversList = new List<Diver>();
        List<ConcreteEquipment> basicEquipment = new List<ConcreteEquipment>();
        Caretaker caretaker = new Caretaker();
        Dive dive;

        const string drySuit = "Suho odijelo";
        const string underwaterPhotographer = "Podvodni fotograf";
        const string nightDive = "Noćno ronjenje";

        int numOfUnderwaterPhotographers = 0;

        public DiversManagement(Dive dive)
        {
            this.dive = dive;
        }

        public List<Diver> GetDivers()
        {
            return diversList;
        }

        public List<ConcreteEquipment> GetBasicEquipment()
        {
            return basicEquipment;
        }

        public void RestoreDiversList()
        {
            diversList = RestoreFromMemento();
        }

        public void RemoveDiverFromList(string diverName)
        {
            Diver diverToRemove = diversList.SingleOrDefault(x => x.name == diverName);
            diversList.Remove(diverToRemove);
        }
        
        public Memento.Memento SaveToMemento()
        {
            return new Memento.Memento(diversList);
        }

        public List<Diver> RestoreFromMemento()
        {
            return caretaker.Memento.Divers;
        }
        
        public int GetNumberOfUndewaterPhotographers()
        {
            return numOfUnderwaterPhotographers;
        }

        /// <summary>
        /// Method for adding divers to divers list
        /// </summary>
        /// <param name="diversRaw">Contains file content. Every file line is one array element</param>
        public void AddDiversToList(string[] diversRaw, string[] superPowerRaw)
        {
            string[] diver;
            string name;
            string birthDate;
            string federationName;
            string certificateName;
            string level;
            int depthDeterminedByCertificate;
            CertificateHelper certHelper = new CertificateHelper();
            CertificateFlyweightFactory certificateFactory = CertificateFlyweightFactory.GetInstance();

            foreach (string d in diversRaw)
            {
                diver = d.Split(';');

                name = diver[0];
                federationName = diver[1];
                level = diver[2];
                birthDate = diver[3];

                // Validate data
                if (diver.Count() != 4 || !Validation.ValidateFederationName(federationName) || !Validation.ValidateDiverLevel(level))
                    continue;

                // Create new certificate
                certificateName = certHelper.getCertificateName(federationName, level);
                depthDeterminedByCertificate = certHelper.getDepthDeterminedByCertificate(level);
                Certificate certificate = certificateFactory.GetCertificateInstance(federationName, certificateName, level, depthDeterminedByCertificate);

                // Create new diver
                Diver diverObj = new Diver(name, birthDate, certificate, federationName);

                // Add super powers
                List<string> superPowers = CheckForDiversSuperPower(superPowerRaw, diverObj.name);
                diverObj.AddSuperPowers(superPowers);

                // Add diver to divers list
                diversList.Add(diverObj);
            }
        }

        private List<string> CheckForDiversSuperPower(string[] superPowerRaw, string diverName)
        {
            string[] superpower;
            List<string> superPowers = new List<string>();

            foreach (string x in superPowerRaw)
            {
                superpower = x.Split(';');
                if (superpower[0] == diverName)
                {
                    superPowers.Add(superpower[1]);
                }
            }

            return superPowers;
        }

        public void FilterDivers(int depth, int temperature, bool isNightDive)
        {
            int diversNo = diversList.Count();
            foreach (Diver diver in diversList.ToArray())
            {
                // Filter by depth, dry suit and night dive specialty
                if ((diver.certificate.depth + 10 < depth) ||
                     (temperature < 15 && diver.CheckIfDiverHasSuperPower(drySuit) == false) ||
                     (isNightDive == true && diver.CheckIfDiverHasSuperPower(nightDive) == false))
                {
                    diversList.Remove(diver);
                    continue;
                }

                // Count how many divers has photography specialty
                if (diver.CheckIfDiverHasSuperPower(underwaterPhotographer) == true)
                {
                    numOfUnderwaterPhotographers++;
                }
            }
            caretaker.Memento = SaveToMemento();
        }

        public void ChangeDiverEquipmentAssociation(string diverName)
        {
            Diver diver = diversList.FirstOrDefault(x => x.name == diverName);
            if (diver != null)
                diver.ChangeEquipmentAssociationType();
        }

        public void AssignEquipmentToDivers(CompositeEquipment allEquipment, Dive dive) 
        {
            foreach (Diver diver in diversList)
                allEquipment.EquipDiver(diver, dive);
            allEquipment.FindBasicEquipmentForDive(basicEquipment, dive);
        }
    }
}

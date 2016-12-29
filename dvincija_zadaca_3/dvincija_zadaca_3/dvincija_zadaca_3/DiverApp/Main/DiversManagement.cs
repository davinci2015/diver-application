using dvincija_zadaca_3.DiverApp.Flyweight;
using dvincija_zadaca_3.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Main
{
    public class DiversManagement
    {
        List<Diver> initialDiversList = new List<Diver>();
        List<Diver> diversList = new List<Diver>();

        const string drySuit = "Suho odijelo";
        const string underwaterPhotographer = "Podvodni fotograf";
        const string nightDive = "Noćno ronjenje";

        int numOfUnderwaterPhotographers = 0;

        public List<Diver> GetDivers()
        {
            return diversList;
        }

        public void AddDiverToInitialList(Diver diver)
        {
            initialDiversList.Add(diver);
        }

        public void RestoreDiversList()
        {
            diversList.Clear();
            diversList = initialDiversList.ToList();
        }
        public void RemoveDiverFromList(string diverName)
        {
            Diver diverToRemove = diversList.SingleOrDefault(x => x.name == diverName);
            diversList.Remove(diverToRemove);
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
                {
                    Console.WriteLine("{0} Preskačem pogrešan redak: {1};{2};{3};{4}\n", Validation.diverInputErr, name, federationName, level, birthDate);
                    continue;
                }

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
                AddDiverToInitialList(diverObj);
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

        public void FilterDivers(int depth, int temperature)
        {
            diversList = initialDiversList.ToList();

            foreach (Diver diver in diversList)
            {
                // Filter by depth and dry suit specialty
                if ((diver.certificate.depth + 10 < depth) ||
                     (temperature < 15 && diver.CheckIfDiverHasSuperPower(drySuit) == false))
                {
                    initialDiversList.Remove(diver);
                    continue;
                }

                // Count how many divers has photography specialty
                if (diver.CheckIfDiverHasSuperPower(underwaterPhotographer) == true)
                {
                    numOfUnderwaterPhotographers++;
                }
            }
           
            RestoreDiversList();

        }

        public void ChangeDiverEquipmentAssociation(string diverName)
        {
            Diver diver = diversList.FirstOrDefault(x => x.name == diverName);
            if (diver != null)
                diver.ChangeEquipmentAssociationType();
        }
    }
}

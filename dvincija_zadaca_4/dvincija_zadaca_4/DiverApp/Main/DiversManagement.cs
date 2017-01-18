using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_4.DiverApp.Helpers;
using dvincija_zadaca_4.DiverApp.Flyweight;

namespace dvincija_zadaca_4.DiverApp.Main
{
    public class DiversManagement
    {
        public List<Diver> diversList = new List<Diver>();
        
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

                name            = diver[0];
                federationName  = diver[1];
                level           = diver[2];
                birthDate       = diver[3];

                // Validate data
                if (!Validation.ValidateFederationName(federationName) || !Validation.ValidateDiverLevel(level))
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
                    superPowers.Add(superpower[1]);
            }

            return superPowers;
        }
    }
}

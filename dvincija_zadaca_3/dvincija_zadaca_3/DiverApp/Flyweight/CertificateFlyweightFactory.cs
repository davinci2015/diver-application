using dvincija_zadaca_3.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Flyweight
{
    public class CertificateFlyweightFactory
    {
        private static CertificateFlyweightFactory instance;
        Dictionary<string, Certificate> certificatePool;

        private CertificateFlyweightFactory()
        {
            certificatePool = new Dictionary<string, Certificate>();
        }
        public static CertificateFlyweightFactory GetInstance()
        {
            if (instance == null)
                instance = new CertificateFlyweightFactory();
            return instance;
        }

        /// <summary>
        /// Check if certificate already exists. If it exists return it otherwise create new 
        /// and add it to certificate list
        /// Key format is: authorizedFederation-name-level
        /// </summary>
        /// <param name="authorizedFederation">Certificate authorized federation name</param>
        /// <param name="name">Cerfificate name</param>
        /// <param name="level">Certificate level</param>
        /// <param name="depth">Max depth determined by certificate</param>
        /// <returns>Certificate object</returns>
        public Certificate GetCertificateInstance(string authorizedFederation, string name, string level, int depth)
        {
            string key = authorizedFederation + "-" + name + "-" + level;

            // Check if certificate already exists
            if (!certificatePool.ContainsKey(key))
            {
                Certificate cerfificate = new Certificate(authorizedFederation, name, level, depth);
                certificatePool.Add(key, cerfificate);
            }

            return certificatePool[key];
        }
    }
}

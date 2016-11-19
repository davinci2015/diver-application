using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Flyweight
{
    public class FlyweightFactory
    {
        private static FlyweightFactory instance;
        Dictionary<string, Certificate> certificatePool;
        Dictionary<string, Federation> federationsPool;
        private FlyweightFactory()
        {
            certificatePool = new Dictionary<string, Certificate>();
            federationsPool = new Dictionary<string, Federation>();
        }

        public static FlyweightFactory GetInstance()
        {
            if (instance == null)
                instance = new FlyweightFactory();
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

        /// <summary>
        /// Check if federation already exists. If it exists return it otherwise create new
        /// and add it to federations list
        /// Federation name is key
        /// </summary>
        /// <param name="federationName"></param>
        /// <returns></returns>
        public Federation GetFederationInstance(string federationName)
        {
            // Check if federation already exists
            if (!federationsPool.ContainsKey(federationName))
            {
                Federation federation = new Federation(federationName);
                federationsPool.Add(federationName, federation);
            }

            return federationsPool[federationName];
        }

        public Dictionary<string, Federation> GetAllFederations()
        {
            return federationsPool;
        }
        public int getFedCount()
        {
            return federationsPool.Count();
        }
    }
}

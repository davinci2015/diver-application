using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Helpers
{
    public class Validation
    {
        public static readonly string filePathErr = "Pogrešna putanja do datoteka!";
        public static readonly string seedErr = "Pogrešan seed!";
        public static readonly string algorithmErr = "Pogrešni nazivi algoritama! Popravi to!";
        public static readonly string diveInputErr = "Postoji pogreška u datoteci urona!";
        public static readonly string diverInputErr = "Postoji pogreška u datoteci ronioca!";

        /// <summary>
        /// Validation for file path
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>true = valid / false = invalid</returns>
        public static bool ValidateFilePath(string filePath)
        {
            return filePath.EndsWith(".txt");
        }

        /// <summary>
        /// Validate federation name - has to be SSI | CMAS | BSAC | NAUI
        /// </summary>
        /// <param name="federationName"></param>
        /// <returns>true if valid</returns>
        public static bool ValidateFederationName(string federationName)
        {
            Regex regex = new Regex(@"((^SSI$)|(^CMAS$)|(^BSAC$)|(^NAUI$))");
            return regex.IsMatch(federationName);
        }

        /// <summary>
        /// Validate Diver Category Level 
        /// </summary>
        /// <param name="level">Diver category level</param>
        /// <returns>true if valid</returns>
        public static bool ValidateDiverLevel(string level)
        {
            Regex regex = new Regex(@"(^R[0-5]$)|(^I[1-8]$)");
            return regex.IsMatch(level);
        }

        /// <summary>
        /// Validation for seed number
        /// </summary>
        /// <param name="seed">Seed</param>
        /// <returns>true = valid / false = invalid</returns>
        public static bool ValidateSeedNumber(string seed)
        {
            Regex regex = new Regex(@"^[1-9]\d{2,4}");
            return regex.IsMatch(seed);
        }


        /// <summary>
        /// Validate depth - needs to be number 1-99
        /// </summary>
        /// <param name="depth"></param>
        /// <returns>true if valid</returns>
        public static bool ValidateDepth(string depth)
        {
            Regex regex = new Regex(@"^\d{1,2}$");
            if (regex.IsMatch(depth))
            {
                if (int.Parse(depth) == 0)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Validate num of divers - need to be number 1-99
        /// </summary>
        /// <param name="numOfDivers"></param>
        /// <returns></returns>
        public static bool ValidateNumOfDivers(string numOfDivers)
        {
            Regex regex = new Regex(@"^[1-9]{1,2}$");
            return regex.IsMatch(numOfDivers);
        }

        /// <summary>
        /// Algorithm name validation
        /// </summary>
        /// <param name="algorithms">List of algorithm names</param>
        /// <returns>List of accepted algorithm names</returns>
        public static List<string> ValidateAlgorithmNames(string[] algorithms)
        {
            int numOfDistinctAlgorithms = algorithms.Distinct().Count();

            List<string> acceptedAlgorithms = new List<string>();

            Regex regex = new Regex(@"((^AlgMaksUron$)|(^AlgIstaKategUron$)|(^AlgSlucUron$))");
            foreach (string algorithm in algorithms)
                if (regex.IsMatch(algorithm))
                    acceptedAlgorithms.Add(algorithm);

            // if there is two or more existing algorithms then check if there's maybe two or more same algorithms
            if (acceptedAlgorithms.Count() >= 2 && numOfDistinctAlgorithms <= 2)
                acceptedAlgorithms.Clear();

            return acceptedAlgorithms;
        }
    }
}

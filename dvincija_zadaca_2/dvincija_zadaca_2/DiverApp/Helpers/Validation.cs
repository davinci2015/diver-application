using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Helpers
{
    public static class Validation
    {
        public static readonly string filePathErr = "Invalid file path!";
        public static readonly string seedErr = "Invalid seed!";
        public static readonly string algorithmErr = "Invalid algorithm input!";
        public static bool ValidateFilePath(string filePath)
        {
            return filePath.EndsWith(".txt");
        }
        public static bool ValidateSeedNumber(string seed)
        {
            Regex regex = new Regex(@"^[1-9]\d{2,4}");
            return regex.IsMatch(seed);
        }
        public static bool ValidateAlgorithmNames(string[] algorithms)
        {
            int numOfExistingAlgorithms = 0;
            int numOfDistinctAlgorithms = algorithms.Distinct().Count();

            Regex regex = new Regex(@"((^AlgMaksUron$)|(^AlgIstaKategUron$)|(^AlgSlucUron$))");
            foreach(string algorithm in algorithms)
                if(regex.IsMatch(algorithm))
                    numOfExistingAlgorithms++;
            
            // If there is no existing algorithms
            if (numOfExistingAlgorithms == 0)
                return false;

            // if there is two or more existing algorithms then check if there's maybe two or more same algorithms
            if (numOfExistingAlgorithms >= 2 && numOfDistinctAlgorithms >= 2)
                return false;

            return true;
        }
    }
}

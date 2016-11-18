using dvincija_zadaca_1.DiverApp;
using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool validArguments = true;
            string errMsg = "";

            if (!args.Count().Equals(7))
                Console.WriteLine("Warning: Invalid number of arguments");
            else
            {
                int seed = 0;

                // File paths
                string diversFilePath = args[1];
                string diveScheduleFilePath = args[2];
                string outFilePath = args[6];

                // Algorithm names
                string[] algorithms = new string[] {
                    args[3],
                    args[4],
                    args[5]
                };

                // Seed validation
                if (!Validation.ValidateSeedNumber(args[0]))
                {
                    validArguments = false;
                    errMsg = Validation.seedErr;
                }
                else
                {
                    seed = Int32.Parse(args[0]);
                }
                    
                if (!Validation.ValidateFilePath(diversFilePath) || !Validation.ValidateFilePath(diveScheduleFilePath) || Validation.ValidateFilePath(outFilePath))
                {
                    validArguments = false;
                    errMsg = Validation.filePathErr;
                }
                
                if (!Validation.ValidateAlgorithmNames(algorithms))
                {
                    validArguments = false;
                    errMsg = Validation.algorithmErr;
                }

                if (validArguments)
                {
                    DiverApplication APP = new DiverApplication(seed, diversFilePath, diveScheduleFilePath, algorithms, outFilePath);
                    APP.Init();
                }
                else
                    Console.WriteLine(errMsg);
            }

            Console.ReadLine();
        }
    }
}

using dvincija_zadaca_1.DiverApp;
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

            if (!args.Count().Equals(7))
                Console.WriteLine("Warning: Invalid number of arguments");
            else
            {
                // Seed
                int seed = Int32.Parse(args[0]);
                bool result = Int32.TryParse(args[0], out seed);

                // Seed validation
                if (!result && seed >= 100)
                    validArguments = false;

                // File paths
                string diversFilePath = args[1];
                string diveScheduleFilePath = args[2];
                if (!diversFilePath.EndsWith(".txt") || !diveScheduleFilePath.EndsWith(".txt"))
                    validArguments = false;

                // Output file validation
                string outFilePath = args[6];
                if (!outFilePath.EndsWith(".txt"))
                    validArguments = false;

                // Algorithm names
                string[] algorithms = new string[] {
                    args[3],
                    args[4],
                    args[5]
                };

                if (validArguments)
                {
                    DiverApplication APP = new DiverApplication(seed, diversFilePath, diveScheduleFilePath, algorithms, outFilePath);
                    APP.Init();
                }
                else
                    Console.WriteLine("Warning: arguments invalid!");
            }

            Console.ReadLine();
        }
    }
}

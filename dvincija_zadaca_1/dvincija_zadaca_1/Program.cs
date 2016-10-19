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
            if (!args.Count().Equals(5))
                Console.WriteLine("Invalid number of arguments!");
            else
            {
                int seed                    = Int32.Parse(args[0]);
                string diversFilePath       = args[1];
                string diveScheduleFilePath = args[2];
                string algorithmName        = args[3];
                string outFilePath          = args[4];

                DiverApplication APP = new DiverApplication(seed, diversFilePath, diveScheduleFilePath, algorithmName, outFilePath);
                APP.Init();
            }

            Console.ReadLine();
        }
    }
}

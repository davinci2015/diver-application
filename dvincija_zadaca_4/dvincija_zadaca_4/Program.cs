using dvincija_zadaca_4.DiverApp.Helpers;
using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!args.Count().Equals(6))
                Console.WriteLine("Neispravan broj argumenata!");
            else
            {
                // Save arguments to variables
                int seed = Int32.Parse(args[0]);
                string filePathDivers = args[1];
                string filePathDives = args[2];
                string filePathSpecialty = args[3];
                string fileEquipmentPath = args[4];
                int maxLoanDays = Int32.Parse(args[5]);

                // Read files
                string[] diversRaw      = Reader.ReadFile(filePathDivers);
                string[] dives          = Reader.ReadFile(filePathDives);
                string[] superPowerRaw  = Reader.ReadFile(filePathSpecialty);
                string[] equipmentRaw   = Reader.ReadFile(fileEquipmentPath);

                // Set divers
                DiversManagement diversManagement = new DiversManagement();
                diversManagement.AddDiversToList(diversRaw, superPowerRaw);

                // Set dives
                DiveManagement diveManagement = new DiveManagement(diversManagement.diversList);
                diveManagement.AddDivesToList(dives);
                diveManagement.AssignDiversToDive();

                // Print
                Writer.PrintDives(diveManagement.DiveList.AsEnumerable());

                Console.ReadLine();
            }
        }
    }
}

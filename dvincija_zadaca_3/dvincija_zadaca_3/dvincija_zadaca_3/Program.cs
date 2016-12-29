using dvincija_zadaca_3.DiverApp.Controller;
using dvincija_zadaca_3.DiverApp.Helpers;
using dvincija_zadaca_3.DiverApp.Helpers.File;
using dvincija_zadaca_3.DiverApp.Main;
using dvincija_zadaca_3.DiverApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool validArguments = true;

            if (!args.Count().Equals(10))
            {
                Console.WriteLine("Neispravan broj argumenata!");
            }             
            else
            {   
                if (validArguments)
                {

                    // Save arguments to variables
                    int rowNumber               = Int32.Parse(args[0]);
                    int colNumber               = Int32.Parse(args[1]);
                    int cacheRowNumber          = Int32.Parse(args[2]);
                    string filePathDivers       = args[3];
                    string filePathSpecialty    = args[4];
                    string fileEquipmentPath    = args[5];
                    int depth                   = Int32.Parse(args[6]);
                    int temperature             = Int32.Parse(args[7]);
                    bool night                  = args[8] == "0" ? false : true;
                    int camera                  = Int32.Parse(args[9]);

                    
                    // Read files
                    string[] diversRaw = Reader.ReadFile(filePathDivers);
                    string[] superPowerRaw = Reader.ReadFile(filePathSpecialty);
                    string[] equipmentRaw = Reader.ReadFile(fileEquipmentPath);


                    // Model
                    DiversManagement diversManagement = new DiversManagement();
                    EquipmentManagement equipmentManagement = new EquipmentManagement();
                    // View
                    DiversView diversView = new DiversView();
                    // Controller
                    DiversController diversController = new DiversController(diversManagement, diversView);


                    // Add controller to view
                    diversView.AddController(diversController);

                    diversManagement.AddDiversToList(diversRaw, superPowerRaw);
                    equipmentManagement.LoadEquipment(equipmentRaw);
                    diversManagement.FilterDivers(depth, temperature);
                    diversController.printDivers();

                }

                else
                {
                    Console.WriteLine("Ne valja");
                }

            }
        }
    }
}

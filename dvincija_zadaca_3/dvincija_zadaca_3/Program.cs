using dvincija_zadaca_3.DiverApp.Composite;
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
            if (!args.Count().Equals(10))
                Console.WriteLine("Neispravan broj argumenata!");          
            else
            {   
                // Save arguments to variables
                int rowNumber               = Int32.Parse(args[0]);
                int colNumber               = Int32.Parse(args[1]);
                int cacheCapacity           = Int32.Parse(args[2]);
                string filePathDivers       = args[3];
                string filePathSpecialty    = args[4];
                string fileEquipmentPath    = args[5];
                int depth                   = Int32.Parse(args[6]);
                int temperature             = Int32.Parse(args[7]);
                bool nightDive              = args[8] == "0" ? false : true;
                int camera                  = Int32.Parse(args[9]);

                // Validation 
                rowNumber = (rowNumber >= 24 && rowNumber <= 40) ? rowNumber : 24;
                colNumber = (colNumber >= 80 && colNumber <= 160) ? colNumber : 80;
                cacheCapacity = (cacheCapacity >= rowNumber && cacheCapacity <= 400) ? cacheCapacity : rowNumber;
                depth = (depth >= 5 && depth <= 40) ? depth : 40;
                temperature = (temperature >= 0 && temperature <= 35) ? temperature : 10;
                camera = (camera >= 0 && camera <= 5) ? camera : 0;

                // Read files
                string[] diversRaw = Reader.ReadFile(filePathDivers);
                string[] superPowerRaw = Reader.ReadFile(filePathSpecialty);
                string[] equipmentRaw = Reader.ReadFile(fileEquipmentPath);

                Dive dive = new Dive(depth, temperature, nightDive, camera);

                // Cache init
                Cache cache = Cache.GetInstance();
                cache.InitCache(cacheCapacity * colNumber, rowNumber - 1);

                // Model
                DiversManagement diversManagement = new DiversManagement(dive);
                EquipmentManagement equipmentManagement = new EquipmentManagement();
                // View
                DiversView diversView = new DiversView(rowNumber, colNumber);
                // Controller
                DiversController diversController = new DiversController(diversManagement, diversView);
                    
                diversManagement.AddDiversToList(diversRaw, superPowerRaw);
                diversManagement.FilterDivers(depth, temperature, nightDive);

                equipmentManagement.LoadEquipment(equipmentRaw);
                diversManagement.AssignEquipmentToDivers(equipmentManagement.GetAllEquipment(), dive);

                // Add controller to view
                diversView.StateController = diversController;
                diversController.Initialize();
            }
        }
    }
}

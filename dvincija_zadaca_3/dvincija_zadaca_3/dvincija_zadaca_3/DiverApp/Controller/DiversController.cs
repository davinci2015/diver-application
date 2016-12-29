using dvincija_zadaca_3.DiverApp.Main;
using dvincija_zadaca_3.DiverApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Controller
{
    public class DiversController
    {
        private DiversManagement model;
        private DiversView view;
        
        public DiversController(DiversManagement model, DiversView view)
        {
            this.model = model;
            this.view = view;
        }

        public void RestoreDivers()
        {
            model.RestoreDiversList();
        }

        public void RemoveDiverFromList(string diverName)
        {
            model.RemoveDiverFromList(diverName);
        }

        public void printDivers()
        {
            view.printDivers(model.GetDivers());
        }

        public void HandleInput(string input)
        {
            Regex regex = new Regex(@"N\s.+");
            if (input.Length > 1)
            {
                // Change equipment association type
                if (regex.IsMatch(input))
                {
                    string diverName = input.Split(' ')[1];
                    model.ChangeDiverEquipmentAssociation(diverName);
                }

                // Delete diver
                else
                {
                    model.RemoveDiverFromList(input);
                }
            }

            else
            {
                switch (input)
                {
                    case "V":
                        model.RestoreDiversList();
                        break;
                    case "P":
                        printDivers();
                        break;
                    case "N":
                        Console.Write("Nastavak");
                        break;
                    case "Q":
                        Console.Write("Exit");
                        break;
                }
            }
        }
    }
}

using dvincija_zadaca_3.DiverApp.Helpers;
using dvincija_zadaca_3.DiverApp.Main;
using dvincija_zadaca_3.DiverApp.Memento;
using dvincija_zadaca_3.DiverApp.State;
using dvincija_zadaca_3.DiverApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Controller
{
    public class DiversController : StateController
    {
        public DiversController(DiversManagement model, DiversView view)
        {
            this.model = model;
            this.view = view;
        }

        public override void Initialize()
        {
            view.ClearCache();
            PrintDivers();
        }

        public void RestoreDivers()
        {
            model.RestoreDiversList();
        }

        public void RemoveDiverFromList(string diverName)
        {
            model.RemoveDiverFromList(diverName);
        }

        public void PrintDivers()
        {
            view.PrintDivers(model.GetDivers());
        }

        public override void RequestInput()
        {
            Console.Write("snoob@UZDIZ:~$ ");
            string input = Console.ReadLine();
            HandleInput(input);
        }

        protected override void HandleInput(string input)
        {
            if (input.Length == 1)
            {
                switch (input)
                {
                    case "V":
                        model.RestoreDiversList();
                        PrintDivers();
                        break;
                    case "P":
                        PrintDivers();
                        break;
                    case "D":
                        if (!view.PageDown())
                            view.ShowMsg("Ne može više nazad!", "red");
                        else
                            view.PrintToConsole();
                        break;
                    case "G":
                        if(!view.PageUp())
                            view.ShowMsg("Nema dalje!", "red");
                        else
                            view.PrintToConsole();
                        break;
                    case "N":
                        view.StateController = new EquipmentController(model, view);
                        view.StateController.Initialize();
                        break;
                    case "Q":
                        Environment.Exit(0);
                        break;
                    default:
                        view.ShowMsg("Pročitati uputstva za korištenje!", "red");
                        break;
                }   
            }
            else
            {
                Regex regex = new Regex(@"N\s.+");
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
                PrintDivers();
            }
        } 
    }
}

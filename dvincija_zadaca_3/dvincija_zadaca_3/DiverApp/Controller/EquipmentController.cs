using dvincija_zadaca_3.DiverApp.Helpers;
using dvincija_zadaca_3.DiverApp.Main;
using dvincija_zadaca_3.DiverApp.State;
using dvincija_zadaca_3.DiverApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Controller
{
    public class EquipmentController : StateController
    {
        public EquipmentController(DiversManagement model, DiversView view)
        {
            this.model = model;
            this.view = view;
        }

        public override void Initialize()
        {
            view.ClearCache();
            view.ShowMsg("Unesi naredbu za nastavak", "green");
        }

        protected override void HandleInput(string input)
        {
            if (input.Length == 1)
            {
                switch (input)
                {
                    case "O":
                        view.PrintBasicEquipment(model.GetBasicEquipment());
                        break;
                    case "D":
                        if (!view.PageDown())
                            view.ShowMsg("Ne može više nazad!", "red");
                        else
                            view.PrintToConsole();
                        break;
                    case "G":
                        if (!view.PageUp())
                            view.ShowMsg("Nema dalje!", "red");
                        else
                            view.PrintToConsole();
                        break;
                    case "V":
                        view.StateController = new DiversController(model, view);
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
                Diver diver = model.GetDivers().FirstOrDefault(x => x.name == input);
                if (diver != null)
                    view.PrintDiverEquipment(diver);
                else
                    view.ShowMsg("Gdje se skriva, kud je nestao??", "red");
            }
        }
        
        public override void RequestInput()
        {
            Console.Write("snoob@UZDIZ:~$ ");
            string input = Console.ReadLine();
            HandleInput(input);
        }
    }
}

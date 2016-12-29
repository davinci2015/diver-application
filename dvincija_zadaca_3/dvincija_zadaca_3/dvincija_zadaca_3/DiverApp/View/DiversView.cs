using dvincija_zadaca_3.DiverApp.Controller;
using dvincija_zadaca_3.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.View
{
    public class DiversView
    {
        private DiversController controller;
        private static readonly String ANSI_ESC = "\x1B[";
        internal void SeparateScreen(int rowNo, int columnNo)
        {
            Console.WriteLine(ANSI_ESC + rowNo + ";" + columnNo + "H");
        }
        public void printDivers(List<Diver> divers)
        {
            Console.Write(ANSI_ESC + "2J");
            foreach (Diver diver in divers)
            {
                Console.Write("{0} - {1} - {2}\n", diver.name, diver.birthDate, diver.GetEquipmentAssociationType());
            }
            RequestInput();
        }

        public void AddController(DiversController controller)
        {
            this.controller = controller;
        }

        public void RequestInput()
        {
            string input = Console.ReadLine();
            controller.HandleInput(input);
            RequestInput();
        }
    }
}

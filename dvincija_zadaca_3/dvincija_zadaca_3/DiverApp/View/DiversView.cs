using dvincija_zadaca_3.DiverApp.Composite;
using dvincija_zadaca_3.DiverApp.Controller;
using dvincija_zadaca_3.DiverApp.Helpers;
using dvincija_zadaca_3.DiverApp.Main;
using dvincija_zadaca_3.DiverApp.State;
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
        int rowNumber;
        int colNumber;
       
        StateController controller;
        Cache cache = Cache.GetInstance();
        VT100 mašinerija = new VT100();

        public DiversView(int rowNumber, int colNumber)
        {
            this.rowNumber = rowNumber;
            this.colNumber = colNumber;
            mašinerija.ClearScreen();
        }
        
        public StateController StateController
        {
            get { return controller; }
            set { controller = value; }
        }

        public void ShowMsg(string msg, string color)
        {
            mašinerija.ChangeForegroundColor(color);
            mašinerija.EraseLine();
            Console.Write(msg);
            mašinerija.ChangeForegroundColor("white");
            SetCursorPositionForInput();
        }

        public void SetCursorPositionForInput()
        {
            mašinerija.SetCursorPosition(rowNumber, 0);
            mašinerija.EraseLine();
            controller.RequestInput();
        }
       
        public void PrintDivers(List<Diver> divers)
        {
            string print;
            string equipmentAssociation;
            foreach (Diver diver in divers)
            {
                equipmentAssociation = diver.IsBasicEquipmentAssociation() ? "Osnovno" : "Napredno";
                print = string.Format("{0}\t {1}.\t - {2}", diver.name, diver.birthDate, equipmentAssociation);
                SaveToCache(print);
            }
            AddEndLineDecorationToCache();
            PrintToConsole();
        }

        public void PrintDiverEquipment(Diver diver)
        {
            string print;
            foreach(ConcreteEquipment equipment in diver.GetDiverEquipment() )
            {
                print = string.Format("{0} {1}", equipment.ID, equipment.GetName());
                SaveToCache(print);
            }
            AddEndLineDecorationToCache();
            PrintToConsole();
        }

        public void PrintBasicEquipment(List<ConcreteEquipment> equipment)
        {
            string print;
            foreach (ConcreteEquipment item in equipment)
            {
                print = string.Format("{0} {1}", item.ID, item.GetName());
                SaveToCache(print);
            }
            AddEndLineDecorationToCache();
            PrintToConsole();
        }

        private void SaveToCache(string text)
        {
            cache.SaveToCache(text);
        }

        private string[] GetCache()
        {
            return cache.GetCache();
        }

        public void ClearCache()
        {
            cache.ClearCache();
        }

        public bool PageDown()
        {
            return cache.PageDown();
        }

        public bool PageUp()
        {
            return cache.PageUp();
        }

        void AddEndLineDecorationToCache()
        {
            SaveToCache("---------------------------");
        }

        public void PrintToConsole()
        {
            string[] print = GetCache();
            int cursorRowPosition = rowNumber - print.Length;

            mašinerija.ClearScreen();
            mašinerija.SetCursorPosition(cursorRowPosition, 0);

            foreach (string x in print)
                Console.WriteLine(x);

            SetCursorPositionForInput();
        }
    }
}

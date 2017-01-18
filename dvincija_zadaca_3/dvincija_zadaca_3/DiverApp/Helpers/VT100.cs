using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.Helpers
{
    public class VT100
    {
        static readonly String ANSI_ESC = "\x1B[";
        Dictionary<string, string> colors = new Dictionary<string, string>() {
            { "white", "37" },
            { "black", "30" },
            { "red", "31"   },
            { "green", "32" }
        };
        public void SetCursorPosition(int rowPosition, int colPosition)
        {
            Console.Write("{0}{1};{2}f", ANSI_ESC, rowPosition, colPosition);
        }
        public void ClearScreen()
        {
            Console.Write(ANSI_ESC + "2J");
        }
        public void EraseLine()
        {
            Console.Write(ANSI_ESC + "2K");
        }
        public void ChangeForegroundColor(string color)
        {
            Console.Write(ANSI_ESC + colors[color] + "m");
        }
    }
}

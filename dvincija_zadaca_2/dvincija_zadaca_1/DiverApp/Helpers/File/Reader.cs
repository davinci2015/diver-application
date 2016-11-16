using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Helpers
{
    public static class Reader
    {
        public static string[] ReadFile(string path)
        {
            path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\", path);
            string[] content = null;

            try
            {
                content = System.IO.File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Warning - {0} - file not found!", path);
            }

            return content;
        }
    }
}

using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Helpers
{
    public class Writer
    {
        private static string outputFilePath; 
        public static string OutputFilePath {
            get { return outputFilePath; }
            set { OutputFilePath = value; }
        }

        /// <summary>
        /// Print all divers and their dives
        /// </summary>
        /// <param name="dives"></param>
        public static void PrintDives(IEnumerable<Dive> dives)
        {
            StringBuilder builder = new StringBuilder();
            int diverCounter = 1;

            builder.Append(GenerateHeading("PODACI O URONIMA"));

            foreach (Dive dive in dives)
            {
                diverCounter = 1;
                builder.AppendFormat("\n\n###### URON ######\n");
                builder.AppendFormat("Datum: \t\t{0}\nVrijeme: \t{1}\nTemperatura: \t{2}°C\nBroj ronioca: \t{3}/{4}\n\n", dive.dateTime.ToString("dd.MM.yyyy"), dive.dateTime.ToString("HH:MM"), dive.temperature, dive.NumOfDivers, dive.numOfDiversNeeded);

                builder.AppendFormat("{0,-4}{1,-16}{2,-20}{3}\n", "#", "IME RONIOCA", "DATUM ROĐENJA", "CERTIFIKAT");
                builder.AppendFormat("{0,-4}{1,-16}{2,-20}{3}\n", "--", "-----------", "-------------", "----------");

                foreach (Diver diver in dive.Divers)
                    builder.AppendFormat("{0,-4}{1,-16}{2,-20}{3}\n", diverCounter++, diver.name, diver.birthDate, diver.certificate.name);
            }

            //AppendTextToFile(outputFilePath, builder.ToString());
            Console.WriteLine(builder.ToString());
        }

        /// <summary>
        /// Generate pretty heading
        /// </summary>
        /// <param name="heading">Heading</param>
        /// <returns>string</returns>
        private static string GenerateHeading(string heading)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("\n*********************************************************************************\n");
            builder.AppendFormat("*{0}\n", "*".PadLeft(80));
            builder.AppendFormat("*{0}{1}\n", heading.PadLeft(40), "*".PadLeft(40));
            builder.AppendFormat("*{0}\n", "*".PadLeft(80));
            builder.AppendFormat("*********************************************************************************\n\n");

            return builder.ToString();
        }


        private static void AppendTextToFile(string path, String content)
        {
            File.AppendAllText(path, content);
        }

        public static void CreateFile(string fileName)
        {
            File.Delete(fileName);
            File.Create(fileName).Close();
        }
    }
}

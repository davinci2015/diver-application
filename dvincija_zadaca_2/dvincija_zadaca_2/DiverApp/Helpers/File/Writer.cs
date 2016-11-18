using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Helpers
{
    public class Writer
    {
        public static void WriteSafetyMeasuresForDive(IEnumerable<DiveSchedule> diveSchedule, string path)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3}\n", "DATUM", "BROJ RONIOCA", "MAX DUBINA", "MJERA SIGURNOSTI");
            builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3}\n", "-----------", "-----------", "-----------", "-----------");

            foreach (DiveSchedule schedule in diveSchedule)
            {
                builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3}\n", schedule.date, schedule.numOfDivers, schedule.maxDepth, schedule.safetyMeasure.ToString("#.##"));
            }
            Console.WriteLine(builder);
        }

        public static void WriteDivers(IEnumerable<Diver> divers, string path)
        {
            StringBuilder builder = new StringBuilder();

            foreach (Diver diver in divers)
            {
                builder.Append("\n\n---------------------------------------------------------\n");
                builder.AppendFormat("{0,-10}{1,-10}\n", diver.name, diver.birthDate);

                int diveCounter = 1;
                builder.AppendFormat("{0,-10}{1,-20}{2,-20}\n", "#", "DATUM URONA", "BROJ RONIOCA");
                foreach (DiveSchedule dive in diver.diveSchedule)
                {
                    builder.AppendFormat("{0,-10}{1,-20}{2,-20}\n", diveCounter++, dive.date, dive.numOfDivers);
                }
                builder.Append("---------------------------------------------------------\n");
            }
            Console.WriteLine(builder);
            AppendTextToFile(path, builder.ToString());
        }

        public static void StatisticsForFederation(Dictionary<string, Federation> federations, string path)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("{0,-20}{1,-20}{2,-20}\n", "FEDERACIJA", "UKUPAN BROJ URONA", "PROSJEČNA DUBINA");
            foreach (var federation in federations)
                builder.AppendFormat("{0,-20}{1,-20}{2,-20}\n", federation.Value.federationName, federation.Value.CountNumOfDives(), federation.Value.CalculateAverageDepth().ToString("#.##"));

            Console.WriteLine(builder);
            AppendTextToFile(path, builder.ToString());
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

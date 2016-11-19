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

            builder.Append(GenerateHeading("SIGURNOSNE MJERE URONA"));

            builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3}\n", "DATUM", "BROJ RONIOCA", "MAX DUBINA", "MJERA SIGURNOSTI");
            builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3}\n", "-----", "------------", "----------", "----------------");

            foreach (DiveSchedule schedule in diveSchedule)
                builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3}\n", schedule.date, schedule.numOfDivers, schedule.maxDepth, schedule.safetyMeasure.ToString("#.##"));

            AppendTextToFile(path, builder.ToString());
        }

        public static void WriteDivers(IEnumerable<Diver> divers, string path)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(GenerateHeading("RONIOCI I NJIHOVI URONI"));

            builder.AppendFormat("{0,-5}{1,-16}{2,-15}{3,-17}{4,-17}{5,-20}{6}\n", "#", "IME RONIOCA", "DATUM URONA", "VRIJEME URONA", "BROJ RONIOCA", "MJERA SIGURNOSTI", "ALGORITAM");
            builder.AppendFormat("{0,-5}{1,-16}{2,-15}{3,-17}{4,-17}{5,-20}{6}\n","--", "-----------", "-----------", "-------------", "------------", "----------------", "---------");

            foreach (Diver diver in divers)
            {
                int diveCounter = 1;
                foreach (DiveSchedule dive in diver.diveSchedule)
                    builder.AppendFormat("{0,-5}{1,-16}{2,-15}{3,-17}{4,-17}{5,-20}{6}\n", diveCounter++, diver.name, dive.date, dive.time, dive.numOfDivers, dive.safetyMeasure, dive.algorithmName, dive.algorithmName);
                builder.AppendFormat("\n");
            }
            
            AppendTextToFile(path, builder.ToString());
        }

        public static void PrintSafetyCheck(Dictionary<string, DiveSchedule> safetyCheckList, string path)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(GenerateHeading("PROVJERA SIGURNOSNIH RIZIKA URONA"));

            builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3,-20}\n", "INSTITUCIJA", "MIN. SIGURNOST", "DATUM", "DUBINA");
            builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3}\n",     "-----------", "--------------", "-----", "------");

            foreach (var safetyCheck in safetyCheckList)
                builder.AppendFormat("{0,-20}{1,-20}{2,-20}{3,-20}\n", safetyCheck.Key, safetyCheck.Value.safetyMeasure, safetyCheck.Value.date, safetyCheck.Value.maxDepth);
            
            AppendTextToFile(path, builder.ToString());
        }

        public static void StatisticsForFederation(Dictionary<string, Federation> federations, string path)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(GenerateHeading("STATISTIČKI PODACI O URONIMA"));

            builder.AppendFormat("{0,-20}{1,-20}{2,-20}\n", "FEDERACIJA", "UKUPAN BROJ URONA", "PROSJEČNA DUBINA");
            builder.AppendFormat("{0,-20}{1,-20}{2,-20}\n", "-----------", "----------------", "----------------");
            foreach (var federation in federations)
                builder.AppendFormat("{0,-20}{1,-20}{2,-20}\n", federation.Value.institutionName, federation.Value.CountNumOfDives(), federation.Value.CalculateAverageDepth().ToString("#.##"));

            builder.AppendFormat("\n{0,-15}{1,-5}{2,-5}{3,-5}{4,-5}{5,-5}{6,-5}{7,-5}{8,-5}{9,-5}{10,-5}{11,-5}{12,-5}{13,-5}{14,-5}\n", "FEDERACIJA", "R0", "R1", "R2", "R3", "R4", "R5", "I1", "I2", "I3", "I4", "I5", "I6", "I7", "I8");
            foreach (var federation in federations)
            {
                List<int> numOfDivers = new List<int>();

                foreach (CertificateAbsoluteLevel level in Enum.GetValues(typeof(CertificateAbsoluteLevel)))
                    numOfDivers.Add(federation.Value.divers.Where(x => x.certificate.level == level.ToString()).Count());

                builder.AppendFormat("{0,-15}", federation.Key);
                builder.AppendFormat("{0,-5}\n", GenerateTxtForLevels(numOfDivers));
            }
              
            AppendTextToFile(path, builder.ToString());
        }

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

        private static string GenerateTxtForLevels(List<int> numOfDivers)
        {
            StringBuilder builder = new StringBuilder();

            foreach(int num in numOfDivers)
                builder.AppendFormat("{0,-5}", num);

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

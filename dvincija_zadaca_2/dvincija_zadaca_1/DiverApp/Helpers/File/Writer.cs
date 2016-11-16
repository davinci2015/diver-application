using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Helpers
{
    public static class Writer
    {
        public static void WriteDiveSchedule(IEnumerable<DiveSchedule> diveSchedule, string path)
        {
            path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\", path);

            StringBuilder builder = new StringBuilder();

            foreach(DiveSchedule schedule in diveSchedule)
            {
                int groupNumber = 1;
                builder.Append("_______________________________________________________\n\n");
                builder.AppendFormat("\t\t  DIVE\n\t\t{0}\n\t\t  {1}\n\n", schedule.date, schedule.time);
                builder.AppendFormat(" Depth: {0}m\n Number of divers: {1}\n\n", schedule.maxDepth, schedule.numOfDivers);

                foreach (PairHelper group in schedule.diveGroups)
                {
                    builder.AppendFormat(" Group {0}:\n", groupNumber++);
                    foreach(Diver diver in group.diverPair)
                    {
                        builder.AppendFormat("\t{0} ({1}m) \t- ({2}) > {3} ({4})\n", diver.name, group.maxDepth, diver.certificate.authorizedFederation, diver.certificate.name, diver.certificate.level);
                    }
                }

                builder.Append("\n_______________________________________________________\n\n\n");
            }
            Console.WriteLine(builder);
            File.AppendAllText(path, builder.ToString());
        }

        public static void WriteDivers(IEnumerable<Diver> divers, string filename)
        {
            StringBuilder builder = new StringBuilder();

            foreach(Diver diver in divers)
            {
                builder.Append("\n\n_______________________________________________________\n");
                builder.AppendFormat(" ({0}) {1} - {2}\n\n", diver.certificate.authorizedFederation, diver.certificate.name, diver.certificate.level);
                builder.AppendFormat("\t\t{0}\n\t\t{1}\n", diver.name, diver.birthDate);

                int diveCounter = 1;
                foreach (DiveSchedule dive in diver.diveSchedule)
                {
                    builder.AppendFormat("\n\n\n {0}. DIVE\n {1}\n", diveCounter++, dive.date);
                    builder.Append(" -----------------\n");

                    var list = dive.diveGroups.Where(x => x.diverPair.Any(z => z.name == diver.name));

                    foreach (PairHelper p in list)
                    {
                        builder.AppendFormat(" Max. dive depth {0}m\n", p.maxDepth);
                        builder.Append(" Group:\n");
                        foreach (Diver d in p.diverPair)
                            builder.AppendFormat("\t{0} - ({1}) > {2} ({3})\n", d.name, d.certificate.authorizedFederation, d.certificate.name, d.certificate.level);
                    }

                }
                builder.Append("\n_______________________________________________________\n\n");
            }
            Console.WriteLine(builder);
            File.AppendAllText(filename, builder.ToString());
        }

        public static void CreateFile(string fileName)
        {
            File.Delete(fileName);
            File.Create(fileName).Close();
        }
    }
}

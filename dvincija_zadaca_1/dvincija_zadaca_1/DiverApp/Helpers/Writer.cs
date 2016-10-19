using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Helpers
{
    public static class Writer
    {
        public static void WriteDiveSchedule(IEnumerable<DiveSchedule> diveSchedule)
        {
            StringBuilder builder = new StringBuilder();

            foreach(DiveSchedule schedule in diveSchedule)
            {
                int groupNumber = 1;
                builder.Append("╔═══════════════════════════════════════════╗\n\n");
                builder.AppendFormat("\t\t   DIVE\n\t\t{0}\n\t\t  {1}\n\n", schedule.date, schedule.time);
                builder.AppendFormat(" Depth: {0}m\n Number of divers: {1}\n\n", schedule.maxDepth, schedule.numOfDivers);

                foreach (PairHelper group in schedule.diveGroups)
                {
                    builder.AppendFormat(" Group {0}:\n", groupNumber++);
                    foreach(Diver diver in group.diverPair)
                    {
                        builder.AppendFormat("\t{0} ({1}m)\n", diver.name, group.maxDepth);
                    }
                }

                builder.Append("\n╚═══════════════════════════════════════════╝\n\n\n");
            }
            Console.WriteLine(builder);
        }

        public static void WriteDivers(Diver diver)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("\n\n╔═══════════════════════════════════════════╗\n");
            builder.AppendFormat(" ({0}) {1} - {2}\n\n", diver.certificate.authorizedFederation, diver.certificate.name, diver.certificate.level);
            builder.AppendFormat("\t\t{0}\n\t\t{1}\n", diver.name, diver.birthDate);

            foreach(DiveSchedule dive in diver.diveSchedule)
            {
                builder.AppendFormat("\nDIVE\n{0}\n", dive.date);
                builder.Append("------------\n");

                var list = dive.diveGroups.Where(x => x.diverPair.Any(z => z.name == diver.name));

                foreach (PairHelper p in list)
                {
                    builder.AppendFormat("Max. depth: {0}m\n", p.maxDepth);
                    foreach(Diver d in p.diverPair)
                    {
                        builder.AppendFormat("{0} - {1}\n", d.name, d.certificate.level);
                    }
                }
                
            }
            builder.Append("\n╚═══════════════════════════════════════════╝\n\n\n");
            Console.WriteLine(builder);
        }
    }
}

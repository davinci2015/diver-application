using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp;
using dvincija_zadaca_1.DiverApp.Helpers;
using dvincija_zadaca_1.DiverApp.Algorithm;

namespace dvincija_zadaca_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(Int32.Parse(args[0]));

            string diversFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\", args[1]);
            string divingScheduleFile = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\", args[2]); ;
            string divigAlgorithm = args[3];
            string outTxt = args[4];

            string[] diversRaw = System.IO.File.ReadAllLines(diversFile);
            string[] scheduleRaw = System.IO.File.ReadAllLines(divingScheduleFile);
            
            string[] diver;
            string[] timeline;
            string date = "";


            List<Diver> divers = new List<Diver>();

            List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
            CertificateHelper certHelper = new CertificateHelper();

            foreach (string d in diversRaw)
            {
                diver = d.Split(';');
                Certificate cert = new Certificate(diver[1], certHelper.getCertificateName(diver[1], diver[2]), diver[2], certHelper.setDepthDeterminedByCertificate(diver[2]));
                divers.Add( new Diver(diver[0], diver[3], cert));
            }

            foreach (string s in scheduleRaw)
            {
                timeline = s.Split(';');

                DiveSchedule schedule = new DiveSchedule(timeline[0], timeline[1], Int32.Parse(timeline[2]), Int32.Parse(timeline[3]));
                diveSchedule.Add(schedule);
            }

            // Order dive schedules by date ascending then descending by number of divers
            diveSchedule = diveSchedule.OrderBy(x => x.date).ThenByDescending(x => x.numOfDivers).ToList();

            // Generate random divers list for each dive
            // and assign each diver to group of 2 or 3 divers

            DiveAlgorithmFactory diveFactory = new DiveAlgorithmFactory();
            DiveAlgorithmProduct algorithm = diveFactory.createAlgorithm(args[3]);

            for (int i = 0; i < diveSchedule.Count(); i++)
            {

                // if dive is on same day as previous one then use same list of divers
                if (diveSchedule[i].date.Equals(date))
                {
                    diveSchedule[i].divers = new List<Diver>(diveSchedule[i - 1].divers);

                    // if there's less divers then on previous dive on same day then remove N divers from list
                    if (diveSchedule[i].divers.Count() > diveSchedule[i].numOfDivers)
                        diveSchedule[i].divers.RemoveRange(diveSchedule[i].numOfDivers - 1, diveSchedule[i].divers.Count() - diveSchedule[i].numOfDivers);
                   
                }
                else
                {
                    diveSchedule[i].setDivers(divers, random, diveSchedule[i].numOfDivers);
                }

                List<PairHelper> diveGroup = algorithm.GetDivePairs(diveSchedule[i].divers, diveSchedule[i].numOfDivers);
                diveSchedule[i].setDiveGroup(diveGroup);

                date = diveSchedule[i].date;
            }

            for (int i = 0; i < diveSchedule.Count(); i++)
            {
                Console.WriteLine("------------------------------------------\n" + diveSchedule[i].date + "Diver Num: " + diveSchedule[i].numOfDivers + "\n");
                foreach(PairHelper h in diveSchedule[i].diversGroup)
                {
                    foreach(Diver d in h.diverPair)
                    {
                        Console.WriteLine(d.name + " " + d.certificate.level + " " + d.certificate.depth);
                    }
                    Console.WriteLine("Max depth in group: " + h.maxDepth + "\n");
                }
            }


            Console.ReadLine();
        }
    }
}

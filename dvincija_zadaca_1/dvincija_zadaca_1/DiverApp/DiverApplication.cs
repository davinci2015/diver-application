using dvincija_zadaca_1.DiverApp.Algorithm;
using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp
{
    public class DiverApplication
    {
        string diversFilePath { get; set; }
        string diveScheduleFilePath { get; set; }
        string algorithmName { get; set; }
        string outFilePath { get; set; }
        Random random { get; set; }

        List<Diver> divers = new List<Diver>();
        List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
        CertificateHelper certHelper = new CertificateHelper();

        public DiverApplication(int seed, string diversFilePath, string diveScheduleFilePath, string algorithmName, string outFilePath)
        {
            this.diversFilePath = diversFilePath;
            this.diveScheduleFilePath = diveScheduleFilePath;
            this.algorithmName = algorithmName;
            this.outFilePath = outFilePath;
            this.random = new Random(seed);
        }

        private string[] ReadFile(string path)
        {
            path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\", path);
            string[] content = System.IO.File.ReadAllLines(path);
            return content;
        }

        private void AddDiversToList(string[] diversRaw)
        {
            string[] diver;
            foreach (string d in diversRaw)
            {
                diver = d.Split(';');
                Certificate cert = new Certificate(diver[1], certHelper.getCertificateName(diver[1], diver[2]), diver[2], certHelper.setDepthDeterminedByCertificate(diver[2]));
                divers.Add(new Diver(diver[0], diver[3], cert));
            }
        }

        private void AddDiveSchedule(string[] scheduleRaw)
        {
            string[] timeline;
            foreach (string s in scheduleRaw)
            {
                timeline = s.Split(';');

                DiveSchedule schedule = new DiveSchedule(timeline[0], timeline[1], Int32.Parse(timeline[2]), Int32.Parse(timeline[3]));
                diveSchedule.Add(schedule);
            }
        }

        private void AddDiversToDiveSchedule(DiveAlgorithmProduct algorithm)
        {
            string date = "";

            // Order dive schedules by date ascending then descending by number of divers
            diveSchedule = diveSchedule.OrderBy(x => x.date).ThenByDescending(x => x.numOfDivers).ToList();

            // Generate random divers list for each dive
            // and assign each diver to group of 2 or 3 divers
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

                List<PairHelper> diveGroup = algorithm.GetDivePairs(diveSchedule[i].divers, diveSchedule[i]);
                diveSchedule[i].setDiveGroups(diveGroup);

                date = diveSchedule[i].date;
            }
        }

        public void Init()
        {

            string[] diversRaw = ReadFile(diversFilePath);
            string[] scheduleRaw = ReadFile(diveScheduleFilePath);

            AddDiversToList(diversRaw);
            AddDiveSchedule(scheduleRaw);

            DiveAlgorithmFactory diveFactory = new DiveAlgorithmFactory();
            DiveAlgorithmProduct algorithm = diveFactory.createAlgorithm(algorithmName);

            AddDiversToDiveSchedule(algorithm);

            Writer.WriteDiveSchedule(diveSchedule.AsEnumerable());

            foreach (Diver diver in divers)
                Writer.WriteDivers(diver);
            
        }
    }
}

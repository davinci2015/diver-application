using dvincija_zadaca_1.DiverApp.Algorithms;
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
        string[] algorithms { get; set; }
        string outFilePath { get; set; }
        Random random { get; set; }

        List<Diver> divers = new List<Diver>();
        List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
        Dictionary<string, Federation> federations = new Dictionary<string, Federation>();
        CertificateHelper certHelper = new CertificateHelper();
        DivingClubSingleton divingClub = DivingClubSingleton.GetInstance();

        public DiverApplication(int seed, string diversFilePath, string diveScheduleFilePath, string[] algorithms, string outFilePath)
        {
            this.diversFilePath = diversFilePath;
            this.diveScheduleFilePath = diveScheduleFilePath;
            this.algorithms = algorithms;
            this.outFilePath = outFilePath;
            this.random = new Random(seed);
        }

        /// <summary>
        /// Method for adding divers to divers list
        /// </summary>
        /// <param name="diversRaw">Contains file content. Every file line is one array element</param>
        private void AddDiversToList(string[] diversRaw)
        {
            string[] diver;
            string name;
            string birthDate;
            string federationName;
            string level;

            foreach (string d in diversRaw)
            {
                diver = d.Split(';');

                name            = diver[0];
                federationName  = diver[1];
                level           = diver[2];
                birthDate       = diver[3];  

                // If federation doesn't exist in dictionary then create it 
                if (!federations.ContainsKey(federationName))
                {
                    // Add federation to federations list
                    federations.Add(federationName, new Federation(federationName));

                    // Add federation as observer 
                    divingClub.addObserver(federations[federationName]);
                }
                   

                Certificate cert = new Certificate(federationName, certHelper.getCertificateName(federationName, level), level, certHelper.getDepthDeterminedByCertificate(level));
                divers.Add(new Diver(name, birthDate, cert, federationName));
            }
        }

        /// <summary>
        /// Method for adding divers to schedule list
        /// </summary>
        /// <param name="scheduleRaw">Contains file content. Every file line is one array element</param>
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

        /// <summary>
        /// Method for random assigning divers to each dive schedule
        /// </summary>
        private void AddDiversToDiveSchedule()
        {
            string date = "";

            // Order dive schedules by date ascending then descending by number of divers
            diveSchedule = diveSchedule.OrderBy(x => x.date).ThenByDescending(x => x.numOfDivers).ToList();

            // Generate random divers list for each dive
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
                    // Add divers to schedule
                    diveSchedule[i].setDivers(divers, random, diveSchedule[i].numOfDivers);
                }

                date = diveSchedule[i].date;
            }
        }

        /// <summary>
        /// For each dive schedule run each algorithm and calculate safety measure sum for each dive
        /// After that choose algorithm with smallest safety measure sum because it represents highest level of safety
        /// </summary>
        /// <param name="algorithms">Array of algorithm names</param>
        private void TestAlgorithms(string[] algorithms)
        {
            AlgorithmFactoryAbstract diveFactory = new DiveAlgorithmFactory();

            // We'll save generated diver groups into dictionary with algorithm name as key
            Dictionary<String, List<PairHelper>> diverGroupsByAlgorithm = new Dictionary<string, List<PairHelper>>();

            // Test each algorithm for each dive
            for (int i = 0; i < diveSchedule.Count(); i++)
            {
                // safety measure sum for dive
                float safetyMeasureMax = 0;
                float safetyMeasureForDive;
                string chosenAlgorithmName = "";

                // Max dive depth
                float maxDiveDepth = diveSchedule[i].maxDepth; 

                foreach(string algorithm in algorithms)
                {
                    DiveAlgorithmProductAbstract currentAlgorithm = diveFactory.createAlgorithm(algorithm);

                    // Generate dive groups with passed algorithm
                    List<PairHelper> diveGroup = currentAlgorithm.GetDivePairs(diveSchedule[i].divers, diveSchedule[i]);

                    // Add dive group to dictionary
                    diverGroupsByAlgorithm.Add(algorithm, diveGroup);

                    // Calculate safety measure sum for group
                    safetyMeasureForDive = PairHelper.CalculateSafetyMeasureSum(diveGroup);

                    // If safetyMeasureForGroup is greater than currently max safetyMeasureSum then safetyMeasureForGroup is max
                    if (safetyMeasureForDive > safetyMeasureMax)
                    {
                        safetyMeasureMax = safetyMeasureForDive;
                        chosenAlgorithmName = algorithm;
                    }
                }

                // Add dive groups to dive
                diveSchedule[i].setDiveGroups(diverGroupsByAlgorithm[chosenAlgorithmName]);

                // Add safety measure to dive
                diveSchedule[i].safetyMeasure = safetyMeasureMax;
              
                // Add used algorithm name to dive
                diveSchedule[i].algorithmName = chosenAlgorithmName;

                // Add dive to each diver
                foreach (Diver d in diveSchedule[i].divers)
                    d.addDive(diveSchedule[i]);

                // Add dive to dive club
                divingClub.setDive(diveSchedule[i]);

                // Clear dictionary with dive groups because it needs to be empty for next iteration
                diverGroupsByAlgorithm.Clear();
            }
        }

        public void Init()
        {
            string[] diversRaw = Reader.ReadFile(diversFilePath);
            string[] scheduleRaw = Reader.ReadFile(diveScheduleFilePath);

            if (diversRaw != null && scheduleRaw != null)
            {
                AddDiversToList(diversRaw);
                AddDiveSchedule(scheduleRaw);
                AddDiversToDiveSchedule();
                TestAlgorithms(algorithms);
                
                Writer.CreateFile(outFilePath);
                Writer.WriteSafetyMeasuresForDive(diveSchedule.AsEnumerable(), outFilePath);
                Writer.WriteDivers(divers.AsEnumerable(), outFilePath);
                Writer.StatisticsForFederation(federations, outFilePath);
            }
        }
    }
}

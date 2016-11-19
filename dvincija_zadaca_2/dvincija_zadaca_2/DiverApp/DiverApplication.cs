using dvincija_zadaca_1.DiverApp.Algorithms;
using dvincija_zadaca_1.DiverApp.Flyweight;
using dvincija_zadaca_1.DiverApp.Helpers;
using dvincija_zadaca_1.DiverApp.Main_Classes;
using dvincija_zadaca_1.DiverApp.Visitor;
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
        List<string> algorithms { get; set; }
        string outFilePath { get; set; }
        Random random { get; set; }

        List<Diver> divers = new List<Diver>();
        List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
        Dictionary<string, Federation> federations = new Dictionary<string, Federation>();
        CertificateHelper certHelper = new CertificateHelper();
        DivingClubSingleton divingClub = DivingClubSingleton.GetInstance();
        FlyweightFactory certificateFlyweightFactory = FlyweightFactory.GetInstance();
        InstitutionAbstract HRS = new NationalDivingAssociation("HRS");
        InstitutionVisitor institutionVisitor = new ConcreteInstitutionVisitor();

        public DiverApplication(int seed, string diversFilePath, string diveScheduleFilePath, List<string> algorithms, string outFilePath)
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
            string certificateName;
            string level;
            int depthDeterminedByCertificate;

            foreach (string d in diversRaw)
            {
                diver = d.Split(';');

                name            = diver[0];
                federationName  = diver[1];
                level           = diver[2];
                birthDate       = diver[3];

                // Validate data
                if (diver.Count() != 4 || !Validation.ValidateFederationName(federationName) || !Validation.ValidateDiverLevel(level))
                {
                    Console.WriteLine("{0} Preskačem pogrešan redak: {1};{2};{3};{4}\n", Validation.diverInputErr, name, federationName, level, birthDate);
                    continue;
                }

                // If federation doesn't exist in dictionary then create it 
                Federation federation = certificateFlyweightFactory.GetFederationInstance(federationName);
                // Add federation as observer 
                divingClub.addObserver(federation);

                // Create new certificate
                certificateName = certHelper.getCertificateName(federationName, level);
                depthDeterminedByCertificate = certHelper.getDepthDeterminedByCertificate(level);
                Certificate certificate = certificateFlyweightFactory.GetCertificateInstance(federationName, certificateName, level, depthDeterminedByCertificate);

                // Create new diver
                Diver diverObj = new Diver(name, birthDate, certificate, federationName);
                // Add diver to divers list
                divers.Add(diverObj);
                // Add diver to federation
                federation.divers.Add(diverObj);
            }
        }

        /// <summary>
        /// Method for adding divers to schedule list
        /// </summary>
        /// <param name="scheduleRaw">Contains file content. Every file line is one array element</param>
        private void AddDiveSchedule(string[] scheduleRaw)
        {
            string[] timeline;
            string date;
            string time;
            string maxDepth;
            string numOfDivers;

            foreach (string s in scheduleRaw)
            {
                timeline = s.Split(';');

                date = timeline[0];
                time = timeline[1];
                maxDepth = timeline[2];
                numOfDivers = timeline[3];

                if (timeline.Count() != 4 || !Validation.ValidateDepth(maxDepth) || !Validation.ValidateNumOfDivers(numOfDivers))
                {
                    Console.WriteLine("{0} Preskačem pogrešan redak: {1};{2};{3};{4}\n", Validation.diveInputErr, date, time, maxDepth, numOfDivers);
                    continue;
                }

                DiveSchedule schedule = new DiveSchedule(date, time, Int32.Parse(maxDepth), Int32.Parse(numOfDivers));
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

                if (diveSchedule[i].numOfDivers > divers.Count())
                {
                    Console.WriteLine("Pogreška za uron {0} - {1} Nemoguće je da postoji uron sa većim brojem ronioca nego je njih stvaran broj!", diveSchedule[i].date, diveSchedule[i].time);
                    diveSchedule.Remove(diveSchedule[i--]);
                    continue;
                }

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
                    diveSchedule[i].SetDivers(divers, random, diveSchedule[i].numOfDivers);
                }

                date = diveSchedule[i].date;
            }
        }


        /// <summary>
        /// Visit institution and get minimum safe dive
        /// </summary>
        /// <param name="institution">Institution object that we want to visit</param>
        /// <param name="visitor">Visitor object</param>
        /// <returns>Dive that have minimum safety measure for institution</returns>
        private DiveSchedule DiveSafetyCheck(InstitutionAbstract institution, InstitutionVisitor visitor)
        {
            var safetyCheck = visitor.Visit(institution);
            return safetyCheck;
        }

        /// <summary>
        /// For each dive schedule run each algorithm and calculate safety measure sum for each dive
        /// After that choose algorithm with smallest safety measure sum because it represents highest level of safety
        /// </summary>
        /// <param name="algorithms">Array of algorithm names</param>
        private void TestAlgorithms(List<string> algorithms)
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
                diveSchedule[i].diveGroups = diverGroupsByAlgorithm[chosenAlgorithmName];

                // Add safety measure to dive
                diveSchedule[i].safetyMeasure = safetyMeasureMax;
              
                // Add used algorithm name to dive
                diveSchedule[i].algorithmName = chosenAlgorithmName;

                // Add dive to each diver
                foreach (Diver d in diveSchedule[i].divers)
                    d.AddDive(diveSchedule[i]);

                // Add dive to dive club and to national diving association
                divingClub.SetDive(diveSchedule[i]);

                // Clear dictionary with dive groups because it needs to be empty for next iteration
                diverGroupsByAlgorithm.Clear();
            }
        }

        public void Init()
        {
            string[] diversRaw = Reader.ReadFile(diversFilePath);
            string[] scheduleRaw = Reader.ReadFile(diveScheduleFilePath);

            divingClub.addObserver(HRS);
            AddDiversToList(diversRaw);
            AddDiveSchedule(scheduleRaw);
            AddDiversToDiveSchedule();
            TestAlgorithms(algorithms);

            var federations = certificateFlyweightFactory.GetAllFederations();
            var safetyCheckList = new Dictionary<string, DiveSchedule>();

            Writer.CreateFile(outFilePath);
            Writer.WriteSafetyMeasuresForDive(diveSchedule.AsEnumerable(), outFilePath);
            Writer.WriteDivers(divers.AsEnumerable(), outFilePath);
            Writer.StatisticsForFederation(federations, outFilePath);
                
            safetyCheckList[HRS.institutionName] = DiveSafetyCheck(HRS, institutionVisitor);
            foreach (var federation in federations)
                safetyCheckList[federation.Key] = DiveSafetyCheck(federation.Value, institutionVisitor);

            Writer.PrintSafetyCheck(safetyCheckList, outFilePath);
            Console.WriteLine("Podaci uspješno zapisani u " + outFilePath);
        }
    }
}

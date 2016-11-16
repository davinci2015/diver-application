using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Algorithm.ConcreteProducts
{
    public class PartnerAtSameLevelAlg : DiveAlgorithmProduct
    {
        public override List<PairHelper> GetDivePairs(List<Diver> diversList, DiveSchedule diveSchedule)
        {
            List<Diver> divers = new List<Diver>(diversList);
            List<PairHelper> pairList = new List<PairHelper>();
            List<Diver> leftovers = new List<Diver>();
            int numOfDivers = diveSchedule.numOfDivers;
            bool tripleAdded = false;

            // Create groups for divers who have their pair
            // And create list with divers who don't have their pair
            while (numOfDivers != 0)
            {
                int counter = 1;
                List<int> diverPositions = new List<int>();
                string firstLvlInArr = divers[0].certificate.level;

                for (int i = 1; i < divers.Count(); i++)
                {
                    if (firstLvlInArr == divers[i].certificate.level)
                    {
                        diverPositions.Add(i);
                        counter++;
                    }
                }

                if (numOfDivers == 3 && leftovers.Count() != 1)
                {
                    numOfDivers -= 3;
                    pairList.Add(new PairHelper(new List<Diver>() { divers[0], divers[1], divers[2] }, diveSchedule.maxDepth));
                }

                // Odd number of divers
                else if (diveSchedule.numOfDivers % 2 != 0)
                {
                    if (counter >= 3 && !tripleAdded)
                    {
                        tripleAdded = true;
                        numOfDivers -= 3;
                        pairList.Add(new PairHelper(new List<Diver>() { divers[0], divers[diverPositions[0]], divers[diverPositions[1]] }, diveSchedule.maxDepth));
                        divers.RemoveAt(0);
                        divers.RemoveAt(diverPositions[0] - 1);
                        divers.RemoveAt(diverPositions[1] - 2);
                    }
                    else if (counter >= 2)
                    {
                        numOfDivers -= 2;
                        pairList.Add(new PairHelper(new List<Diver>() { divers[0], divers[diverPositions[0]] }, diveSchedule.maxDepth));
                        divers.RemoveAt(0);
                        divers.RemoveAt(diverPositions[0] - 1);
                    }
                    else if (counter == 1)
                    {
                        leftovers.Add(divers[0]);
                        divers.RemoveAt(0);
                        numOfDivers--;
                    }
                }

                else if (diveSchedule.numOfDivers % 2 == 0)
                {
                    if (counter >= 2)
                    {
                        numOfDivers -= 2;
                        pairList.Add(new PairHelper(new List<Diver>() { divers[0], divers[diverPositions[0]] }, diveSchedule.maxDepth));
                        divers.RemoveAt(0);
                        divers.RemoveAt(diverPositions[0] - 1);
                    }
                    else if (counter == 1)
                    {
                        leftovers.Add(divers[0]);
                        divers.RemoveAt(0);
                        numOfDivers--;
                    }
                }
            }

            // Create groups from leftovers :(
            numOfDivers = leftovers.Count();
            int start = 0;

            while (numOfDivers > 0)
            {
                if (numOfDivers % 2 == 0)
                {
                    numOfDivers -= 2;
                    pairList.Add(new PairHelper(new List<Diver>() { leftovers[start], leftovers[start + 1] }, diveSchedule.maxDepth));
                    start += 2;
                }
                else if (numOfDivers % 2 != 0)
                {
                    numOfDivers -= 3;
                    pairList.Add(new PairHelper(new List<Diver>() { leftovers[start], leftovers[start + 1], leftovers[start + 2] }, diveSchedule.maxDepth));
                    start += 3;
                }
            }

            foreach (Diver d in diversList)
                d.addDive(diveSchedule);

            return pairList;
        }
    }
}

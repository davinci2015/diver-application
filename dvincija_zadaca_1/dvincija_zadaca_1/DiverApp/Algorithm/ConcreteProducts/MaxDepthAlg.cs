﻿using dvincija_zadaca_1.DiverApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Algorithm
{
    public class MaxDepthAlg : DiveAlgorithmProduct
    {
        public override List<PairHelper> GetDivePairs(List<Diver> divers, DiveSchedule diveSchedule)
        {
            List<PairHelper> pairList = new List<PairHelper>();

            // Sort diver by depth determined by certification
            divers = divers.OrderBy(x => x.certificate.depth).ToList();

            int start = 0,
                end = divers.Count() - 1,
                numOfDivers = diveSchedule.numOfDivers;

            while (numOfDivers > 0)
            {
                if (numOfDivers % 2 == 0)
                {
                    numOfDivers -= 2;
                    pairList.Add(new PairHelper(new List<Diver>() { divers[start++], divers[end--] }, diveSchedule.maxDepth));
                }
                else
                {
                    numOfDivers -= 3;
                    pairList.Add(new PairHelper(new List<Diver>() { divers[start], divers[start + 1], divers[end--] }, diveSchedule.maxDepth));
                    start += 2;
                }
            }

            foreach (Diver d in divers)
                d.addDive(diveSchedule);

            return pairList;
        }
    }
   
}

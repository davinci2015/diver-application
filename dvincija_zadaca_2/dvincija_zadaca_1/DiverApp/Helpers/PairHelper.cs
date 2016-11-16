using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp.Helpers
{
    public class PairHelper
    {
        public int maxDepth { get; set; }
        public float safetyMeasure { get; set; }

        public List<Diver> diverPair = new List<Diver>();

        public PairHelper(List<Diver> divers, int maxDiveDepth) 
        {
            this.diverPair = divers;
            this.maxDepth = maxDiveDepth;
            this.CalculateDepth(divers);
            this.CalculateSafetyMeasure(divers);
        }

        /// <summary>
        /// Calculate safety measure for dive group
        /// Safety measure = (Max. dive depth / max depth difference beetwen categories) + 1
        /// <param name="divers">List of divers</param>
        /// </summary>
        private void CalculateSafetyMeasure(List<Diver> divers)
        {
            // Get absolute levels for every diver in group
            int[] absoluteLevels = divers.Select(x => x.certificate.absoluteLevel).ToArray();

            // Get max difference beetwen absolute levels
            int absoluteLevelDifference = absoluteLevels.SelectMany((x) => absoluteLevels.Select((y) => Math.Abs(x - y))).Max();

            // Calculate safety measure for group
            safetyMeasure = (float)maxDepth / ((float)absoluteLevelDifference + 1);
        }

        /// <summary>
        /// Calculate max depth for dive group
        /// </summary>
        /// <param name="depthArr">List of divers</param>
        private void CalculateDepth(List<Diver> divers)
        {
            // Get max depth for each diver
            int[] depthArr = divers.Select(x => x.certificate.depth).ToArray();

            // Get max difference beetwen divers max depth
            int maxDepthDifference = depthArr.SelectMany((a) => depthArr.Select((b) => Math.Abs(a - b))).Max();
            int depthForDivers;

            if (maxDepthDifference >= 20)
                depthForDivers = depthArr.Min() + 10;
            else
                depthForDivers = depthArr.Max() == 40 ? 40 : depthArr.Max() + 10;
                
            maxDepth = maxDepth < depthForDivers ? maxDepth : depthForDivers;
        }

        /// <summary>
        /// Safety measure suma sumarum for all groups in one dive
        /// </summary>
        /// <param name="diveGroups">List of dive groups with their divers in dive</param>
        /// <returns>Sum of safety measure for dive</returns>
        public static float CalculateSafetyMeasureSum(List<PairHelper> diveGroups)
        {
            float safetyMeasureSum = 0;

            foreach(PairHelper group in diveGroups)
                safetyMeasureSum += group.safetyMeasure;

            return safetyMeasureSum;
        }
    }
}

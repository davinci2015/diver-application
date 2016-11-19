using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp.Main_Classes;

namespace dvincija_zadaca_1.DiverApp.Visitor
{
    public class ConcreteInstitutionVisitor : InstitutionVisitor
    {

        /// <summary>
        /// Get minimum safe dive
        /// </summary>
        /// <param name="institution">Institution we're inspecting/visiting</param>
        /// <returns>Minimum safe dive</returns>
        public DiveSchedule Visit(InstitutionAbstract institution)
        {
            DiveSchedule minSafeDive = null;
            float minSafetyMeasure = 0;
            float safetyMeasurePerDive = 0;

            foreach (DiveSchedule dive in institution.diveSchedule)
            {
                safetyMeasurePerDive = dive.safetyMeasure / dive.diveGroups.Count();
                if (safetyMeasurePerDive > minSafetyMeasure)
                {
                    minSafeDive = dive;
                    minSafetyMeasure = safetyMeasurePerDive;
                }   
            }

            return minSafeDive;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp.Observer;
using dvincija_zadaca_1.DiverApp.Visitor;

namespace dvincija_zadaca_1.DiverApp.Main_Classes
{
    public class NationalDivingAssociation : InstitutionAbstract
    {
        public NationalDivingAssociation(string institutionName)
        {
            this.institutionName = institutionName;
        }

        /// <summary>
        /// Update dive schedule list when subject transmits update
        /// </summary>
        /// <param name="subject">concrete subject</param>
        public override void Update(Subject subject)
        {
            DiveSchedule dive = subject.GetDive();
            diveSchedule.Add(dive);
        }

        /// <summary>
        /// Accept Institution visitor so he can freely discover beauty of all dives
        /// </summary>
        /// <param name="visitor">visitor</param>
        public override void Accept(InstitutionVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

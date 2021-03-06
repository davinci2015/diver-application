﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dvincija_zadaca_1.DiverApp.Observer;
using dvincija_zadaca_1.DiverApp.Helpers;
using dvincija_zadaca_1.DiverApp.Main_Classes;
using dvincija_zadaca_1.DiverApp.Visitor;

namespace dvincija_zadaca_1.DiverApp
{
    public class Federation : InstitutionAbstract
    {
        public Federation(string federationName)
        {
            this.institutionName = federationName;
        }

        /// <summary>
        /// Update dive schedule list when subject transmits update
        /// Add dive only if there's divers with federation name = this.federationName
        /// </summary>
        /// <param name="subject">concrete subject</param>
        public override void Update(Subject subject)
        {
            DiveSchedule dive = subject.GetDive();
            int diversNum = dive.diveGroups.Where(x => x.diverPair.Any(z => z.certificate.authorizedFederation == institutionName)).Count();
            if (diversNum > 0)
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

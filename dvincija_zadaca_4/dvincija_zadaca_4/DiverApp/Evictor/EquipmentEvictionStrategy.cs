using dvincija_zadaca_4.DiverApp.Composite;
using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Evictor
{
    public class EquipmentEvictionStrategy
    {
        /// <summary>
        /// Check if diver need to return equipment before current dive date 
        /// </summary>
        /// <param name="diveDate">Current dive date</param>
        /// <param name="loanDate">Date when user rent equipment</param>
        /// <param name="maxLoanDays">Number of loan days</param>
        /// <returns>true if user need to return equipment</returns>
        public bool DidLoanDateExpired(DateTime diveDate, ConcreteEquipment equipment)
        {
            return diveDate >= equipment.loanDate.AddDays(equipment.maxLoanDays);
        }
        
        /// <summary>
        /// If there is not enough diver for dive then all divers in that dive should 
        /// return the equipment to storage
        /// </summary>
        /// <param name="dive"></param>
        /// <param name="diver"></param>
        /// <returns></returns>
        public bool CheckIfEnoughDiversForDive(Dive dive, Diver diver)
        {
            return dive.NumOfDivers < dive.numOfDiversNeeded && dive.Divers.Contains(diver);
        }
    }
}

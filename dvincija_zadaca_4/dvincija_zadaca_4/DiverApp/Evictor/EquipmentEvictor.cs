using dvincija_zadaca_4.DiverApp.Helpers;
using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.Evictor
{
    public class EquipmentEvictor : IEquipmentEvictor
    {
        private EquipmentEvictionStrategy evictionStrategy = new EquipmentEvictionStrategy();

        /// <summary>
        /// Return equipment to warehouse 
        /// </summary>
        /// <param name="dive"></param>
        public void ReturnEquipment(Dive dive, List<Diver> diverList)
        {
            DateTime diveDate = dive.dateTime;

            Writer.PrintEquipmentReturnHeading(dive);

            foreach (Diver diver in diverList)
            {
                for (int i = diver.EquipmentList.Count() - 1; i >= 0; i--)
                {
                    // Check whether diver must return the equipment
                    if (evictionStrategy.DidLoanDateExpired(diveDate, diver.EquipmentList[i]) || 
                        evictionStrategy.CheckIfEnoughDiversForDive(dive, diver))
                    {
                        // Calculate return date
                        DateTime returnDate = diveDate >= diver.EquipmentList[i].loanDate.AddDays(diver.EquipmentList[i].maxLoanDays) ?
                                                          diver.EquipmentList[i].loanDate.AddDays(diver.EquipmentList[i].maxLoanDays) :
                                                          diveDate;
                        // Return equipment to storage
                        diver.EquipmentList[i].ReturnEquipment();

                        // Console log about equipment return info
                        Writer.PrintEquipmentReturn(returnDate, diver, diver.EquipmentList[i]);

                        // Delete equipment from diver
                        diver.EquipmentList.RemoveAt(i);
                    }
                }
            }
        }
    }
}

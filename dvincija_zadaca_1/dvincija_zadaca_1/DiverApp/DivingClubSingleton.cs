using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp
{
    public class DivingClubSingleton
    {
        private static DivingClubSingleton Instance;
        private static readonly Object thisLock = new Object();
        private List<Diver> divers = new List<Diver>();
        private List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
        private DivingClubSingleton() { }
        public static DivingClubSingleton GetInstance()
        {
            if (Instance != null)
                return Instance;
            else
            {
                lock(thisLock)
                {
                    if (Instance == null)
                    {
                        Instance = new DivingClubSingleton();
                    }
                }

                return Instance;
            }
        }
    }
}

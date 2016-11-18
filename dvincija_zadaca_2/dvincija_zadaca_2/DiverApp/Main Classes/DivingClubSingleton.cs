using dvincija_zadaca_1.DiverApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_1.DiverApp
{
    public class DivingClubSingleton : Subject
    {
        private List<Observer.Observer> observers = new List<Observer.Observer>();
        private List<DiveSchedule> diveSchedule = new List<DiveSchedule>();
        private List<Diver> divers = new List<Diver>();

        private static readonly Object thisLock = new Object();
        private static DivingClubSingleton Instance;

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

        public void setDive(DiveSchedule dive)
        {
            diveSchedule.Add(dive);
            notifyObservers();
        }
        public DiveSchedule getDive()
        {
            return diveSchedule.Last();
        }

        public void addObserver(Observer.Observer newObserver)
        {
            observers.Add(newObserver);
        }

        public void removeObserver(Observer.Observer observerToDelete)
        {
            observers.Remove(observerToDelete);
        }
        public void notifyObservers()
        {
            for (int i = 0; i < observers.Count(); i++)
                observers[i].Update(this);
        }
    }
}

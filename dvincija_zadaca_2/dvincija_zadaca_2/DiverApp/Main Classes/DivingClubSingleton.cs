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

        /// <summary>
        /// Add dive to list and notify all observers
        /// </summary>
        /// <param name="dive">Dive schedule object</param>
        public void SetDive(DiveSchedule dive)
        {
            diveSchedule.Add(dive);
            NotifyObserver();
        }

        /// <summary>
        /// Get last dive schedule in list
        /// </summary>
        /// <returns>Last dive schedule in dive schedule list</returns>
        public DiveSchedule GetDive()
        {
            return diveSchedule.Last();
        }

        /// <summary>
        /// Add observer to observers list
        /// </summary>
        /// <param name="newObserver">observer to add</param>
        public void addObserver(Observer.Observer newObserver)
        {
            // Don't add observer if he is already exists in observer list
            if (!observers.Contains(newObserver))    
                observers.Add(newObserver);
        }

        /// <summary>
        /// Remove observer from observers list
        /// </summary>
        /// <param name="observerToDelete">observer to delete</param>
        public void RemoveObserver(Observer.Observer observerToDelete)
        {
            observers.Remove(observerToDelete);
        }

        /// <summary>
        /// Notify all observers that update happened
        /// </summary>
        public void NotifyObserver()
        {
            for (int i = 0; i < observers.Count(); i++)
                observers[i].Update(this);
        }
    }
}

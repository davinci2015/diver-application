using dvincija_zadaca_3.DiverApp.Main;
using dvincija_zadaca_3.DiverApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_3.DiverApp.State
{
    public abstract class StateController
    {
        protected DiversView view;
        protected DiversManagement model;
        protected abstract void HandleInput(string input);
        public abstract void Initialize();
        public abstract void RequestInput();
    }
}

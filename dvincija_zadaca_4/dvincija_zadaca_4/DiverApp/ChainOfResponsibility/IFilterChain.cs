﻿using dvincija_zadaca_4.DiverApp.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dvincija_zadaca_4.DiverApp.ChainOfResponsibility
{
    public interface IFilterChain
    {
        IFilterChain NextChain { set; }
        void FilterDivers(List<Diver> divers, int numOfDiversToRemove);
    }
}

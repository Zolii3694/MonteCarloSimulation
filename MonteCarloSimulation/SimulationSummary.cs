using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloSimulation
{
    internal class SimulationSummary
    {
        public int FinalMoney { get; set; }
        public int LostAtRound { get; set; }
        public int TotalWins { get; set; }
        public int TotalLosses { get; set; }
    }
}

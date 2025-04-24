using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloSimulation
{
    internal class GameResult
    {
        public int Round {  get; set; }
        public int Bet { get; set; }
        public int Money { get; set; }
        public bool Win {  get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloSimulation
{
    internal class Player
    {
        public int Money { get; private set; }
        private int bet;

        public Player(int startingMoney, int bet) 
        {
            this.Money = startingMoney;
            this.bet = bet;
        }
        
        public void BetOnRed(Roulette roulette, int result)
        {
            if(roulette.Red(result))
            {
                Money += bet;
            }
            else if(roulette.Black(result) || roulette.Green(result))
            {
                Money -= bet;
            }
        }
    }
}

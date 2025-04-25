using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloSimulation
{
    enum Strategy
    {
        Flat,       // Fix tét
        Martingale,
        Fibonacci 
    }

    internal class Player
    {
        public int Money { get; private set; }
        private int bet;
        private int currentBet;
        private Strategy strategy;
        private int fibonacciIndex;
        private List<int> fibonacci;
        public int CurrentBet => currentBet; // read-only property

        public Player(int startingMoney, int bet, Strategy strategy) 
        {
            this.Money = startingMoney;
            this.bet = bet;
            this.currentBet = bet;
            this.strategy = strategy;
            fibonacciIndex = 0;
            fibonacci = new List<int> { 1, 1 };
        }
        
        public void BetOnRed(Roulette roulette, int result)
        {
            if (currentBet > Money)
            {
                currentBet = Money;
            }
            if (roulette.Red(result))
            {
                Money += bet;
                ApplyWinStrategy();
            }
            else if(roulette.Black(result) || roulette.Green(result))
            {
                Money -= bet;
                ApplyLossStrategy();
            }
        }

        private void ApplyWinStrategy()
        {
            switch(strategy)
            {
                case Strategy.Martingale:
                    currentBet = bet;
                    break;

                case Strategy.Fibonacci:
                    if (fibonacciIndex > 1)
                        fibonacciIndex -= 2;
                    else
                        fibonacciIndex = 0;

                    currentBet = bet * fibonacci[fibonacciIndex];
                    break;

                case Strategy.Flat:
                default:
                    currentBet = bet;
                    break;

            }
        }

        private void ApplyLossStrategy()
        {
            switch(strategy)
            {
                case Strategy.Martingale:
                    currentBet *= 2;
                    break;

                case Strategy.Fibonacci:
                    fibonacciIndex++;
                    if(fibonacciIndex >= fibonacci.Count)
                    {
                        fibonacci.Add(fibonacci[fibonacci.Count - 1] + fibonacci[fibonacci.Count - 2]);
                    }
                    currentBet = bet * fibonacci[fibonacciIndex]; 
                    break;

                case Strategy.Flat:
                default:
                    currentBet = bet;
                    break;
                    
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<GameResult> results = new List<GameResult>();

            int totalGames = 100000;
            int startingMoney = 1000;
            int bet = 10;
            int lossIndex = -1;
            Player player = new Player(startingMoney, bet, Strategy.Fibonacci); // vagy Martingale, Flat

            Roulette roulette = new Roulette();

            for (int i = 0; i < totalGames; i++)
            {
                int result = roulette.Spin();
                bool win = roulette.Red(result);

                player.BetOnRed(roulette,result);

                results.Add(new GameResult
                {
                    Round = i + 1,
                    Bet = player.CurrentBet,
                    Money = player.Money,
                    Win = win,
                });

                if(player.Money <= 0)
                {
                    lossIndex = i;
                    break;
                }
            }

            if (player.Money <= 0)
            {
                Console.WriteLine($"Jatekos penze elfogyott: {lossIndex}. korben");
            }
            else
            {
                Console.WriteLine($"Jatekos penze nem fogyott el: {player.Money} maradt");
            }

            File.WriteAllLines("results.csv", results.Select(r => $"{r.Round};{r.Bet};{r.Money};{(r.Win ? 1 : 0)}"));

            Console.ReadKey();

        }
    }
}

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

            int numberOfSimulations = 1000;
            int totalGames = 100000;
            int startingMoney = 1000;
            int bet = 10;
            Strategy strategy = Strategy.Flat; // választható: Flat, Martingale, Fibonacci

            List<SimulationSummary> summaries = new List<SimulationSummary>();
            List<GameResult> allResults = new List<GameResult>();

            for (int sim = 0; sim < numberOfSimulations; sim++)
            {
                List<GameResult> results = new List<GameResult>();
                Player player = new Player(startingMoney, bet, strategy);
                Roulette roulette = new Roulette();
                int lossIndex = -1;

                for (int i = 0; i < totalGames; i++)
                {
                    int result = roulette.Spin();
                    bool win = roulette.Red(result);

                    player.BetOnRed(roulette, result);

                    results.Add(new GameResult
                    {
                        Round = i + 1,
                        Bet = player.CurrentBet,
                        Money = player.Money,
                        Win = win,
                    });

                    if (player.Money <= 0)
                    {
                        lossIndex = i;
                        break;
                    }
                }
                        summaries.Add(new SimulationSummary
                    {
                        FinalMoney = player.Money,
                        LostAtRound = lossIndex,
                        TotalWins = results.Count(r => r.Win),
                        TotalLosses = results.Count(r => !r.Win),
                    });

                allResults.AddRange(results);
                Console.WriteLine($"[{sim + 1}/{numberOfSimulations}] Final Money: {player.Money}");
            
            }
            // Átlagok számolása
            double averageFinalMoney = summaries.Average(s => s.FinalMoney);
            int bankruptcies = summaries.Count(s => s.FinalMoney <= 0);
            double bankruptcyRate = (double)bankruptcies / numberOfSimulations * 100;

            Console.WriteLine();
            Console.WriteLine("=== ÖSSZESÍTÉS ===");
            Console.WriteLine($"Szimulációk száma: {numberOfSimulations}");
            Console.WriteLine($"Átlagos záró pénz: {averageFinalMoney:F2}");
            Console.WriteLine($"Csődök száma: {bankruptcies} ({bankruptcyRate:F2}%)");

            File.WriteAllLines("results.csv", allResults.Select(r => $"{r.Round};{r.Bet};{r.Money};{(r.Win ? 1 : 0)}"));
            File.WriteAllLines("summary.csv", summaries.Select(s => $"{s.FinalMoney};{s.LostAtRound};{s.TotalWins};{s.TotalLosses}"));

            int maxMoney = allResults.Max(r => r.Money);
            int minMoney = allResults.Min(r => r.Money);
            double averageMoney = allResults.Average(r => r.Money);
            int totalWins = allResults.Count(r => r.Win);
            int totalLosses = allResults.Count - totalWins;

            File.WriteAllLines("summary.csv", summaries.Select(s =>
                            $"{s.FinalMoney};{s.LostAtRound};{s.TotalWins};{s.TotalLosses}"
                        ));

            Console.WriteLine("CSV fájl elmentve: summary.csv");
            Console.ReadKey();
        }
    }
}

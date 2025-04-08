using System;
using System.Collections.Generic;
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
            int totalGames = 1000;
            int startingMoney = 1000;
            int bet = 10;
            int lossIndex = -1;
            Player player = new Player(startingMoney, bet);
            Roulette roulette = new Roulette();

            for (int i = 0; i < totalGames; i++)
            {
                int result = roulette.Spin();
                player.BetOnRed(roulette,result);
                if (player.Money <= 0)
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



            Console.ReadKey();

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarloSimulation
{
    internal class Roulette
    {
        protected Random rnd;
        protected List<int> red;
        protected List<int> black;
        protected int green;

        public Roulette()
        {
            rnd = new Random();
            red = new List<int>() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 29, 31, 33, 35 };
            black = new List<int>() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 30, 32, 34, 36 };
            green = 0;
        }
        
        public int Spin()
        {
            return rnd.Next(37);
        }
        
        public bool Red(int number)
        {
            return red.Contains(number);
        }

        public bool Black(int number)
        {
            return black.Contains(number);
        }

        public bool Green(int number)
        {
            return number == green;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker_dan
{
    class cashier
    {
        List<int> player;
        int sum;

        public cashier()
        {
            player = new List<int>();
            sum = 0;
        }
        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }
        public List<int> players
        {
            get { return player; }
            set { player = value; }
        }
    }

}
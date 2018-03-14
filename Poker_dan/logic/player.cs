using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Poker_dan
{
    class player :Panel
    {
        public int Money;
        public Panel PlayerCards;
        public bool In_Game;

        public player(int Money)
        {
            this.Money = Money;
            this.PlayerCards = new Panel();
            this.In_Game = true;
           
        }

    }
}

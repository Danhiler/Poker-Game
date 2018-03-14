using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Poker_dan
{
    public partial class PlayerControl : UserControl
    {
        public int Money;
        public bool In_Game;
        public int Total_Money_In_Round;
        public int Money_In_Round;
        public double Astratgy;//vs comp only

        public PlayerControl(string name ,int Money)
        {
            InitializeComponent();
            this.Money = Money;
            this.In_Game = true;
            this.PlayerName.Text = name;
            this.PlayerMoney.Text = "" + Money + "";
            Random rnd = new Random();
            this.Astratgy = (rnd.Next(30, 61) * 0.01);//0=brave 1=bonker
        }
        public PlayerControl()
        {
            InitializeComponent();
        }
       

    }
}

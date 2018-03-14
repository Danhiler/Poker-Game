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
    public partial class Options : Form
    {
        public menu Parent;
        public Options(menu Par)
        {
            InitializeComponent();
            Parent = Par;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Parent.small_blind = (int)Small_Blind.Value;
            Parent.starting_money = (int)Starting_Money.Value;
            Parent.num_of_players = (int)Num_Of_Players.Value;
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Poker_dan
{
    public partial class menu : Form
    {
        public int small_blind;
        public int num_of_players;
        public int starting_money;
        public menu()
        {
            small_blind = 5;
            num_of_players = 4;
            starting_money = 1500;
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
           Close();
        }
  

        private void start_Click(object sender, EventArgs e)
        {
            Hide();
            (new gameform(0,this)).ShowDialog();
            if (gameform.Mode)
                Show();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {//server
            Hide();
            (new gameformNet(1,this)).ShowDialog();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {//client
            Hide();
            (new Client_ip(this)).ShowDialog();
        }

        private void option_Click(object sender, EventArgs e)
        {
            Hide();
            (new Options(this)).ShowDialog();
            Show();
        }
    }
}

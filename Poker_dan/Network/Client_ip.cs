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
    public partial class Client_ip : Form
    {
        public menu Par;
        public Client_ip(menu Paren)
        {
            InitializeComponent();
            Par = Paren;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            (new gameformNet(2,textBox1.Text.ToString(),Par)).ShowDialog();
            Show();
        }
    }
}

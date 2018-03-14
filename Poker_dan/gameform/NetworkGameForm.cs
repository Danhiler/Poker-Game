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
    public partial class NetworkGameForm : Form
    {
        Server server;
        Client client;
        gameform Table;
        public int x, y;

        public NetworkGameForm(int Mode)
        {

            InitializeComponent();
            switch (Mode)
            {
                case 1:   //   server
                       server = new Server(this);
                    break;
                case 2:   //   client
                    client = new Client(this);
                    break;
            }
            x = -1;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if ( server != null)
               e.Graphics.DrawImage(global::Poker_dan.Properties.Resources.CardOff, 390, 350, 31, 38);
            if (x!=-1)
                e.Graphics.DrawImage(global::Poker_dan.Properties.Resources.CardOff, x, y, 31, 38);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (client != null)
            {
                client.Send("Connect#" + e.X + "@" + e.Y);
                x = e.X; y = e.Y;
                pictureBox1.Invalidate();
            }
        }
    }
}

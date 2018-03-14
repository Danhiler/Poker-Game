using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Poker_dan
{
    enum Suit {Heart, Spade, Dimond, Tiltan}

    class Card : UserControl
    {
        public static int WIDTH  = 73;
        public static int HEIGHT = 98;
        public int num;
        public Suit kind;
        gameform Parent;
        
        public Card(gameform Parent, int N, Suit S)
        {
            this.Parent = Parent;
            num = N;
            kind = S;
            this.Size = new Size(WIDTH, HEIGHT);
            string Name = "Cards\\" + num + "_" + (int)kind + ".bmp";
            this.BackgroundImage = Image.FromFile(Name);

        }

       
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Card
            // 
            this.Name = "Card";
            this.Load += new System.EventHandler(this.Card_Load);
            this.ResumeLayout(false);

        }

        private void Card_Load(object sender, EventArgs e)
        {

        }
    }
}

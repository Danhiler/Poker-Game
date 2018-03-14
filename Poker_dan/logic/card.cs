using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Poker_dan
{
    public enum Suit {Heart, Spade, Dimond, Tiltan}

    public class Card : UserControl
    {
        public static int WIDTH  = 73;
        public static int HEIGHT = 98;
        public int num;
        public Suit kind;
        
        public Card(int N, Suit S)
        {
            num = N;
            kind = S;
            this.Size = new Size(WIDTH, HEIGHT);
            //string Name = "C:\\Users\\dan\\Desktop\\Poker_dan\\Poker_dan\\cards\\card_off.bmp";
            //this.BackgroundImage = Image.FromFile(Name);
            this.BackgroundImage = (Image)global::Poker_dan.Properties.Resources.card_off;
            this.Hide();
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
        public void flip_on()
        {
                string Name = "Cards\\" + this.num + "_"+(int)this.kind +".bmp";
               this.BackgroundImage = Image.FromFile(Name);
        }
        public void flip_off()
        {
       string Name = "C:\\Users\\dan\\Desktop\\Poker_dan\\Poker_dan\\cards\\card_off.bmp";
       
            this.BackgroundImage = Image.FromFile(Name);
        }
    }
}

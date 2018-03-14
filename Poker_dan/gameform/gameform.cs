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
    public partial class gameform : Form
    {
        public static bool Mode = true;
        public Game game;
        protected int Temp = 0;
        public int turn_in_stage = 0;
        protected bool allin = false;
        public int this_comp_player = 0;
        public menu options;

        public gameform(int Mode,menu par)
        {//mode=0=>regular mode=1=>client mode=2=>server
            InitializeComponent();
            options = par;
            if (Mode == 0)
            {//regular
                client_loading.Visible = false;
                StartButton.Visible = false;
                Num_Of_Players.Visible = false;
                game = new Game(this, options.small_blind, options.num_of_players, options.starting_money);
            }

            if (Mode == 1)
            {//server
                client_loading.Visible = false;
                StartButton.Visible = true;
                Num_Of_Players.Visible = true;
                return;
            }

            if (Mode == 2)
            {//client
                client_loading.Visible = true;
                StartButton.Visible = false;
                Num_Of_Players.Visible = false;
                game = new Game(this);
            }

            RaiseAmount.Value = game.SmallBlind *4;
            RaiseAmount.Minimum = game.SmallBlind*4;

            if (this is gameformNet == false)
            {
                game.Computer_Turn();
                HideControls();
                RaiseAmount.Maximum = game.Players[(game.Dealer + 3) % game.NumOfPlayers].Money;
            }
        }
        private void backToMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Mode = false;
            Application.Exit();
        }
        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
        }
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            game.Delete();
            game.Init();
        }
        private void resToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Delete();
            game.Init();


        }

        public void CheakButtom_Click(object sender, EventArgs e)
        {
            if (this is gameformNet)
            {
                NetClick(1);
            }
            else
            {
                //--------------------------------------------------
                if (game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money < (game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round))
                {
                    Temp = game.CallAmount;
                    game.CallAmount = game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money + game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round;
                    this.allin = true;
                }
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money -= game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round;
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Total_Money_In_Round += (game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round);

                CummonButtn();

                if ((game.All_Money_in_Round_Iqual()) && (this.turn_in_stage >= (game.NumOfPlayers)))
                {
                    if (All_In())
                    {
                        for (int i = game.stage + 1; i <= 4; i++)
                            game.GameStage(i);
                        return;
                    }
                    else
                    {
                        game.stage += 1;
                        game.GameStage(game.stage);
                        this.turn_in_stage = 0;
                        if (game.stage == 4)
                            return;
                    }
                }
                HideControls();
                game.Computer_Turn();
            }
        }
        public void RaiseButton_Click(object sender, EventArgs e)
        {
            if (this is gameformNet)
            {
                NetClick(3);
            }
            else
            {
                game.CallAmount = (int)RaiseAmount.Value;
                CheakButton.Text = "Call " + (game.CallAmount - game.Players[((game.PlayerTurn + 1) % (game.NumOfPlayers))].Money_In_Round) + "$";
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money -= game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round;
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Total_Money_In_Round += game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round;
                CummonButtn();
                HideControls();
                game.Computer_Turn();
            }
        }
        public void FoldButton_Click(object sender, EventArgs e)
        {
            if (this is gameformNet)
            {
                NetClick(2);
            }
            else
            {
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].In_Game = false;
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].PlayerRaise.Text = "Fold!";
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].BackColor = System.Drawing.Color.DarkGray;
                game.PlayerTurn += 1;
                while (!game.Players[(game.PlayerTurn % (game.NumOfPlayers))].In_Game)
                    game.PlayerTurn += 1;
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].BackColor = System.Drawing.Color.Aquamarine;
                if ((game.All_Money_in_Round_Iqual()) && (game.PlayerTurn > (game.NumOfPlayers)))
                {
                    game.stage += 1;
                    game.GameStage(game.stage);
                    if (game.stage == 4)
                        return;
                }
                if (Is_Alone() != -1)
                {
                    List<int> a = new List<int>();
                    a.Add(Is_Alone());
                    game.SplitMoney();
                    game.Delete();
                    game.Init();
                }
                HideControls();
                game.Computer_Turn();
            }
        }

        public virtual void Send_Card_To_Clients(string p) { }
        public virtual void NetClick(int Click) {}
        public virtual void Send_cards_to_clients() { }
        public virtual void Send_Winningstring_To_Clients(string mes) { }

        
        private void CummonButtn()
        {
            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round = game.CallAmount;
            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].PlayerMoney.Text = "" + game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money + "$";
            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].PlayerRaise.Text = "" + game.CallAmount + "$";

            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].BackColor = System.Drawing.Color.Transparent;
            game.PlayerTurn += 1;
            this.turn_in_stage++;
            //----------------------------------------
            int i = 0;
            while (((!game.Players[(game.PlayerTurn % (game.NumOfPlayers))].In_Game) || (game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money == 0))&&(i<game.NumOfPlayers))
            {
                
                game.PlayerTurn += 1;
                this.turn_in_stage++;
                i++;
                if(this is gameformNet)
                Send_cards_to_clients();
            }
            //---------------------------------------------
            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].BackColor = System.Drawing.Color.Aquamarine;
            if (allin == true)
            {
                game.CallAmount = Temp;
                this.allin = false;
            }
            if (game.CallAmount > game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money)
                CheakButton.Text = "Call " + game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money + "";
            else if (game.CallAmount == game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round)
                CheakButton.Text = "Check!";
            else CheakButton.Text = "Call " + (game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round) + "$";
            RaiseAmount.Minimum = game.CallAmount + (game.SmallBlind * 2);
            RaiseAmount.Maximum = game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money;
            RaiseAmount.Value = RaiseAmount.Minimum;
            if ((game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round) > game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money)
                RaiseButton.Enabled = false;
            else
                RaiseButton.Enabled = true;
        }
        public void HideControls()
        {
            if (this.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                this.EndInvoke(this.BeginInvoke(new MethodInvoker(delegate { this.HideControls(); })));
            }
            else
            {
                if (this_comp_player == (game.PlayerTurn % (game.NumOfPlayers)))
                {
                    CheakButton.Visible = true;
                    FoldButton.Visible = true;
                    RaiseAmount.Visible = true;
                    RaiseButton.Visible = true;
                    //---------------------------------------------------------------------------
                    //List<Card> hand = new List<Card>();
                    //hand = game.GetHand(game.Players[game.PlayerTurn % game.NumOfPlayers].PlayerCards);
                    //textBox1.Text=""+ GetValByChenFormula(hand)+"";
                    //---------------------------------------------------------------------------
                }
                else
                {
                    CheakButton.Visible = false;
                    FoldButton.Visible = false;
                    RaiseAmount.Visible = false;
                    RaiseButton.Visible = false;
                }
            }
        }

        public bool All_In()
        {
            int mone = 0;
            for (int i = 0; i < game.NumOfPlayers; i++)
                if (game.Players[i].Money != 0)
                    mone++;
            return (mone <= 1);
        }
        public int Is_Alone()
        {
            int mone = 0;
            int alone=-1;
            for (int i = 0; i < game.NumOfPlayers; i++)

                if (game.Players[i].In_Game)
                {
                    mone++;
                    alone = i;
                }

            if (mone ==1 )
                return alone;
            return -1;
        }

        public virtual void Init_Start(){ }
        public virtual int SetNumOfPlayersNET() { return 4; }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Visible = false;
            Num_Of_Players.Visible = false;
            game = new Game(this, options.small_blind, SetNumOfPlayersNET(), options.starting_money);
            RaiseAmount.Value = game.SmallBlind * 4;
            RaiseAmount.Minimum = game.SmallBlind * 4;
            HideControls();
            //send details to client
            Init_Start();

            
        }   
    }   
    }

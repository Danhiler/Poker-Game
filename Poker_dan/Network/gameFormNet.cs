using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Poker_dan
{
    public class gameformNet: gameform
    {
         Server server;
         Client client;

        public gameformNet(int Mode,menu par) : base(Mode,par)//server
        {
            server = new Server(this);
        }

        public gameformNet(int Mode,string Server_IP,menu par) : base(Mode,par)//client
        {
            client = new Client(this, Server_IP);
        }

        public override void Send_Card_To_Clients(string p)
        {
            if(this.server!=null)//server
            foreach (PokerClient Player_net in server.Clients)
                Player_net.Send("AddCard#" + p);
        }
        public override void Send_Winningstring_To_Clients(string msg)
        {
            if (server != null)//Server
                foreach (PokerClient Player_net in server.Clients)
                    Player_net.SendWinningString(msg);
        }
        public override void NetClick(int Click) 
        {
            if (server == null && client == null)
                return;
            if (server != null)
            { // Server
                switch (Click)
                {
                    case 1:  // Check
                        Check();
                        foreach (PokerClient Player_net in server.Clients)
                            Player_net.Update_Clients();
                        break;
                    case 2:  // Fold
                        Fold();
                        foreach (PokerClient Player_net in server.Clients)
                            Player_net.Update_Clients();
                        break;
                    case 3:  // Raise
                        Raise(int.Parse(RaiseAmount.Value.ToString()));
                        foreach (PokerClient Player_net in server.Clients)
                            Player_net.Update_Clients();
                        break;
                }
            }
            else
            {   // Client
                switch (Click)
                {
                    case 1:  // Check
                        client.Send("Update#Check");
                        break;
                    case 2:  // Fold
                        client.Send("Update#Fold");
                        break;
                    case 3:  // Raise
                        client.Send("Update#Raise#" + (int)RaiseAmount.Value);
                        break;
                }
            }
        
        }
        public override void Init_Start() 
        {
            if(server!=null)//Server
            {
                int index = 1;
                foreach (PokerClient Player_net in server.Clients)
                {
                    Player_net.Send("SetThisCompPlayer#"+index+"");
                    Player_net.SendPlayers();
                    index++;
                }
            }
        }
        public override int SetNumOfPlayersNET() 
        {
            return (server.Clients.Count+1);
        }
        public void Check()
        {
            if (this.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                this.EndInvoke(this.BeginInvoke(new MethodInvoker(delegate { this.Check(); })));
            }
            else
            {
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
                        for (int i = game.stage + 1; i <= 4; i++)
                            game.GameStage(i);
                    else
                    {
                        game.stage += 1;
                        game.GameStage(game.stage);
                        this.turn_in_stage = 0;
                    }

                }

                HideControls();
            }
        }
        public void Fold()
        {
            if (this.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                this.EndInvoke(this.BeginInvoke(new MethodInvoker(delegate { this.Fold(); })));
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
                }
                if (Is_Alone() != -1)
                {
                    List<int> a = new List<int>();
                    a.Add(Is_Alone());
                    game.SplitMoney();
                    game.Delete();
                    game.Init();
                    System.Threading.Thread.Sleep(2000);
                    Init_Start();
                }
                HideControls();
            }
            
        }
        public void Raise(int RaiseAmount)
        {
            if (this.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                this.EndInvoke(this.BeginInvoke(new MethodInvoker(delegate { this.Raise(RaiseAmount); })));
            }
            else
            {
                game.CallAmount = RaiseAmount;
                CheakButton.Text = "Call " + (game.CallAmount - game.Players[((game.PlayerTurn + 1) % (game.NumOfPlayers))].Money_In_Round) + "$";
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money -= game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round;
                game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Total_Money_In_Round += game.CallAmount - game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round;
                CummonButtn();
                HideControls();
            }
            
        }
        private void CummonButtn()
        {
             if (this.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                this.EndInvoke(this.BeginInvoke(new MethodInvoker(delegate { this.CummonButtn(); })));
            }
            else
            {
            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money_In_Round = game.CallAmount;
            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].PlayerMoney.Text = "" + game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money + "$";
            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].PlayerRaise.Text = "" + game.CallAmount + "$";

            game.Players[(game.PlayerTurn % (game.NumOfPlayers))].BackColor = System.Drawing.Color.Transparent;
            game.PlayerTurn += 1;
            this.turn_in_stage++;
            //----------------------------------------
            int i = 0;
            while (((!game.Players[(game.PlayerTurn % (game.NumOfPlayers))].In_Game) || (game.Players[(game.PlayerTurn % (game.NumOfPlayers))].Money == 0)) && (i < game.NumOfPlayers))
            {

                game.PlayerTurn += 1;
                this.turn_in_stage++;
                i++;
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
        }


        internal void update_nop_labe()//Number Of Players
        {
            if (this.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                this.EndInvoke(this.BeginInvoke(new MethodInvoker(delegate { this.update_nop_labe(); })));
            }
            else
            {
                Num_Of_Players.Text="Number Of Players: "+(server.Clients.Count+1)+"";
            }
            
        }
    }
}

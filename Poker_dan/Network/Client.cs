using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Poker_dan
{
    class Client
    {
        TcpClient client;
        Thread  ServerThread;
        bool Done = false;
        gameformNet form;

        public Client(gameformNet form)
        {
            this.form = form;
            client = new TcpClient();
            client.Connect("10.100.101.101", 2000);
            ServerThread = new Thread(new ThreadStart(Run));
            ServerThread.Start();
        }
        public Client(gameformNet form,string server_ip)
        {
            this.form = form;
            client = new TcpClient();
            try
            {
                client.Connect(server_ip, 2000);
            }
            catch
            {
                object sender = new object();
                EventArgs e = new EventArgs();
                form.exitToolStripMenuItem1_Click(sender,e);
                return;
            }
            ServerThread = new Thread(new ThreadStart(Run));
            ServerThread.Start();
        }
        public void Send(string Mes)
        {
            NetworkStream ns = client.GetStream();
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(Mes);
            ns.Write(data, 0, data.Length);
        }
        public void Run()
        {
            NetworkStream ns = client.GetStream();
            string[] param;
            while (!Done)
            {
                byte[] data = new byte[1024];
                int receivedDataLength = ns.Read(data, 0, data.Length);
                string recdata = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                string[] items = recdata.Split('#');
                switch (items[0])
                {
                      
                    case "SetThisCompPlayer":
                        form.this_comp_player = int.Parse(items[1]);
                        break;

                    case "InitCards":
                        InitCards(items);
                        form.HideControls();
                        Playerturn(form.game.PlayerTurn);
                        break;

                    case "Update":
                        param = items[1].Split('@');
                        break;

                    case "InitGame":
                        InitGame(items);
                        break;

                    case "WinningString":
                        WinningString(items[1]);
                        break;

                    case "AddCard":
                        AddCard(items[1]);
                        break;

                }
            }
            
            client.Close();
        
        }
        private void WinningString(string p)
        {
            if (form.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                form.EndInvoke(form.BeginInvoke(new MethodInvoker(delegate { this.WinningString(p); })));
            }
            else
            {
                form.textBox1.Text = "";
            string[] param = p.Split('#');
            string[] det;
            for (int i = 0; i < param.Length; i++)
            {
                det = param[i].Split('&');
                
                Card s = (Card)form.game.Players[int.Parse(det[0])].PlayerCards.Controls[0];
                Card s1 = (Card)form.game.Players[int.Parse(det[0])].PlayerCards.Controls[1];
                        s.flip_on();
                        s1.flip_on();
                        form.textBox1.Text += det[1];
            }
            
            }
        }
        private void AddCard(string p)
        {
            if (form.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                form.EndInvoke(form.BeginInvoke(new MethodInvoker(delegate { this.AddCard(p); })));
            }
            else
            {
                string[] param = p.Split('$');
                Card card = new Card(int.Parse(param[0]), (Suit)int.Parse(param[1]));
                card.flip_on();
                card.Show();
                card.Location = new System.Drawing.Point(5 + form.TableCards.Controls.Count * (Card.WIDTH + 5), 5);
                form.TableCards.Controls.Add(card);
            }
        }
        private void InitGame(string[] items)
        {
            if (form.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                form.EndInvoke(form.BeginInvoke(new MethodInvoker(delegate { this.InitGame(items); })));
            }
            else
            {
                string[] pratim = items[1].Split('@');
                form.game.CallAmount = int.Parse(pratim[1]);
                form.TableMoney.Text = pratim[2];

                string[] players = items[2].Split('@');
                string[] updet;
                for (int i = 0; i < form.game.NumOfPlayers; i++)
                {
                    updet=players[i].Split('$');
                    form.game.Players[i].PlayerMoney.Text = updet[0]+"$";
                    form.game.Players[i].Money=int.Parse(updet[0]);
                    form.game.Players[i].In_Game=bool.Parse(updet[1]);
                    form.game.Players[i].Money_In_Round = int.Parse(updet[2]);
                    form.game.Players[i].PlayerRaise.Text = updet[2] + "$";
                    if (form.game.Players[i].In_Game)
                        form.game.Players[i].BackColor = System.Drawing.Color.Transparent;
                    else
                    {
                        form.game.Players[i].BackColor = System.Drawing.Color.Gray;
                        form.game.Players[i].PlayerRaise.Text = "Fold!";
                    }

                }
                Playerturn(int.Parse(pratim[0]));
                form.game.PlayerTurn = int.Parse(pratim[0]);
                form.HideControls();
            }
        }
        private void Playerturn(int p)
        {
            if (form.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                form.EndInvoke(form.BeginInvoke(new MethodInvoker(delegate { this.Playerturn(p); })));
            }
            else
            {
                form.game.Players[p].BackColor = System.Drawing.Color.Aquamarine;
                if (form.game.CallAmount > form.game.Players[p].Money)
                    form.CheakButton.Text = "Call " + form.game.Players[p].Money + "";
                else if (form.game.CallAmount == form.game.Players[p].Money_In_Round)
                    form.CheakButton.Text = "Check!";
                else form.CheakButton.Text = "Call " + (form.game.CallAmount - form.game.Players[p].Money_In_Round) + "$";
                form.RaiseAmount.Minimum = form.game.CallAmount + (form.game.SmallBlind * 2);
                form.RaiseAmount.Maximum = form.game.Players[p].Money;
                form.RaiseAmount.Value = form.RaiseAmount.Minimum;
                if ((form.game.CallAmount - form.game.Players[p].Money_In_Round) > form.game.Players[p].Money)
                    form.RaiseButton.Enabled = false;
                else
                    form.RaiseButton.Enabled = true;
            }
        }
        public void SendMess(string mes)
        {
            NetworkStream networkStream = client.GetStream();
            string serverResponse = mes + "$";
            Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }
        private void InitCards(string[] items)
        {
            if (form.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                form.EndInvoke(form.BeginInvoke(new MethodInvoker(delegate { this.InitCards(items); })));
            }
            else
            {
                form.game.Delete();
                form.client_loading.Visible = false;
                form.game.NumOfPlayers = items.Length - 3;
                for (int i = 0; i < form.game.NumOfPlayers; i++)
                {
                    form.game.Players.Add(new PlayerControl("Player" + i + "", 15 + i * 10));
                    form.game.Players[i].Location = new Point(450 + (int)(400 * Math.Sin(i * 2 * Math.PI / form.game.NumOfPlayers)), 260 + (int)(220 * Math.Cos(i * 2 * Math.PI / form.game.NumOfPlayers)));
                    form.game.Parent.Controls.Add(form.game.Players[i]);
                    form.game.Players[i].Show();
                    string[] cards = items[i+1].Split('$');
                    for (int j = 0; j < cards.Length - 3; j++)
                    {
                        string[] pratim = cards[j].Split('@');
                        Card card = new Card(int.Parse(pratim[0]), (Suit)int.Parse(pratim[1]));
                        if (i == form.this_comp_player)
                            card.flip_on();
                        card.Show();
                        card.Location = new System.Drawing.Point(5 + j * (Card.WIDTH + 5), 5);
                        form.game.Players[i].PlayerCards.Controls.Add(card);
                    }
                    form.game.Players[i].PlayerName.Text = cards[2];
                    form.game.Players[i].Money = int.Parse(cards[3]);
                    form.game.Players[i].PlayerMoney.Text = cards[3] + "$";
                    form.game.Players[i].Money_In_Round = int.Parse(cards[4]);
                    form.game.Players[i].PlayerRaise.Text = cards[4] + "$";
                }
                form.game.PlayerTurn = int.Parse(items[items.Length - 2]);
                form.game.CallAmount = int.Parse(items[items.Length - 1])*2;
            }
        }
        private void DeleteForm()
        {
            form.TableCards.Controls.Clear();
            form.game.Players.Clear();
            form.TableMoney.Text = "0$";
        }



    }
}

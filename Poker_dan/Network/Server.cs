using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Poker_dan
{
    public class Server
    {
        TcpListener server;
        Thread ListenerThread, ClientThread;
        public List<PokerClient> Clients = new List<PokerClient>();
        gameformNet form;

        public Server(gameformNet form)
        {
            this.form = form;
            server = new TcpListener(2000);
            server.Start();
            ListenerThread = new Thread(new ThreadStart(Run));
            ListenerThread.Start();
        }

        public void Run()
        {
            while (true)
            {
                    TcpClient Client = server.AcceptTcpClient();
                    if (Client.Connected)
                    {
                        Clients.Add(new PokerClient(Client, form, Clients));
                        form.update_nop_labe();
                    }
            }

        }

       
    }

  public class PokerClient
    {
        Thread  ClientThread;
        TcpClient Client;
        bool Done = false;
        gameformNet form;
        List<PokerClient> Clients;

        public PokerClient(TcpClient Client, gameformNet form, List<PokerClient> Clients)
        {
            this.Clients = Clients;
            this.form = form;
            this.Client = Client;
            ClientThread = new Thread(new ThreadStart(Run));
            ClientThread.Start();



        }

      public void SendPlayers()
        {
            string Mes = "InitCards#";
            for (int i = 0; i < form.game.Players.Count; i++)
            {
                foreach (Card card in form.game.GetHand(form.game.Players[i].PlayerCards))
                {
                    Mes += card.num + "@" + (int)card.kind + "$";
                }
                Mes += form.game.Players[i].PlayerName.Text + "$" + form.game.Players[i].Money + "$" +
                       form.game.Players[i].Money_In_Round + "#";
            }
            Mes += (form.game.PlayerTurn % form.game.NumOfPlayers)+"#";
            Mes += form.game.SmallBlind;
            Send(Mes);
        }

        public void Send(string Mes)
        {
            NetworkStream ns = Client.GetStream();
            byte[] data = new byte[1024];
            data = Encoding.ASCII.GetBytes(Mes);
            ns.Write(data, 0, data.Length);
        }

        private void BroadCast()
        {
            foreach (PokerClient client in Clients)
            {
                client.Send( ""  );
            }
        }
      public void Update_Clients()
      {           
                     string Mes = "InitGame#";
                       Mes += ""+(form.game.PlayerTurn%form.game.NumOfPlayers)+"@"+form.game.CallAmount+"@"+form.TableMoney.Text+"#";
                       for (int i = 0; i < form.game.Players.Count; i++)
                         {
                          Mes +=  form.game.Players[i].Money + "$" +
                                  form.game.Players[i].In_Game + "$" +
                                  form.game.Players[i].Money_In_Round + "@";
                                       }
                                   Send(Mes);

      }

        public void Run()
        {
            NetworkStream ns = Client.GetStream();
            string[] param;
            while (!Done)
            {
                byte[] data = new byte[1024];
                int receivedDataLength = ns.Read(data, 0, data.Length);
                string recdata= Encoding.ASCII.GetString(data, 0, receivedDataLength);
                string[] items = recdata.Split('#');
                switch (items[0])
                {
                    case "Connect":
                        param = items[1].Split('@');
                        break;
                    case "Update":
                        
                        if (items[1].ToString().Equals("Check"))
                        {
                            this.form.Check();
                            
                            foreach (PokerClient Player_net in Clients)
                                Player_net.Update_Clients();
                        }
                        if (items[1].ToString().Equals("Fold"))
                        {
                            this.form.Fold();
                            foreach (PokerClient Player_net in Clients)
                                Player_net.Update_Clients();
                        }
                        if (items[1].ToString().Equals("Raise"))
                        {
                            this.form.Raise(int.Parse(items[2]));
                            foreach (PokerClient Player_net in Clients)
                                Player_net.Update_Clients();
                        }
                        break;

                }

         
                
            }

        }


        internal void SendWinningString(string msg)
        {
            string Mes = "WinningString#"+msg;
            
            Send(Mes);
            
        }

    }
}

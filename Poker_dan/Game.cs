using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Poker_dan
{
    public  class Game
    {
        Stack<Card> CardsPack;
        Stack<Card> CardsPack_Cheat;
        Random R;
        public int CallAmount;
        public int NumOfPlayers;// Options
        public List<PlayerControl> Players;
        public int stage;//stage of the game;
        bool[,] Flags = new bool[13, 4];
        public int  Dealer//index of the dealer Player
            ,SmallBlind//amount of small blind
            ,PlayerTurn;//index of the currect player turn 
        public cashier []Pots;
        public int NumOfPots;
        public gameform Parent;

        public Game(gameform Parent)
        {
            this.Parent = Parent;
            this.Players = new List<PlayerControl>();
        }

        public Game(gameform Parent,int SmallBlind, int NumOfPlayers,int start_money)
        {
            NumOfPots=0;
            this.NumOfPlayers = NumOfPlayers;
            this.Parent = Parent;
            this.Dealer = -1;
            this.SmallBlind = SmallBlind;
            this.Pots = new cashier[this.NumOfPlayers];
            R = new Random();
            this.Players = new List<PlayerControl>();
            for (int i = 0; i < NumOfPlayers; i++)
            {
                this.Players.Add(new PlayerControl("Player" + i + "",start_money));
            }
            Init();
        }


        public void Init()
        {
            DrawUserHands();
            for (int i = 0; i < NumOfPlayers; i++)
            {

                    Players[i].In_Game = true;
                    Players[i].BackColor = System.Drawing.Color.Transparent;
                    Players[i].PlayerRaise.Text = "0$";
                    Players[i].Money_In_Round = 0;
                    Players[i].Total_Money_In_Round = 0;
                Pots[i] = new cashier();
            }
            Parent.TableMoney.Text = "0$";
            
            this.Dealer++;
            this.stage = 0;
            this.NumOfPots = 0;
            this.PlayerTurn = (this.Dealer + 3) % this.NumOfPlayers;
            Players[(PlayerTurn % (NumOfPlayers))].BackColor = System.Drawing.Color.Aquamarine;
            this.CallAmount = SmallBlind * 2;
            CardsPack = new Stack<Card>();
            #region Cheat //cheat
            CardsPack_Cheat = new Stack<Card>();
            CardsPack_Cheat.Push(new Card( 12, (Suit)0));
            CardsPack_Cheat.Push(new Card( 10, (Suit)0));
            CardsPack_Cheat.Push(new Card( 0, (Suit)1));
            CardsPack_Cheat.Push(new Card( 11, (Suit)0));
            CardsPack_Cheat.Push(new Card( 8, (Suit)3));

            CardsPack_Cheat.Push(new Card(8, (Suit)0));
            CardsPack_Cheat.Push(new Card(9, (Suit)0));

            CardsPack_Cheat.Push(new Card(1, (Suit)2));
            CardsPack_Cheat.Push(new Card(6, (Suit)0));

            CardsPack_Cheat.Push(new Card( 1, (Suit)2));
            CardsPack_Cheat.Push(new Card( 6, (Suit)0));

            CardsPack_Cheat.Push(new Card(0, (Suit)1));
            CardsPack_Cheat.Push(new Card(0, (Suit)3));

            //end cheat
            #endregion

            int num;
            Suit kind;
            for (int i = 0; i < 52; i++)
            {
                do
                {
                    num = R.Next(13);
                    kind = (Suit)R.Next(4);
                } while (Flags[num, (int)kind]);
                Flags[num, (int)kind] = true;
                CardsPack.Push(new Card( num, kind));
              
            }
            
            DealForUser();
            SmallBigBlinds();
        }


        public void GameStage(int Stage)
        {
            if (Parent.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                Parent.EndInvoke(Parent.BeginInvoke(new MethodInvoker(delegate { this.GameStage(Stage); })));
            }
            else
            {
                switch (Stage)
                {
                    case 1: Flop(); break;
                    case 2: Turn(); break;
                    case 3: River(); break;
                    case 4:
                        SplitMoney();
                        System.Threading.Thread.Sleep(3000);
                        Delete();
                        Init();
                        Parent.Init_Start();//net game
                        Computer_Turn();
                        Parent.HideControls();
                        return;

                }
                Players[(PlayerTurn % (NumOfPlayers))].BackColor = System.Drawing.Color.Transparent;
                this.PlayerTurn = (Dealer + 1) % (NumOfPlayers);
                //
                int t = 0;
                while (((!Players[(PlayerTurn % (NumOfPlayers))].In_Game) || (Players[(PlayerTurn % (NumOfPlayers))].Money == 0)) && (t < NumOfPlayers))
                {

                    PlayerTurn += 1;
                    Parent.turn_in_stage++;
                    t++;
                }
                //
                Players[(PlayerTurn % (NumOfPlayers))].BackColor = System.Drawing.Color.Aquamarine;
                this.CallAmount = 0;

                int sum = 0;
                for (int i = 0; i < NumOfPlayers; i++)
                {
                    sum += Players[i].Total_Money_In_Round;
                }
                Parent.TableMoney.Text = "" + sum + "$";
                Parent.CheakButton.Text = "Check!";


                for (int i = 0; i < NumOfPlayers; i++)
                {
                    Players[i].Money_In_Round = 0;
                    if (Players[i].In_Game)
                        Players[i].PlayerRaise.Text = "" + Players[i].Money_In_Round + "$";
                } 
            }
            
        }
      
        public void SmallBigBlinds() 
        {
            int i = (this.Dealer + 1) % NumOfPlayers;
           int j = (i + 1)%NumOfPlayers;
           if (Players[i].Money > this.SmallBlind)
           {
               Players[i].Money -= this.SmallBlind;
               Players[i].Money_In_Round = this.SmallBlind;
               Players[i].Total_Money_In_Round += this.SmallBlind;
           }
           else
           {
               Players[i].Money_In_Round = Players[i].Money;
               Players[i].Total_Money_In_Round += Players[i].Money;
               Players[i].Money = 0;

           }
           if (Players[j].Money > this.SmallBlind*2)
           {
               Players[j].Money -= this.SmallBlind * 2;
               Players[j].Money_In_Round = this.SmallBlind * 2;
               Players[j].Total_Money_In_Round += this.SmallBlind * 2;
           }
           else
           {
               Players[j].Money_In_Round = Players[i].Money;
               Players[j].Total_Money_In_Round += Players[i].Money;
               Players[j].Money = 0;

           }
                Players[i].PlayerMoney.Text = "" + Players[i].Money + "$";
                Players[j].PlayerMoney.Text = "" + Players[j].Money + "$";
                Players[i].PlayerRaise.Text = "" + Players[i].Money_In_Round + "$";
                Players[j].PlayerRaise.Text = "" + Players[j].Money_In_Round + "$";
                Parent.CheakButton.Text = "Call " + (2 * SmallBlind) + "$";
        
        
        }
        public void DrawUserHands()
        {
            for (int i = 0; i < NumOfPlayers; i++)
            {
                this.Players[i].Location = new Point(450 + (int)(400 * Math.Sin(i * 2 * Math.PI / NumOfPlayers)), 260 + (int)(220 * Math.Cos(i * 2 * Math.PI / NumOfPlayers)));
                Parent.Controls.Add(this.Players[i]);
                Players[i].Show();


            }
                
        }
        public void Delete()
        {
            if(CardsPack!=null)//Because its client
            CardsPack.Clear();
            Parent.TableCards.Controls.Clear();
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].PlayerCards.Controls.Clear();
                if (Players[i].Money <= 0)
                {
                    Players[i].Hide();
                    Players.RemoveAt(i);
                    NumOfPlayers--;
                    i--;
                }
            }

            Flags = new bool[13, 4];
        }
        public bool All_Money_in_Round_Iqual()
        {
            for (int i = 0; i < Players.Count - 1; i++)
                if((Players[i].In_Game)&&(Players[i+1].In_Game))
                    if(Players[i].Money!=0)
                if (Players[i].Money_In_Round != Players[i + 1].Money_In_Round)
                    return false;
            return true;
        }

        private void Flop()
        {
            if (Parent.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                Parent.EndInvoke(Parent.BeginInvoke(new MethodInvoker(delegate { this.Flop(); })));
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Card card = CardsPack.Pop();
                    card.flip_on();
                    card.Show();
                    card.Location = new System.Drawing.Point(5 + i * (Card.WIDTH + 5), 5);
                    Parent.TableCards.Controls.Add(card);
                    if (Parent is gameformNet)
                        Parent.Send_Card_To_Clients("" + card.num + "$" + (int)card.kind + "");
                    System.Threading.Thread.Sleep(1000);
                    

                }
            }
        }
        public void Turn()
        {
            if (Parent.InvokeRequired)
            {
                // thread marshalling is required, so let's do that... call ourselves when we're in the ui thread ;)
                Parent.EndInvoke(Parent.BeginInvoke(new MethodInvoker(delegate { this.Turn(); })));
            }
            else
            {
                Card card = CardsPack.Pop();
                card.flip_on();
                card.Show();
                card.Location = new System.Drawing.Point(5 + 3 * (Card.WIDTH + 5), 5);
                Parent.TableCards.Controls.Add(card);
                if (Parent is gameformNet)
                    Parent.Send_Card_To_Clients("" + card.num + "$" + (int)card.kind + "");
            }

        }
        public void River()
        {
            Card card = CardsPack.Pop();
            card.flip_on();
            card.Show();
            card.Location = new System.Drawing.Point(5 + 4 * (Card.WIDTH + 5), 5);
            Parent.TableCards.Controls.Add(card);
            if (Parent is gameformNet)
                Parent.Send_Card_To_Clients("" + card.num + "$" + (int)card.kind + "");

        }
        private void DealForUser()
        {
            for (int j = 0; j < Players.Count; j++)
            for (int i = 0; i < 2; i++)
            {
                Card card = CardsPack.Pop();
               if (j == 0)
                    card.flip_on();
                card.Show();
                card.Location = new System.Drawing.Point(5 + i * (Card.WIDTH + 5), 5);
               Players[j].PlayerCards.Controls.Add(card);
                
                
            }
        }
        public List<Card> GetHand(Panel panelUser)
        {
            List<Card> Hand = new List<Card>();
            foreach (Card c in panelUser.Controls)
                Hand.Add(c);
            foreach (Card c in Parent.TableCards.Controls)
                Hand.Add(c);
            return Hand;
        }
        public int GetFirst(List<Card> hand)//מקבלת יד של 7 קלפים ומחזירה את המספר שהיא שווה
        {
            if (Is_Straight_Flush(hand))
                return 9;
            if (Is_Four_Of_A_Kind(hand))
                return 8;
            if (Is_Full_House(hand))
                return 7;
            if (Is_Flush(hand))
                return 6;
            if (Is_Straight(hand))
                return 5;
            if (Is_Tree_Of_A_Kind(hand))
                return 4;
            if (Is_Two_Pairs(hand))
                return 3;
            if (Is_Pair(hand))
                return 2;
            return 1;
        }
        private bool Is_Pair(List<Card> hand)
        {

            for (int i = 0; i < (hand.Count - 1); i++)
                for (int j = i + 1; j < (hand.Count); j++)
                        if (hand[i].num == hand[j].num)
                        return true;
            return false;
        }
        private bool Is_Two_Pairs(List<Card> hand)
        {
            int mone = 0;
            for (int i = 0; i < (hand.Count - 1); i++)
                for (int j = i + 1; j < (hand.Count ); j++)
                        if (hand[i].num == hand[j].num)
                            mone++;
            if (mone >= 2)
                return true;

            return false;
        }
        private bool Is_Tree_Of_A_Kind(List<Card> hand)
        {
            for (int i = 0; i < (hand.Count - 2); i++)
                for (int j = i + 1; j < (hand.Count-1); j++)
                    for (int q = j + 1; q < (hand.Count); q++)
                      if ((hand[i].num == hand[j].num) && (hand[j].num== hand[q].num))
                            return true;
            return false;

        }
        private bool Is_Straight(List<Card> hand)
        {
            int mone = 0;
            bool[] mz = new bool[13];
            for (int i = 0; i < hand.Count; i++)
                    mz[hand[i].num] = true;
            for (int j = 0; j < 13; j++)
            {
                if (mz[j] == true) mone++;
                if (mone == 5) return true;
                if (mz[j] == false) mone = 0;
            }
            if ((mz[0] == true) && (mone == 4)) return true;
                return false;
            }
        private bool Is_Flush(List<Card> hand)
        {
            int []mm = new int [4];
            for (int i = 0; i < hand.Count ; i++)
                     mm[(int)hand[i].kind]++;
            for (int i = 0; i < 4; i++)
                if (mm[i] >= 5) return true;
            return false;

        }
        private bool Is_Full_House(List<Card> hand)
        {
            if (!Is_Tree_Of_A_Kind(hand)) return false;
            bool flag = true;
            List<Card> TempHand = new List<Card>();
            foreach (Card c in hand)
                TempHand.Add(c);
                for (int i = 0; i < (TempHand.Count - 2)&&flag; i++)
                    for (int j = i + 1; j < (TempHand.Count - 1) && flag; j++)
                        for (int q = j + 1; q < (TempHand.Count) && flag; q++)
                            if ((TempHand[i].num == TempHand[j].num) && (TempHand[j].num == TempHand[q].num))
                            {
                                TempHand.Remove(TempHand[q]);
                                TempHand.Remove(TempHand[j]);
                                TempHand.Remove(TempHand[i]);
                                flag = false;
                            }
                return Is_Pair(TempHand);
        }
        private bool Is_Four_Of_A_Kind(List<Card> hand)
        {
            for (int i = 0; i < (hand.Count - 3); i++)
                for (int j = i + 1; j < (hand.Count - 2); j++)
                    for (int q = j + 1; q < (hand.Count - 1); q++)
                        for (int p = q + 1; p < (hand.Count); p++)
                                if ((hand[i].num == hand[j].num) && (hand[j].num == hand[q].num) && (hand[q].num == hand[p].num))
                                    return true;
            return false;
        }
        private bool Is_Straight_Flush(List<Card> hand)
        {
            int fkind=-1;;
            if (!Is_Flush(hand))
                return false;
           
           int []mm= new int [4];
            for (int i = 0; i < hand.Count; i++)
                mm[(int)hand[i].kind]++;

            for (int i = 0; i < 4;i++)//put in fkind the kind of the flush
               if(mm[i]>=5)
            fkind = i;
            List<Card> TempHand = new List<Card>();
            foreach (Card c in hand)
                TempHand.Add(c);

            for (int i = 0; i < TempHand.Count; i++)
                if ((int)TempHand[i].kind != fkind)
                {
                    TempHand.Remove(TempHand[i]);
                    i -= 1;
                }
                    
            return (Is_Straight(TempHand));


        }
        public List<int> GetSecond(List<Card> hand,int First)
        {//מקבלת יש של 7 קלפים ואת מה שהיד שווה ומחזירה רשימה מפורטת של מה שהיא שווה            
            List<int> second = new List<int>();
            int max= -1;
            int temp;
            switch (First)
            {
                #region High Card 
                case 1://מציב את כל המספרים ב סקנד לפי גודלם
                     
                    for (int j = 0; j < 5; j++)
                    {
                        max = -1;
                        for (int i = 0; i < hand.Count; i++)
                            {
                                if ((hand[i].num > max) && (max != 0))
                                    max = hand[i].num;
                                if (hand[i].num == 0) max = 0;
                                
                            }
                        for (int i = 0; i < hand.Count; i++)
                            if (hand[i].num == max)
                            {
                                hand.Remove(hand[i]);
                                i -= 1;
                            }
                        second.Add(max);
                    }
                    break;
                #endregion
                #region Pair
                case 2://מציב ב סקנד זוג ואז 4 מספרים לפי הגודל
                    max = -1;
                    for (int i = 0; i < (hand.Count - 1); i++)
                        for (int j = i + 1; j < hand.Count; j++)
                                if (hand[i].num == hand[j].num)
                                {
                                    second.Add(hand[i].num);
                                    hand.Remove(hand[j]);
                                    hand.Remove(hand[i]);
                                }

                    for (int j = 0; j < 3; j++)
                    {
                        max = -1;
                        for (int i = 0; i < hand.Count; i++)
                            {
                                if ((hand[i].num > max)&&(max!=0))
                                    max = hand[i].num;
                                if (hand[i].num == 0) max = 0; 
                            }

                        for (int i = 0; i < hand.Count; i++)
                            if (hand[i].num == max)
                                hand.Remove(hand[i]);
                        second.Add(max);
                    }
                    break;
                #endregion
                #region Two Pairs
                case 3://מציב ב סקנד זוג גבוה אחר כך זוג נמוך ואז קלף גבוה
                    int max2=-2;
                    max = -1;
                    
                    for (int i = 0; i < (hand.Count - 1); i++)
                        for (int j = i + 1; j < hand.Count; j++)
                            if (hand[i].num == hand[j].num)
                            {
                                max2 = hand[i].num;
                                if (((max2 > max)&&(max!=0))||(max2==0))
                                {
                                    temp = max;
                                    max = max2;
                                    max2 = temp;
                                }
                            }
                    second.Add(max);
                    second.Add(max2);
                    for(int i=0;i<hand.Count;i++)
                        if ((hand[i].num == max) || (hand[i].num == max2))
                        {
                            hand.Remove(hand[i]);
                            i -= 1;
                        }
                    max=-1;
                    for (int i = 0; i < hand.Count; i++)
                    {
                        if ((hand[i].num > max) && (max != 0))
                            max = hand[i].num;
                        if (hand[i].num == 0)
                            max = hand[i].num;
                    }
                      
                            
                                
                            
                    second.Add(max);
                    break;
                #endregion
                #region Tree OF A Kind
                case 4://מכניס ל סקנד את המספר של השלשה ועוד 2 מספרים לפי הגודל
                    for (int i = 0; i < (hand.Count - 2); i++)
                        for (int j = i + 1; j < (hand.Count - 1); j++)
                            for (int q = j + 1; q < (hand.Count); q++)
                                    if ((hand[i].num == hand[j].num) && (hand[j].num == hand[q].num))
                                    {
                                        second.Add(hand[i].num);
                                        hand.Remove(hand[q]);
                                        hand.Remove(hand[j]);
                                        hand.Remove(hand[i]);
                                    }
                    max=-1;
                    max2=-2;
                    
                    for (int i = 0; i < hand.Count; i++)
                        if (((hand[i].num > max) || (hand[i].num == 0)) && (max != 0))
                        {
                            max2 = max;
                            max = hand[i].num;
                        }

                        else if ((hand[i].num > max2) && (max2 != 0))
                            max2 = hand[i].num;
                            second.Add(max);
                            second.Add(max2);
                                    break;
#endregion
                #region Straight
                case 5://מכניס ל סקנד את הסדרה לפי הגודל
                                    bool[] mz = new bool[13];
                                    for (int i = 0; i < hand.Count; i++)
                                        mz[hand[i].num] = true;
                                     int mone = 0;

                    if (mz[0] == true) mone++;   
                    for(int i=12;i>=0;i--)
                        if(mz[i] ==true)
                        {
                            mone++;
                            if (mone == 5)
                                for (int j = 0; j < 5; j++)
                                    if ((4 + i - j) == 13) second.Add(0);
                                    else  second.Add(4+i-j);
                        }
                        else
                            mone=0;

                                    break;

                #endregion
                #region Flush
                case 6://מכניס ל סקנד את החמש קלפים שבאותו הצבע לפי הגודל
                                    int maxkind = -1;
                                    int[] mm = new int[4];
                                    for (int i = 0; i < hand.Count; i++)
                                        mm[(int)hand[i].kind]++;
                                    for (int i = 0; i < mm.Length; i++)
                                        if (mm[i] >= 5)
                                            maxkind = i;
                                    for (int i = 0; i < hand.Count; i++)
                                        if ((int)hand[i].kind != maxkind)
                                        {
                                            hand.Remove(hand[i]);
                                            i -= 1;
                                        }
                                    for (int j = 0; j < 5; j++)
                                    {
                                        max = -1;
                                        for (int i = 0; i < hand.Count; i++)
                                        {
                                            if ((hand[i].num > max) && (max!= 0))
                                                max = hand[i].num;
                                            if (hand[i].num == 0) max = 0;

                                        }
                                        for (int i = 0; i < hand.Count; i++)
                                            if (hand[i].num == max)
                                            {
                                                hand.Remove(hand[i]);
                                                i -= 1;
                                            }
                                        second.Add(max);
                                    }

                                                                        break;
                #endregion
                #region Full House
                case 7://מכניס לסקנד את מספר השלישייה ומספר הזוג
                                    int hightree = -1;
                    for (int i = 0; i < (hand.Count - 2); i++)
                                        for (int j = i + 1; j < (hand.Count - 1); j++)
                                            for (int q = j + 1; q < (hand.Count); q++)
                                                    if ((hand[i].num == hand[j].num) && (hand[j].num == hand[q].num))
                                                        if (((hand[i].num > hightree)&&(hightree !=0))||(hand[i].num==0))
                                                            hightree = hand[i].num;
                    for (int i = 0; i < hand.Count; i++)
                        if (hand[i].num == hightree)
                        {
                            hand.Remove(hand[i]);
                            i -= 1;
                        }
                    second.Add(hightree);
                    max = -1;            
                    for (int i = 0; i < hand.Count - 1; i++)
                        for (int j = i + 1; j < hand.Count ; j++)
                            if (hand[i].num == hand[j].num)
                            {
                                if (hand[i].num == 0)
                                    max = hand[i].num;
                                if ((hand[i].num > max) && (max != 0))
                                    max = hand[i].num;
                            }
                                second.Add(max);
                                
                                        break;
                #endregion
                #region Four Of A Kind
                case 8://מכניס ל סקנד את מספר הרבעייה וקלף גובה
                                    for (int i = 0; i < (hand.Count - 3); i++)
                                        for (int j = i + 1; j < (hand.Count - 2); j++)
                                            for (int q = j + 1; q < (hand.Count - 1); q++)
                                                for (int p = q + 1; p < (hand.Count); p++)
                                                    if ((hand[i].num == hand[j].num) && (hand[j].num == hand[q].num) && (hand[q].num == hand[p].num))
                                                    {
                                                        second.Add(hand[i].num);
                                                        hand.Remove(hand[p]);
                                                        hand.Remove(hand[q]);
                                                        hand.Remove(hand[j]);
                                                        hand.Remove(hand[i]);
                                                    }
                                    max = -1;
                                    for (int i = 0; i < hand.Count; i++)
                                    {
                                        if (hand[i].num == 0)
                                        max= hand[i].num;
                                            if ((hand[i].num > max)&&(max!=0))
                                                max = hand[i].num;
                                            
                                        }
                                    second.Add(max);


                                    break;
                #endregion
                #region Straight Flush
                case 9:
                                    {//מכניס לקנד את כל הקלפים לפי הסדר
                                        maxkind = -1;
                                        int[] mm2 = new int[4];
                                        for (int i = 0; i < hand.Count; i++)
                                            mm2[(int)hand[i].kind]++;
                                        for (int i = 0; i < mm2.Length; i++)
                                            if (mm2[i] >= 5)
                                                maxkind = i;
                                        for (int i = 0; i < hand.Count; i++)
                                            if ((int)hand[i].kind != maxkind)
                                            {
                                                hand.Remove(hand[i]);
                                                i -= 1;
                                            }
                                        bool[] mz2 = new bool[13];
                                        for (int i = 0; i < hand.Count; i++)
                                            mz2[hand[i].num] = true;
                                         mone = 0;

                                        if (mz2[0] == true) mone++;
                                        for (int i = 12; i >= 0; i--)
                                            if (mz2[i] == true)
                                            {
                                                mone++;
                                                if (mone == 5)
                                                    for (int j = 0; j < 5; j++)
                                                        if ((4 + i - j) == 13) second.Add(0);
                                                        else second.Add(4 + i - j);
                                            }
                                            else
                                                mone = 0;

                                        
                                        break;
                                    }
                #endregion


            }
            return second;
        }
       
        public List<int> Whowin2(List<int> plr)
        {
            int temp = 0;
            for (int i = 0; i < plr.Count - 1; i++)
            {
                 for (int j = 0; j < plr.Count - 1; j++)
                        if (GetFirst(GetHand(Players[plr[j]].PlayerCards)) < GetFirst(GetHand(Players[plr[j+1]].PlayerCards)))
                        {
                            temp = plr[j];
                            plr[j] = plr[j+1];
                            plr[j+1] = temp;
                        }
                        else
                        {
                            if (GetFirst(GetHand(Players[plr[j]].PlayerCards)) == GetFirst(GetHand(Players[plr[j+1]].PlayerCards)) 
                                && Compare(plr[j], plr[j+1]) )
                            {
                                temp = plr[j];
                                plr[j] = plr[j+1];
                                plr[j+1] = temp;
                            }
                        }
               
                }
          // Parent.textBox1.Text = "" + plr[0] + ", " + plr[1] + ", " + plr[2] + ", " + plr[3] + "";
            
            List<int> winners = new List<int>();
           
              winners.Add(plr[0]);
            int z=0;
            while((plr.Count>z+1)&&(GetFirst(GetHand(Players[plr[z]].PlayerCards)) == GetFirst(GetHand(Players[plr[z+1]].PlayerCards)) 
                                && Equal(plr[z], plr[z+1]) )){
            

                winners.Add(plr[z + 1]);
                z++;
            }
            return winners;
            }//V

        private bool Compare(int p1, int p2)
            { 
                    List<int> second1 =GetSecond(GetHand(Players[p1].PlayerCards),GetFirst(GetHand(Players[p1].PlayerCards)));
                    List<int>  second2 =GetSecond(GetHand(Players[p2].PlayerCards),GetFirst(GetHand(Players[p2].PlayerCards)));
                    for(int j=0; j<second1.Count ; j++)
                    {
                        if ((second1[j] < second2[j])||(second1[j]!=0 && second2[j]==0))
                            return true;
                        if ((second1[j] > second2[j]) || (second1[j] == 0 && second2[j] != 0))
                            return false;
                    }
                    return false;               
            }
        private bool Equal(int p1, int p2)
        {
            List<int> second1 = GetSecond(GetHand(Players[p1].PlayerCards), GetFirst(GetHand(Players[p1].PlayerCards)));
            List<int> second2 = GetSecond(GetHand(Players[p2].PlayerCards), GetFirst(GetHand(Players[p2].PlayerCards)));
            for (int j = 0; j < second1.Count; j++)
            {
                if (second1[j] < second2[j])
                    return false;
                if (second1[j] > second2[j])
                    return false;
            }
            return true;
        }

            //bool swaped = false;
            //List<int> second1 =new List<int>();
            //List<int> second2 = new List<int>();
            //for (int i = 1; i < plr.Count; i++)
            //{
            //    if (GetFirst(GetHand(Players[plr[i - 1]].PlayerCards)) == GetFirst(GetHand(Players[plr[i]].PlayerCards)))
            //    {
            //        second1 =GetSecond(GetHand(Players[plr[i - 1]].PlayerCards),GetFirst(GetHand(Players[plr[i - 1]].PlayerCards)));
            //        second2 =GetSecond(GetHand(Players[plr[i]].PlayerCards),GetFirst(GetHand(Players[plr[i]].PlayerCards)));
            //        for (int j = 0; j < second1.Count; j++)
            //        {
            //            if (second1[j] == 0)
            //                second1[j] = 14;
            //            if (second2[j] == 0)
            //                second2[j] = 14;
            //        }
            //        for(int j=0;((j<second1.Count)&&(!swaped));j++)
            //        {
            //            if ((second1[j] < second2[j]))
            //            {
            //                swaped = true;
            //                temp = plr[i - 1];
            //                plr[i - 1] = plr[i];
            //                plr[i] = temp;
                            
            //            }
            //            }
                
            //}}
      
        public bool Is_Equal(List<int> lst1, List<int> lst2)
        {
           
           for(int i=0;i<lst1.Count;i++)
               if (lst1[i] != lst2[i])
                   return false;
           return true;

        }
        public void SplitMoney()
       {
          //split to pots
           int min = Smallest_Pot();
           while ((min != int.MaxValue) && (NumOfPots<=Pots.Length))
           {
               for (int i = 0; i < NumOfPlayers; i++)
               {
                   if (Players[i].Total_Money_In_Round!=0)
                   {
                       Pots[NumOfPots].Sum += min;
                       Players[i].Total_Money_In_Round -= min;
                       if(Players[i].In_Game)
                       Pots[NumOfPots].players.Add(i);
                   }
                   
               }
               NumOfPots++;
               min = Smallest_Pot();
           }

           List<int> lst;
            Parent.textBox1.Text = "";
            string mes= "";
            for(int i=0;i<NumOfPots;i++)
            {
                lst =Whowin2(Pots[i].players);
                for (int j = 0; j < lst.Count; j++)
                {
                    int win = lst[j];
                    if (Pots[i].Sum > 0)
                    {

                        Card s= (Card)Players[win].PlayerCards.Controls[0];
                        Card s1 = (Card)Players[win].PlayerCards.Controls[1];
                        s.flip_on();
                        s1.flip_on();
                        mes += win+"&Player " + win + " won " + (Pots[i].Sum / lst.Count + " with "+Player_Hand_Desc(win)+"#");
                        Players[win].Money += Pots[i].Sum / lst.Count;
                        Players[win].PlayerMoney.Text = "" + Players[win].Money + "";
                    }
                    
                }
                if (Parent is gameformNet)
                    Parent.Send_Winningstring_To_Clients(mes);
                string[] winners = mes.Split('#');
                foreach(string winner in winners)
                    if(winner!="")
                Parent.textBox1.Text += winner.Split('&')[1];
                Pots[i].Sum = 0;
            
            }
       }

        private string Player_Hand_Desc(int win)
        {
            string str = "";
            int first = GetFirst(GetHand(Players[win].PlayerCards));
            List<int> second = GetSecond(GetHand(Players[win].PlayerCards), first);
            switch (first)
            {
                case 1: str = "High Card of "+(second[0]+1)+"  "; break;
                case 2: str = "Pair of " + (second[0]+1) + "  "; break;
                case 3: str = "Two Pairs of "+ (second[0]+1) + " and "+(second[1]+1)+"  "; break;
                case 4: str = "Tree Of A Kind of " + (second[0]+1) + "  "; break;
                case 5: str = "Straight to "+(second[0]+1)+"  "; break;
                case 6: str = "Flush to " + (second[0]+1) + "  "; break;
                case 7: str = "Full House of " + (second[0]+1) + " and " + (second[1]+1) + "  "; break;
                case 8: str = "Four Of A Kind of " + (second[0]+1 )+ "  "; break;
                case 9: str = "Straight Flush to "+(second[0]+1)+"  "; break;
            }
            str = str.Replace("10", "Ten");
            str=str.Replace("11", "Jack");
            str=str.Replace("12", "Queen");
            str = str.Replace("13", "King");
            str = str.Replace("1", "Ace");
            return str;
        }

       
        public int Smallest_Pot()
        {
            int min = int.MaxValue;
            for (int i = 0; i < NumOfPlayers; i++)
                if ((min > Players[i].Total_Money_In_Round) && (Players[i].Total_Money_In_Round != 0) && (Players[i].In_Game))
                    min = Players[i].Total_Money_In_Round;

            return min;
        }

        public void Computer_Turn()
        {
            if (Parent.this_comp_player != (PlayerTurn % NumOfPlayers))
            {     
                EventArgs e = new EventArgs();
                object sender = new object();

                List<Card> hand = new List<Card>();
                hand = GetHand(Players[PlayerTurn % NumOfPlayers].PlayerCards);
                Random rnd = new Random();
                double HandEval = 0;
                if (stage == 0)
                    HandEval = GetValByChenFormula(hand);
                if (stage > 0 && stage < 3)
                {
                    HandEval = GetHandPotential(hand) + GetHandVal(hand);
                    if (HandEval > 100) HandEval = 100;
                }
                if (stage ==3)
                    HandEval = GetHandVal(hand);
                
                if (CallAmount > Players[PlayerTurn % NumOfPlayers].Money_In_Round)
                {//raised

                    double PotOdd = (double)decimal.Divide((CallAmount - Players[PlayerTurn % NumOfPlayers].Money_In_Round), Sum_money_in_round());
                    PotOdd *= 100;

                    if ((PotOdd*Players[PlayerTurn % NumOfPlayers].Astratgy) < HandEval)
                    {
                            Parent.CheakButtom_Click(sender, e);
                    }
                    else
                    {
                        Parent.FoldButton_Click(sender, e);
                    }
                }
                else
                {//not raised
                    if (Do_Precent((int)Math.Round(80+20*Players[PlayerTurn%NumOfPlayers].Astratgy)))
                    {//regular
                        if (Do_Precent((int)Math.Round(Players[PlayerTurn % NumOfPlayers].Astratgy * 5 + 0.8 * HandEval)))
                        {//raise

                            int RaiseAmount = (int)(Math.Round((HandEval * 0.7 + Players[PlayerTurn % NumOfPlayers].Astratgy * 30) * 0.01 * Players[PlayerTurn % NumOfPlayers].Money));
                            if (RaiseAmount > Parent.RaiseAmount.Maximum) RaiseAmount = (int)Parent.RaiseAmount.Maximum;
                            if (RaiseAmount < Parent.RaiseAmount.Minimum) RaiseAmount = (int)Parent.RaiseAmount.Minimum;
                            Parent.RaiseAmount.Value = RaiseAmount;
                            Parent.RaiseButton_Click(sender, e);
                        }
                        else
                        {//cheaked
                            Parent.CheakButtom_Click(sender, e);
                        }
                    }
                    else
                    {//blof OR Cheake
                        if (Do_Precent(50))
                        {
                            Parent.CheakButtom_Click(sender, e);
                        }
                        else
                        {
                            Parent.RaiseAmount.Value = (int)Parent.RaiseAmount.Maximum;
                            Parent.RaiseButton_Click(sender, e);
                        }

                    }
                }
            }
        }
        private double GetHandVal(List<Card> hand)
        {
            int first = GetFirst(hand);
            List<int> second = GetSecond(hand, first);
            int second1 = EvaluateCard(second[0]);
            int second2 = EvaluateCard(second[1]);
            //Parent.textBox1.Text = ((first * 100 + second1 * 10 + second2) *0.1) + " ";
            return ((double)decimal.Divide((first * 100 + second1 * 10 + second2) , 10));


        }
        private int EvaluateCard(int p)
        {
            switch (p)
            {
                case 0: return 9;
                case 12: return 8;
                case 11: return 7;
                case 10: return 6;
                case 9: return 5;
                case 8: return 4;
                case 7: return 3;
                case 6: return 2;
                case 5: return 1;
                default: return 0;
            }

        }
        private int Sum_money_in_round()
        {
            int sum = 0;
            for (int i = 0; i < NumOfPlayers; i++)
            {
                sum += Players[i].Total_Money_In_Round;
            }
            if (stage == 0)
                return sum * 2;
            return sum;
        }
        private double GetValByChenFormula(List<Card> hand)
        {
            double point = 0;
            //rule 1
            if ((hand[0].num < hand[1].num && hand[0].num != 0) || hand[1].num == 0)
            {//swap
                Card c = hand[1];
                hand[1] = hand[0];
                hand[0] = c;
            }
            if (hand[0].num == 0)
                point += 10;
            else if (hand[0].num == 12)
                point += 8;
            else if (hand[0].num == 11)
                point += 7;
            else if (hand[0].num == 10)
                point += 6;
            else
                point += (double)(hand[0].num + 1) / 2;
            // rule 2
            if (hand[0].num == hand[1].num)
            {
                point *= 2;
                if (point < 5) point = 5;
            }
            //rule 3
            if ((int)hand[0].kind == (int)hand[1].kind)
                point += 2;
            //rule 4
            int gap = 0;
            if (hand[0].num == hand[1].num)
                gap = 0;
            else if (hand[0].num == 0)
                gap = minvalue(12 - hand[1].num, hand[1].num - 1);
            else
                gap = (hand[0].num - hand[1].num - 1);
            if (gap == 1) point -= 1;
            if (gap == 2) point -= 2;
            if (gap == 3) point -= 4;
            if (gap >= 4) point -= 5;

            //rule 5
            if (gap < 2 && hand[0].num < 11 && hand[0].num != 0) point += 1;

            //rule 6
            if (point % 1 != 0) point += 0.5;
            //Parent.textBox1.Text = ((point+1)*5) + " ";
            if (((point + 1) * 5) > 100)
                return (double)100;
            return (double)((point+1)*5);
        }
        private int minvalue(int p1, int p2)
        {
            if (p1 < p2)
                return p1;
            else return p2;
        }
        public double GetHandPotential(List<Card> hand)
        {
            
            double odd = 100;
            int first = GetFirst(hand);
            if (first < 9)
            {
                odd = 0;
                odd += (GetStraightFlushOuts(hand) * 100 / (52 - hand.Count));
                if (first < 8)
                {
                    odd += (GetFourOfAKindOuts(hand) * 100 / (52 - hand.Count));
                    if (first < 7)
                    {
                        odd += (GetFullHouseOuts(hand) * 100 / (52 - hand.Count));
                        if (first < 6)
                        {
                            odd += (GetFlushOuts(hand) * 100 / (52 - hand.Count));
                            if (first < 5)
                            {
                                odd += (GetStraightOuts(hand) * 100 / (52 - hand.Count));
                                if (first < 4)
                                {
                                    odd += (GetTreeOfAKindOuts(hand) * 100 / (52 - hand.Count) * 0.8);
                                    if (first < 3)
                                    {
                                        odd += (GetTwoPairsOuts(hand) * 100 / (52 - hand.Count) * 0.5);
                                        if (first < 2)
                                        {
                                            odd += (GetPairOuts(hand) * 100 / (52 - hand.Count) * 0.3);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            //Parent.textBox1.Text += odd + " ";
            return odd;
        }
        public bool Do_Precent(int num)
        {
            bool[] arr = new bool[100];
            for (int i = 0; i < num; i++)
                arr[i] = true;
            Random rnd = new Random();
            return (arr[rnd.Next(0, 99)]);

        }

        public int GetPairOuts(List<Card> hand)
        {
            int odd = 0;
            int []mm = new int[13];

            for (int i = 0; i < hand.Count; i++)
                mm[hand[i].num]++;

            for (int i = 0; i < mm.Length; i++)
                if (mm[i] == 1) odd+=3;

            return odd;
        }
        public int GetTwoPairsOuts(List<Card> hand)
        {
            int odd = 0;
            int[] mm = new int[13];

            for (int i = 0; i < hand.Count; i++)
                mm[hand[i].num]++;

            for (int i = 0; i < mm.Length - 1; i++)
                for (int j = i + 1; j < mm.Length; j++)
                {
                    if (mm[i] == 2 && mm[j] == 1) odd += 3;
                    if (mm[i] == 1 && mm[j] == 2) odd += 3;
                }

            return odd;
        }
        public int GetTreeOfAKindOuts(List<Card> hand)
        {
            int odd = 0;
            int[] mm = new int[13];

            for (int i = 0; i < hand.Count; i++)
                mm[hand[i].num]++;

            for (int i = 0; i < mm.Length; i++)
                if (mm[i] == 2) odd += 2;

            return odd;
        }
        public int GetStraightOuts(List<Card> hand)
        {
            int odd = 0;
            bool[] mz = new bool[13];
            for (int i = 0; i < hand.Count; i++)
                    mz[hand[i].num] = true;

            for (int i = 0; i < mz.Length - 4; i++)
            {
                if (mz[i] && mz[i + 1] && mz[i + 2] && mz[i + 3]) odd += 4;
                if (mz[i] && mz[i + 1] && mz[i + 2] && mz[i + 4]) odd += 4;
                if (mz[i] && mz[i + 1] && mz[i + 3] && mz[i + 4]) odd += 4;
                if (mz[i] && mz[i + 2] && mz[i + 3] && mz[i + 4]) odd += 4;
                if (mz[i + 1] && mz[i + 2] && mz[i + 3] && mz[i + 4]) odd += 4;
            }
               
            return odd;
        }
        public int GetFlushOuts(List<Card> hand)
        {
             int odd = 0;
            int[] mm = new int[4];
            for (int i = 0; i < hand.Count; i++)
                mm[(int)hand[i].kind]++;
            for (int i = 0; i < 4; i++)
                if (mm[i] == 4) odd += 9;
            return odd;
            
        }
        public int GetFullHouseOuts(List<Card> hand)
        {
            int odd = 0;
            int[] mm = new int[13];

            for (int i = 0; i < hand.Count; i++)
                mm[hand[i].num]++;

            for (int i = 0; i < mm.Length-1; i++)
                for (int j = i + 1; j < mm.Length; j++)
                {
                    if (mm[i] == 3 && mm[j] == 1) odd += 3;
                    if (mm[i] == 2 && mm[j] == 2) odd += 4;
                }

            return odd;
        }
        public int GetFourOfAKindOuts(List<Card> hand)
        {
            int odd = 0;
            int[] mm = new int[13];

            for (int i = 0; i < hand.Count; i++)
                mm[hand[i].num]++;

            for (int i = 0; i < mm.Length; i++)
                if (mm[i] == 3) odd += 1;

            return odd;
        }
        public int GetStraightFlushOuts(List<Card> hand)
        {
            int odd = 0;
            bool[,] mz = new bool[13,4];

            for (int i = 0; i < hand.Count; i++)
                mz[hand[i].num, (int)hand[i].kind] = true; ;

            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 9; i++)
                {
                    if (mz[i,j] && mz[i + 1,j] && mz[i + 2,j] && mz[i + 3,j]) odd += 1;
                    if (mz[i,j] && mz[i + 1,j] && mz[i + 2,j] && mz[i + 4,j]) odd += 1;
                    if (mz[i,j] && mz[i + 1,j] && mz[i + 3,j] && mz[i + 4,j]) odd += 1;
                    if (mz[i,j] && mz[i + 2,j] && mz[i + 3,j] && mz[i + 4,j]) odd += 1;
                    if (mz[i + 1,j] && mz[i + 2,j] && mz[i + 3,j] && mz[i + 4,j]) odd += 1;
                }
                
                
               

            return odd;
        }
    }
}
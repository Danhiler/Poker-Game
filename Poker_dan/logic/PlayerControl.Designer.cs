namespace Poker_dan
{
    partial class PlayerControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PlayerRaise = new System.Windows.Forms.Label();
            this.PlayerMoney = new System.Windows.Forms.Label();
            this.PlayerName = new System.Windows.Forms.Label();
            this.PlayerCards = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PlayerRaise
            // 
            this.PlayerRaise.AutoSize = true;
            this.PlayerRaise.Location = new System.Drawing.Point(73, 126);
            this.PlayerRaise.Name = "PlayerRaise";
            this.PlayerRaise.Size = new System.Drawing.Size(41, 13);
            this.PlayerRaise.TabIndex = 36;
            this.PlayerRaise.Text = "label13";
            // 
            // PlayerMoney
            // 
            this.PlayerMoney.AutoSize = true;
            this.PlayerMoney.Location = new System.Drawing.Point(91, 5);
            this.PlayerMoney.Name = "PlayerMoney";
            this.PlayerMoney.Size = new System.Drawing.Size(41, 13);
            this.PlayerMoney.TabIndex = 33;
            this.PlayerMoney.Text = "label14";
            // 
            // PlayerName
            // 
            this.PlayerName.AutoSize = true;
            this.PlayerName.Location = new System.Drawing.Point(17, 5);
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.Size = new System.Drawing.Size(41, 13);
            this.PlayerName.TabIndex = 35;
            this.PlayerName.Text = "label15";
            // 
            // PlayerCards
            // 
            this.PlayerCards.AutoSize = true;
            this.PlayerCards.Location = new System.Drawing.Point(7, 21);
            this.PlayerCards.Name = "PlayerCards";
            this.PlayerCards.Size = new System.Drawing.Size(150, 100);
            this.PlayerCards.TabIndex = 34;
            // 
            // PlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PlayerRaise);
            this.Controls.Add(this.PlayerMoney);
            this.Controls.Add(this.PlayerName);
            this.Controls.Add(this.PlayerCards);
            this.Name = "PlayerControl";
            this.Size = new System.Drawing.Size(168, 143);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label PlayerRaise;
        public System.Windows.Forms.Label PlayerMoney;
        public System.Windows.Forms.Label PlayerName;
        public System.Windows.Forms.Panel PlayerCards;
    }
}

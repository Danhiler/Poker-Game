namespace Poker_dan
{
    partial class gameform
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backToMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.TableCards = new System.Windows.Forms.Panel();
            this.CheakButton = new System.Windows.Forms.Button();
            this.FoldButton = new System.Windows.Forms.Button();
            this.RaiseAmount = new System.Windows.Forms.NumericUpDown();
            this.TableMoney = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.RaiseButton = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.client_loading = new System.Windows.Forms.Label();
            this.Num_Of_Players = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RaiseAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1122, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.backToMenuToolStripMenuItem,
            this.optionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // backToMenuToolStripMenuItem
            // 
            this.backToMenuToolStripMenuItem.Name = "backToMenuToolStripMenuItem";
            this.backToMenuToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.backToMenuToolStripMenuItem.Text = "Back to menu";
            this.backToMenuToolStripMenuItem.Click += new System.EventHandler(this.backToMenuToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullScreenToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fullScreenToolStripMenuItem.Text = "Full Screen";
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // TableCards
            // 
            this.TableCards.AutoSize = true;
            this.TableCards.BackColor = System.Drawing.Color.Aquamarine;
            this.TableCards.Location = new System.Drawing.Point(294, 226);
            this.TableCards.Name = "TableCards";
            this.TableCards.Size = new System.Drawing.Size(372, 110);
            this.TableCards.TabIndex = 4;
            // 
            // CheakButton
            // 
            this.CheakButton.Location = new System.Drawing.Point(400, 375);
            this.CheakButton.Name = "CheakButton";
            this.CheakButton.Size = new System.Drawing.Size(94, 42);
            this.CheakButton.TabIndex = 5;
            this.CheakButton.Text = "Check";
            this.CheakButton.UseVisualStyleBackColor = true;
            this.CheakButton.Visible = false;
            this.CheakButton.Click += new System.EventHandler(this.CheakButtom_Click);
            // 
            // FoldButton
            // 
            this.FoldButton.Location = new System.Drawing.Point(517, 375);
            this.FoldButton.Name = "FoldButton";
            this.FoldButton.Size = new System.Drawing.Size(94, 42);
            this.FoldButton.TabIndex = 6;
            this.FoldButton.Text = "Fold";
            this.FoldButton.UseVisualStyleBackColor = true;
            this.FoldButton.Visible = false;
            this.FoldButton.Click += new System.EventHandler(this.FoldButton_Click);
            // 
            // RaiseAmount
            // 
            this.RaiseAmount.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RaiseAmount.Location = new System.Drawing.Point(520, 436);
            this.RaiseAmount.Maximum = new decimal(new int[] {
            2000000,
            0,
            0,
            0});
            this.RaiseAmount.Name = "RaiseAmount";
            this.RaiseAmount.Size = new System.Drawing.Size(91, 20);
            this.RaiseAmount.TabIndex = 8;
            this.RaiseAmount.Visible = false;
            // 
            // TableMoney
            // 
            this.TableMoney.AutoSize = true;
            this.TableMoney.Location = new System.Drawing.Point(397, 359);
            this.TableMoney.Name = "TableMoney";
            this.TableMoney.Size = new System.Drawing.Size(19, 13);
            this.TableMoney.TabIndex = 14;
            this.TableMoney.Text = "0$";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 662);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1098, 20);
            this.textBox1.TabIndex = 15;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 601);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(915, 20);
            this.textBox2.TabIndex = 16;
            this.textBox2.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(12, 636);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(915, 20);
            this.textBox3.TabIndex = 17;
            this.textBox3.Visible = false;
            // 
            // RaiseButton
            // 
            this.RaiseButton.Location = new System.Drawing.Point(400, 423);
            this.RaiseButton.Name = "RaiseButton";
            this.RaiseButton.Size = new System.Drawing.Size(94, 42);
            this.RaiseButton.TabIndex = 7;
            this.RaiseButton.Text = "Raise";
            this.RaiseButton.UseVisualStyleBackColor = true;
            this.RaiseButton.Visible = false;
            this.RaiseButton.Click += new System.EventHandler(this.RaiseButton_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(686, 529);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(241, 20);
            this.textBox4.TabIndex = 18;
            this.textBox4.Visible = false;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(53, 60);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 19;
            this.StartButton.Text = "StartGame";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // client_loading
            // 
            this.client_loading.AutoSize = true;
            this.client_loading.Location = new System.Drawing.Point(472, 180);
            this.client_loading.Name = "client_loading";
            this.client_loading.Size = new System.Drawing.Size(73, 13);
            this.client_loading.TabIndex = 20;
            this.client_loading.Text = "Please Wait...";
            // 
            // Num_Of_Players
            // 
            this.Num_Of_Players.AutoSize = true;
            this.Num_Of_Players.Location = new System.Drawing.Point(68, 101);
            this.Num_Of_Players.Name = "Num_Of_Players";
            this.Num_Of_Players.Size = new System.Drawing.Size(110, 13);
            this.Num_Of_Players.TabIndex = 21;
            this.Num_Of_Players.Text = "Number Of Players:  1";
            // 
            // gameform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(1122, 698);
            this.Controls.Add(this.Num_Of_Players);
            this.Controls.Add(this.client_loading);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TableMoney);
            this.Controls.Add(this.RaiseAmount);
            this.Controls.Add(this.RaiseButton);
            this.Controls.Add(this.FoldButton);
            this.Controls.Add(this.CheakButton);
            this.Controls.Add(this.TableCards);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "gameform";
            this.Text = "gameform";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RaiseAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backToMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        public System.Windows.Forms.Panel TableCards;
        public System.Windows.Forms.Button CheakButton;
        private System.Windows.Forms.Button FoldButton;
        public System.Windows.Forms.Label TableMoney;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.Button RaiseButton;
        public System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.NumericUpDown RaiseAmount;
        private System.Windows.Forms.Button StartButton;
        public System.Windows.Forms.Label client_loading;
        public System.Windows.Forms.Label Num_Of_Players;
    }
}
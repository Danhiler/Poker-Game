namespace Poker_dan
{
    partial class Options
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Small_Blind = new System.Windows.Forms.NumericUpDown();
            this.Num_Of_Players = new System.Windows.Forms.NumericUpDown();
            this.Starting_Money = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Small_Blind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Of_Players)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Starting_Money)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number Of Players: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Small Blind: ";
            // 
            // Small_Blind
            // 
            this.Small_Blind.Location = new System.Drawing.Point(95, 107);
            this.Small_Blind.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Small_Blind.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Small_Blind.Name = "Small_Blind";
            this.Small_Blind.Size = new System.Drawing.Size(50, 20);
            this.Small_Blind.TabIndex = 9;
            this.Small_Blind.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Num_Of_Players
            // 
            this.Num_Of_Players.Location = new System.Drawing.Point(132, 46);
            this.Num_Of_Players.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Num_Of_Players.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.Num_Of_Players.Name = "Num_Of_Players";
            this.Num_Of_Players.Size = new System.Drawing.Size(35, 20);
            this.Num_Of_Players.TabIndex = 10;
            this.Num_Of_Players.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // Starting_Money
            // 
            this.Starting_Money.Location = new System.Drawing.Point(154, 166);
            this.Starting_Money.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Starting_Money.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.Starting_Money.Name = "Starting_Money";
            this.Starting_Money.Size = new System.Drawing.Size(76, 20);
            this.Starting_Money.TabIndex = 12;
            this.Starting_Money.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Starting Money Amount: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(90, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "Options";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Starting_Money);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Num_Of_Players);
            this.Controls.Add(this.Small_Blind);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Options";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.Small_Blind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Of_Players)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Starting_Money)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown Small_Blind;
        public System.Windows.Forms.NumericUpDown Num_Of_Players;
        public System.Windows.Forms.NumericUpDown Starting_Money;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
    }
}
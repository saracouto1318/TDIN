namespace BankClient
{
    partial class AddFunds
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
            this.add = new System.Windows.Forms.Button();
            this.fundsAdded = new System.Windows.Forms.TextBox();
            this.Funds = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // add
            // 
            this.add.BackColor = System.Drawing.Color.LightCoral;
            this.add.FlatAppearance.BorderSize = 0;
            this.add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.add.Location = new System.Drawing.Point(457, 212);
            this.add.Margin = new System.Windows.Forms.Padding(2);
            this.add.Name = "add";
            this.add.Size = new System.Drawing.Size(61, 26);
            this.add.TabIndex = 20;
            this.add.Text = "Add";
            this.add.UseVisualStyleBackColor = false;
            this.add.Visible = false;
            this.add.Click += new System.EventHandler(this.add_Click);
            // 
            // fundsAdded
            // 
            this.fundsAdded.Location = new System.Drawing.Point(343, 216);
            this.fundsAdded.Name = "fundsAdded";
            this.fundsAdded.Size = new System.Drawing.Size(100, 20);
            this.fundsAdded.TabIndex = 19;
            this.fundsAdded.Visible = false;
            // 
            // Funds
            // 
            this.Funds.AutoSize = true;
            this.Funds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Funds.Location = new System.Drawing.Point(283, 215);
            this.Funds.Name = "Funds";
            this.Funds.Size = new System.Drawing.Size(54, 18);
            this.Funds.TabIndex = 18;
            this.Funds.Text = "Funds";
            this.Funds.Visible = false;
            // 
            // userName
            // 
            this.userName.Dock = System.Windows.Forms.DockStyle.Top;
            this.userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.Location = new System.Drawing.Point(0, 0);
            this.userName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(800, 79);
            this.userName.TabIndex = 21;
            this.userName.Text = "Nome de utilizador";
            this.userName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(272, 82);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(260, 50);
            this.button4.TabIndex = 23;
            this.button4.Text = "Orders";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(540, 82);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(260, 50);
            this.button5.TabIndex = 24;
            this.button5.Text = "Statistics";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(1, 82);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(260, 50);
            this.button6.TabIndex = 22;
            this.button6.Text = "Main Page";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // AddFunds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.add);
            this.Controls.Add(this.fundsAdded);
            this.Controls.Add(this.Funds);
            this.Name = "AddFunds";
            this.Text = "AddFunds";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button add;
        private System.Windows.Forms.TextBox fundsAdded;
        private System.Windows.Forms.Label Funds;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}
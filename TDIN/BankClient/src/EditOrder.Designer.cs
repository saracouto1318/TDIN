namespace BankClient
{
    partial class EditOrder
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
            this.userName = new System.Windows.Forms.Label();
            this.mainPageButton = new System.Windows.Forms.Button();
            this.statisticsButton = new System.Windows.Forms.Button();
            this.ordersButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.quote = new System.Windows.Forms.TextBox();
            this.diginotes = new System.Windows.Forms.Label();
            this.editOrderButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.logout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userName
            // 
            this.userName.Dock = System.Windows.Forms.DockStyle.Top;
            this.userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.Location = new System.Drawing.Point(0, 0);
            this.userName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(800, 79);
            this.userName.TabIndex = 26;
            this.userName.Text = "Nome de utilizador";
            this.userName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPageButton
            // 
            this.mainPageButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.mainPageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPageButton.Location = new System.Drawing.Point(1, 82);
            this.mainPageButton.Name = "mainPageButton";
            this.mainPageButton.Size = new System.Drawing.Size(260, 50);
            this.mainPageButton.TabIndex = 23;
            this.mainPageButton.Text = "Main Page";
            this.mainPageButton.UseVisualStyleBackColor = false;
            this.mainPageButton.Click += new System.EventHandler(this.MainPage_Click);
            // 
            // statisticsButton
            // 
            this.statisticsButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.statisticsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statisticsButton.Location = new System.Drawing.Point(540, 82);
            this.statisticsButton.Name = "statisticsButton";
            this.statisticsButton.Size = new System.Drawing.Size(260, 50);
            this.statisticsButton.TabIndex = 25;
            this.statisticsButton.Text = "Statistics";
            this.statisticsButton.UseVisualStyleBackColor = false;
            this.statisticsButton.Click += new System.EventHandler(this.Statistics_Click);
            // 
            // ordersButton
            // 
            this.ordersButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ordersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ordersButton.Location = new System.Drawing.Point(272, 82);
            this.ordersButton.Name = "ordersButton";
            this.ordersButton.Size = new System.Drawing.Size(260, 50);
            this.ordersButton.TabIndex = 24;
            this.ordersButton.Text = "Orders";
            this.ordersButton.UseVisualStyleBackColor = false;
            this.ordersButton.Click += new System.EventHandler(this.Orders_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(269, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Quotation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // quote
            // 
            this.quote.Location = new System.Drawing.Point(367, 238);
            this.quote.Name = "quote";
            this.quote.Size = new System.Drawing.Size(100, 20);
            this.quote.TabIndex = 27;
            this.quote.TextChanged += new System.EventHandler(this.Quote_TextChanged);
            // 
            // diginotes
            // 
            this.diginotes.AutoSize = true;
            this.diginotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diginotes.Location = new System.Drawing.Point(406, 197);
            this.diginotes.Name = "diginotes";
            this.diginotes.Size = new System.Drawing.Size(14, 13);
            this.diginotes.TabIndex = 31;
            this.diginotes.Text = "0";
            // 
            // editOrderButton
            // 
            this.editOrderButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.editOrderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editOrderButton.Location = new System.Drawing.Point(378, 335);
            this.editOrderButton.Name = "editOrderButton";
            this.editOrderButton.Size = new System.Drawing.Size(89, 38);
            this.editOrderButton.TabIndex = 30;
            this.editOrderButton.Text = "Edit Order";
            this.editOrderButton.UseVisualStyleBackColor = false;
            this.editOrderButton.Click += new System.EventHandler(this.EditOrder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(194, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 16);
            this.label2.TabIndex = 29;
            this.label2.Text = "Number of Diginotes";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(299, 283);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Price";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = true;
            // 
            // price
            // 
            this.price.AutoSize = true;
            this.price.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.price.ForeColor = System.Drawing.Color.Firebrick;
            this.price.Location = new System.Drawing.Point(402, 283);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(25, 13);
            this.price.TabIndex = 32;
            this.price.Text = "0 $";
            // 
            // logout
            // 
            this.logout.BackColor = System.Drawing.Color.SandyBrown;
            this.logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.Location = new System.Drawing.Point(689, 30);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(89, 31);
            this.logout.TabIndex = 34;
            this.logout.Text = "Logout";
            this.logout.UseVisualStyleBackColor = false;
            this.logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // EditOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.logout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.price);
            this.Controls.Add(this.diginotes);
            this.Controls.Add(this.editOrderButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.quote);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.mainPageButton);
            this.Controls.Add(this.statisticsButton);
            this.Controls.Add(this.ordersButton);
            this.Name = "EditOrder";
            this.Text = "EditOrder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Button mainPageButton;
        private System.Windows.Forms.Button statisticsButton;
        private System.Windows.Forms.Button ordersButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox quote;
        private System.Windows.Forms.Label diginotes;
        private System.Windows.Forms.Button editOrderButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label price;
        private System.Windows.Forms.Button logout;
    }
}
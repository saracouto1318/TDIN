namespace BankClient
{
    partial class CreateOrder
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
            this.createButton = new System.Windows.Forms.Button();
            this.numDiginotes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mainPageButton = new System.Windows.Forms.Button();
            this.statisticsButton = new System.Windows.Forms.Button();
            this.ordersButton = new System.Windows.Forms.Button();
            this.userName = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.quote = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.createButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createButton.Location = new System.Drawing.Point(366, 356);
            this.createButton.Name = "button3";
            this.createButton.Size = new System.Drawing.Size(89, 38);
            this.createButton.TabIndex = 14;
            this.createButton.Text = "Create Order";
            this.createButton.UseVisualStyleBackColor = false;
            this.createButton.Click += new System.EventHandler(this.Create_Click);
            // 
            // numDiginotes
            // 
            this.numDiginotes.Location = new System.Drawing.Point(355, 201);
            this.numDiginotes.Name = "numDiginotes";
            this.numDiginotes.Size = new System.Drawing.Size(100, 20);
            this.numDiginotes.TabIndex = 11;
            this.numDiginotes.Visible = true;
            this.numDiginotes.TextChanged += new System.EventHandler(this.NumDiginotes_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(188, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Number of Diginotes";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = true;
            // 
            // button2
            // 
            this.mainPageButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.mainPageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainPageButton.Location = new System.Drawing.Point(6, 82);
            this.mainPageButton.Name = "button2";
            this.mainPageButton.Size = new System.Drawing.Size(260, 50);
            this.mainPageButton.TabIndex = 19;
            this.mainPageButton.Text = "Main Page";
            this.mainPageButton.UseVisualStyleBackColor = false;
            this.mainPageButton.Click += new System.EventHandler(this.MainPage_Click);
            // 
            // button4
            // 
            this.statisticsButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.statisticsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statisticsButton.Location = new System.Drawing.Point(545, 82);
            this.statisticsButton.Name = "button4";
            this.statisticsButton.Size = new System.Drawing.Size(260, 50);
            this.statisticsButton.TabIndex = 21;
            this.statisticsButton.Text = "Statistics";
            this.statisticsButton.UseVisualStyleBackColor = false;
            this.statisticsButton.Click += new System.EventHandler(this.Statistics_Click);
            // 
            // button5
            // 
            this.ordersButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ordersButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ordersButton.Location = new System.Drawing.Point(277, 82);
            this.ordersButton.Name = "button5";
            this.ordersButton.Size = new System.Drawing.Size(260, 50);
            this.ordersButton.TabIndex = 20;
            this.ordersButton.Text = "Orders";
            this.ordersButton.UseVisualStyleBackColor = false;
            this.ordersButton.Click += new System.EventHandler(this.Orderes_Click);
            // 
            // userName
            // 
            this.userName.Dock = System.Windows.Forms.DockStyle.Top;
            this.userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.Location = new System.Drawing.Point(0, 0);
            this.userName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(811, 79);
            this.userName.TabIndex = 22;
            this.userName.Text = "Nome de utilizador";
            this.userName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // price
            // 
            this.price.AutoSize = true;
            this.price.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.price.ForeColor = System.Drawing.Color.Firebrick;
            this.price.Location = new System.Drawing.Point(396, 294);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(25, 13);
            this.price.TabIndex = 23;
            this.price.Text = "0 $";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(210, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Current Quotation";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(293, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "Price";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // quote
            // 
            this.quote.AutoSize = true;
            this.quote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quote.Location = new System.Drawing.Point(396, 250);
            this.quote.Name = "quote";
            this.quote.Size = new System.Drawing.Size(25, 13);
            this.quote.TabIndex = 25;
            this.quote.Text = "0 $";
            // 
            // CreateOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 450);
            this.Controls.Add(this.quote);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.price);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.mainPageButton);
            this.Controls.Add(this.statisticsButton);
            this.Controls.Add(this.ordersButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numDiginotes);
            this.Name = "CreateOrder";
            this.Text = "Create Order";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox numDiginotes;
        private System.Windows.Forms.Button mainPageButton;
        private System.Windows.Forms.Button statisticsButton;
        private System.Windows.Forms.Button ordersButton;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label price;
        private System.Windows.Forms.Label quote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}
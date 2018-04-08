using System.Collections;

namespace BankClient
{
    partial class OrdersList
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
            this.all = new System.Windows.Forms.LinkLabel();
            this.bought = new System.Windows.Forms.LinkLabel();
            this.sold = new System.Windows.Forms.LinkLabel();
            this.IDlabel = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.transactionID = new System.Windows.Forms.Label();
            this.value = new System.Windows.Forms.Label();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.open = new System.Windows.Forms.LinkLabel();
            this.close = new System.Windows.Forms.LinkLabel();
            this.labelDiginote = new System.Windows.Forms.Label();
            this.nDiginotes = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.labelQuotation = new System.Windows.Forms.Label();
            this.quotation = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // all
            // 
            this.all.ActiveLinkColor = System.Drawing.Color.OrangeRed;
            this.all.AutoSize = true;
            this.all.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.all.LinkColor = System.Drawing.Color.IndianRed;
            this.all.Location = new System.Drawing.Point(32, 174);
            this.all.Name = "all";
            this.all.Size = new System.Drawing.Size(34, 24);
            this.all.TabIndex = 3;
            this.all.TabStop = true;
            this.all.Text = "All";
            this.all.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.all_LinkClicked);
            // 
            // bought
            // 
            this.bought.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.bought.AutoSize = true;
            this.bought.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bought.ForeColor = System.Drawing.Color.IndianRed;
            this.bought.LinkColor = System.Drawing.Color.IndianRed;
            this.bought.Location = new System.Drawing.Point(121, 174);
            this.bought.Name = "bought";
            this.bought.Size = new System.Drawing.Size(76, 24);
            this.bought.TabIndex = 4;
            this.bought.TabStop = true;
            this.bought.Text = "Bought";
            this.bought.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.bought_LinkClicked);
            // 
            // sold
            // 
            this.sold.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.sold.AutoSize = true;
            this.sold.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sold.ForeColor = System.Drawing.Color.IndianRed;
            this.sold.LinkColor = System.Drawing.Color.IndianRed;
            this.sold.Location = new System.Drawing.Point(257, 174);
            this.sold.Name = "sold";
            this.sold.Size = new System.Drawing.Size(52, 24);
            this.sold.TabIndex = 5;
            this.sold.TabStop = true;
            this.sold.Text = "Sold";
            this.sold.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sold_LinkClicked);
            // 
            // IDlabel
            // 
            this.IDlabel.AutoSize = true;
            this.IDlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDlabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.IDlabel.Location = new System.Drawing.Point(33, 240);
            this.IDlabel.Name = "IDlabel";
            this.IDlabel.Size = new System.Drawing.Size(28, 20);
            this.IDlabel.TabIndex = 6;
            this.IDlabel.Text = "ID";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValue.Location = new System.Drawing.Point(317, 240);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(55, 20);
            this.labelValue.TabIndex = 7;
            this.labelValue.Text = "Value";
            // 
            // transactionID
            // 
            this.transactionID.AutoSize = true;
            this.transactionID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transactionID.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.transactionID.Location = new System.Drawing.Point(25, 281);
            this.transactionID.Name = "transactionID";
            this.transactionID.Size = new System.Drawing.Size(41, 13);
            this.transactionID.TabIndex = 8;
            this.transactionID.Text = "label3";
            // 
            // value
            // 
            this.value.AutoSize = true;
            this.value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.value.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.value.Location = new System.Drawing.Point(327, 281);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(41, 13);
            this.value.TabIndex = 9;
            this.value.Text = "label4";
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.editButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editButton.Location = new System.Drawing.Point(413, 281);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 10;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(509, 281);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.Button2_Click);
            // 
            // open
            // 
            this.open.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.open.AutoSize = true;
            this.open.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.open.ForeColor = System.Drawing.Color.IndianRed;
            this.open.LinkColor = System.Drawing.Color.IndianRed;
            this.open.Location = new System.Drawing.Point(614, 174);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(62, 24);
            this.open.TabIndex = 12;
            this.open.TabStop = true;
            this.open.Text = "Open";
            this.open.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.open_LinkClicked);
            // 
            // close
            // 
            this.close.ActiveLinkColor = System.Drawing.Color.IndianRed;
            this.close.AutoSize = true;
            this.close.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.ForeColor = System.Drawing.Color.IndianRed;
            this.close.LinkColor = System.Drawing.Color.IndianRed;
            this.close.Location = new System.Drawing.Point(714, 174);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(63, 24);
            this.close.TabIndex = 13;
            this.close.TabStop = true;
            this.close.Text = "Close";
            this.close.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.close_LinkClicked);
            // 
            // labelDiginote
            // 
            this.labelDiginote.AutoSize = true;
            this.labelDiginote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDiginote.Location = new System.Drawing.Point(95, 240);
            this.labelDiginote.Name = "labelDiginote";
            this.labelDiginote.Size = new System.Drawing.Size(85, 20);
            this.labelDiginote.TabIndex = 16;
            this.labelDiginote.Text = "Diginotes";
            // 
            // nDiginotes
            // 
            this.nDiginotes.AutoSize = true;
            this.nDiginotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nDiginotes.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.nDiginotes.Location = new System.Drawing.Point(113, 281);
            this.nDiginotes.Name = "nDiginotes";
            this.nDiginotes.Size = new System.Drawing.Size(41, 13);
            this.nDiginotes.TabIndex = 17;
            this.nDiginotes.Text = "label6";
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(812, 79);
            this.label7.TabIndex = 30;
            this.label7.Text = "Nome de utilizador";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(7, 91);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(260, 50);
            this.button5.TabIndex = 27;
            this.button5.Text = "Main Page";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(546, 91);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(260, 50);
            this.button6.TabIndex = 29;
            this.button6.Text = "Statistics";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.DarkSalmon;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(278, 91);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(260, 50);
            this.button7.TabIndex = 28;
            this.button7.Text = "Orders";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // labelQuotation
            // 
            this.labelQuotation.AutoSize = true;
            this.labelQuotation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuotation.Location = new System.Drawing.Point(199, 240);
            this.labelQuotation.Name = "labelQuotation";
            this.labelQuotation.Size = new System.Drawing.Size(88, 20);
            this.labelQuotation.TabIndex = 31;
            this.labelQuotation.Text = "Quotation";
            // 
            // quotation
            // 
            this.quotation.AutoSize = true;
            this.quotation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quotation.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.quotation.Location = new System.Drawing.Point(222, 281);
            this.quotation.Name = "quotation";
            this.quotation.Size = new System.Drawing.Size(41, 13);
            this.quotation.TabIndex = 32;
            this.quotation.Text = "label9";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.IndianRed;
            this.label.Location = new System.Drawing.Point(256, 253);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(318, 25);
            this.label.TabIndex = 33;
            this.label.Text = "There is no orders registered";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(286, 99);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // OrdersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(812, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.quotation);
            this.Controls.Add(this.labelQuotation);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.nDiginotes);
            this.Controls.Add(this.labelDiginote);
            this.Controls.Add(this.close);
            this.Controls.Add(this.open);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.value);
            this.Controls.Add(this.transactionID);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.IDlabel);
            this.Controls.Add(this.sold);
            this.Controls.Add(this.bought);
            this.Controls.Add(this.all);
            this.Name = "OrdersList";
            this.Text = "OrdersList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel all;
        private System.Windows.Forms.LinkLabel bought;
        private System.Windows.Forms.LinkLabel sold;
        private System.Windows.Forms.Label IDlabel;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Label transactionID;
        private System.Windows.Forms.Label value;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.LinkLabel open;
        private System.Windows.Forms.LinkLabel close;
        private System.Windows.Forms.Label labelDiginote;
        private System.Windows.Forms.Label nDiginotes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label labelQuotation;
        private System.Windows.Forms.Label quotation;
        private System.Windows.Forms.Label label;
        private ArrayList labels = new ArrayList(50);
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
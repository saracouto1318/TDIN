using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient
{
    public partial class OrdersList : Form
    {
        public OrdersList()
        {
            InitializeComponent();
            ExistTransactions();
            CreateTable(null);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //Program.context.ChangeForm(this, new CreateOrder(false));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to delete this order?";
            string caption = "Error Detected in Input";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                this.Close();
            }
            else
            {

            }
        }

        private void ExistTransactions()
        {
            if(Services.GetInstance().GetMyTransactions(TransactionType.ALL, false) == null || Services.GetInstance().GetMyTransactions(TransactionType.ALL, true) == null)
            {
                this.all.Visible = false;
                this.bought.Visible = false;
                this.sold.Visible = false;
                this.open.Visible = false;
                this.close.Visible = false;
                this.IDlabel.Visible = false;
                this.labelDiginote.Visible = false;
                this.labelQuotation.Visible = false;
                this.labelValue.Visible = false;
                this.transactionID.Visible = false;
                this.value.Visible = false;
                this.nDiginotes.Visible = false;
                this.quotation.Visible = false;
                this.editButton.Visible = false;
                this.deleteButton.Visible = false;
                this.label.Visible = true;
            }
            else
            {
                this.label.Visible = false;
            }
        }

        private void all_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            List<Label> labels = new List<Label>();

            for (int i = 0; i < this.labels.Count; i++)
            {
                var temp = new Label();

                temp.Location = new Point(0, 0);
                temp.Text = "o";

                temp.BackColor = System.Drawing.Color.White;

                this.Controls.Add(temp);

                temp.Show();
                labels.Add(temp);
            }
        }

        private void bought_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void sold_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void open_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void close_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void CreateTable(List<Transaction> transactions)
        {
            RowStyle temp = this.tableLayoutPanel1.RowStyles[this.tableLayoutPanel1.RowCount - 1];
            //increase panel rows count by one
            this.tableLayoutPanel1.RowCount++;
            //add a new RowStyle as a copy of the previous one
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            //add your three controls
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "ID" }, 0, this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "Diginotes" }, 1, this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "Quotation" }, 2, this.tableLayoutPanel1.RowCount - 1);
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "Price" }, 2, this.tableLayoutPanel1.RowCount - 1);
        }
    }
}

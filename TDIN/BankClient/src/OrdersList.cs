﻿using System;
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
            GetUserInforamtionAsync();
        }

        private async void GetUserInforamtionAsync()
        {
            User user = null;
            await Task.Run(() =>
            {
                try
                {
                    user = Services.GetInstance().GetUserInformation();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Program.context.ChangeForm(this, new AuthenticationPage());
                }
            });
            UpdateUserProfile(user);
        }

        private void UpdateUserProfile(User user)
        {
            if (user == null)
            {
                return;
            }
            userName.Text = user.name;
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
                this.label2.Visible = false;
                this.comboBox1.Visible = false;
                this.tableLayoutPanel1.Visible = false;
                this.label.Visible = true;
            }
            else
            {
                this.label.Visible = false;
                CreateTable(Services.GetInstance().GetMyTransactions(TransactionType.ALL, true));
            }
        }

        private void CreateTable(List<Transaction> transactions)
        {
            RowStyle temp = this.tableLayoutPanel1.RowStyles[this.tableLayoutPanel1.RowCount - 1];
            //increase panel rows count by one
            this.tableLayoutPanel1.RowCount++;
            //add a new RowStyle as a copy of the previous one
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
            //add your three controls
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "ID", TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Black, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold) }, 0, 0);
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "Diginotes", TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Black, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold) }, 1, 0);
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "Quotation", TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Black, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold) }, 2, 0);
            this.tableLayoutPanel1.Controls.Add(new Label() { Text = "Price", TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Black, Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold) }, 3, 0);

            for(int i=0; i<transactions.Count; i++)
            {
                this.tableLayoutPanel1.Controls.Add(new Label() { Text = transactions[i].ID.ToString(), TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Gray, Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold) }, 0, i+1);
                this.tableLayoutPanel1.Controls.Add(new Label() { Text = transactions[i].quantity.ToString(), TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Gray, Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold) }, 1, i+1);
                this.tableLayoutPanel1.Controls.Add(new Label() { Text = "2", TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Gray, Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold) }, 2, i+1);
                this.tableLayoutPanel1.Controls.Add(new Label() { Text = "3", TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.Gray, Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold) }, 3, i+1);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.comboBox1.SelectedItem)
            {
                case "All":
                    CreateTable(Services.GetInstance().GetMyTransactions(TransactionType.ALL, true));
                    break;
                case "Purchase Orders - Open":
                    CreateTable(Services.GetInstance().GetMyTransactions(TransactionType.BUY, true));
                    break;
                case "Purchase Orders - Close":
                    CreateTable(Services.GetInstance().GetMyTransactions(TransactionType.BUY, false));
                    break;
                case "Selling Orders - Open":
                    CreateTable(Services.GetInstance().GetMyTransactions(TransactionType.SELL, true));
                    break;
                case "Selling Orders - Close":
                    CreateTable(Services.GetInstance().GetMyTransactions(TransactionType.SELL, false));
                    break;
            }
               
        }
    }
}

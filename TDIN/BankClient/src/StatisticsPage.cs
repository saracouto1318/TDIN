﻿using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankClient
{
    public partial class StatisticsPage : Form
    {
        public StatisticsPage()
        {
            InitializeComponent();
            UpdateChart();
            UpdateTransactionsInfo();
            GetUserInformationAsync();
        }

        private async void GetUserInformationAsync()
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

        private void Button2_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void Button4_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void Button5_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void UpdateChart()
        {
            Dictionary<float, int> quotes = Services.GetInstance().GetQuotationFlutuation();

            bool success = false;

            foreach (var pair in quotes)
            {
                if(pair.Value != 0) { 
                    chart1.Series["Quote"].Points.AddXY(pair.Key, pair.Value);
                    success = true;
                    chart1.Visible = true;
                    label.Visible = false;
                }
            }

            if (!success)
            {
                chart1.Visible = false;
                label.Visible = true;
            }     
        }

        private void UpdateTransactionsInfo()
        {
            List<Transaction> myTransactions = Services.GetInstance().GetMyTransactions(TransactionType.ALL, false);
            List<Transaction> mySelltransactions = Services.GetInstance().GetMyTransactions(TransactionType.SELL, false);
            List<Transaction> myBuyTransactions = Services.GetInstance().GetMyTransactions(TransactionType.BUY, false);

            if (myTransactions == null || mySelltransactions == null || myBuyTransactions == null)
            {
                Services.GetInstance().OnExit();
                Program.context.ChangeForm(this, new AuthenticationPage());
                return;
            }
            else
            {
                nTransactions.Text = myTransactions.Count.ToString();

                int open = myTransactions.Count - mySelltransactions.Count - myBuyTransactions.Count;
                int close = mySelltransactions.Count + myBuyTransactions.Count;
                tOpen.Text = open.ToString();
                tClose.Text = close.ToString();
            }

            int quantity = 0;       
            foreach (Transaction t in mySelltransactions)
                quantity += t.quantity;

            digiSold.Text = quantity.ToString();
            
            quantity = 0;
            foreach (Transaction t in myBuyTransactions)
                quantity += t.quantity;

            digiBought.Text = quantity.ToString();
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Services.GetInstance().DeleteSession();
            Program.context.ChangeForm(this, new AuthenticationPage());
            Program.exit();
        }
    }
}

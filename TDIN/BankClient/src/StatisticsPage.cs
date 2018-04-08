using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace BankClient
{
    public partial class StatisticsPage : Form
    {
        public StatisticsPage()
        {
            InitializeComponent();
            UpdateChart();
            UpdateTransactionsInfo();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void UpdateChart()
        {
            /*Dictionary<float, int> quotes = Services.GetInstance().GetQuotationFlutuation();

            foreach (var pair in quotes)
            {
                this.chart1.Series["Quote"].Points.AddXY(pair.Key, pair.Value);
            }*/
        }

        private void UpdateTransactionsInfo()
        {
            /*this.nTransactions.Text = Services.GetInstance().GetMyTransactions(TransactionType.ALL).Count.ToString();

            int quantity = 0;
            List<Transaction> transactions = Services.GetInstance().GetMyTransactions(TransactionType.SELL);
            foreach (Transaction t in transactions)
                quantity += t.quantity;

            this.digiSold.Text = quantity.ToString();

            transactions = Services.GetInstance().GetMyTransactions(TransactionType.BUY);

            quantity = 0;
            foreach (Transaction t in transactions)
                quantity += t.quantity;

            this.digiBought.Text = quantity.ToString();*/
        }
    }
}

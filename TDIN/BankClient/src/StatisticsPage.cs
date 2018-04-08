using System;
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
            Dictionary<float, int> quotes = Services.GetInstance().GetQuotationFlutuation();

            foreach (var pair in quotes)
            {
                this.chart1.Series["Quote"].Points.AddXY(pair.Key, pair.Value);
            }
        }

        private void UpdateTransactionsInfo()
        {
            if (Services.GetInstance().GetMyTransactions(TransactionType.ALL, false) == null)
                this.nTransactions.Text = "0";
            else
                this.nTransactions.Text = Services.GetInstance().GetMyTransactions(TransactionType.ALL, false).Count.ToString();

            int quantity = 0;
            List<Transaction> transactions = Services.GetInstance().GetMyTransactions(TransactionType.SELL, false);

            if (transactions == null)
                this.digiSold.Text = "0";
            else
            {
                foreach (Transaction t in transactions)
                    quantity += t.quantity;

                this.digiSold.Text = quantity.ToString();
            }
           
            transactions = Services.GetInstance().GetMyTransactions(TransactionType.BUY, false);

            if (transactions == null)
                this.digiSold.Text = "0";
            else
            {
                quantity = 0;
                foreach (Transaction t in transactions)
                    quantity += t.quantity;

                this.digiBought.Text = quantity.ToString();
            }
        }
    }
}

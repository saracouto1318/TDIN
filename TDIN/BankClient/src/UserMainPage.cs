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
    public partial class UserMainPage : Form
    {
        public UserMainPage()
        {
            InitializeComponent();
            GetUserInformationAsync();
        }

        private async void GetUserInformationAsync()
        {
            User user = null;
            float power = 1f;
            await Task.Run(() =>
            {
                try
                {
                    user = Services.GetInstance().GetUserInformation();
                    power = Services.GetInstance().GetPower();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Program.context.ChangeForm(this, new AuthenticationPage());
                }
            });
            UpdateUserInformation(user, power);
        }

        private void UpdateUserInformation(User user, float power)
        {
            if(user == null)
            {
                return;
            }
            userName.Text = user.name;
            nDiginotes.Text = user.availableDiginotes + "/" + user.totalDiginotes;
            quotation.Text = power + "$";
            balance.Text = user.balance + "$";
        }

        private void EditProfile_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new EditProfile());
        }

        private void AddFunds_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new AddFunds());
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new CreateOrder(TransactionType.BUY));
        }

        private void SellButton_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new CreateOrder(TransactionType.SELL));
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new Wallet());
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            //.GetInstance().DeleteSession()
        }
    }
}

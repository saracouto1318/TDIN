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
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void Button5_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
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

        private void Logout_Click(object sender, EventArgs e)
        {
            Services.GetInstance().DeleteSession();
            Program.context.ChangeForm(this, new AuthenticationPage());
            Program.exit();
        }
    }
}

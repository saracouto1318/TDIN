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
    public partial class AddFunds : Form
    {
        public AddFunds()
        {
            InitializeComponent();
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

        private void Button6_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                float funds = float.Parse(this.fundsAdded.Text, System.Globalization.CultureInfo.InvariantCulture);
                if(!Services.GetInstance().AddingFunds(funds))
                {
                    CreateOkBox("Server error while adding funds to your account", "Error");
                }
                else
                {
                    CreateOkBox("Success", "Add Funds");
                }
            } catch(Exception)
            {
                CreateOkBox("Error adding funds to your account", "Error");
            }

            fundsAdded.Text = "";
        }

        private void CreateOkBox(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
        }

    }
}

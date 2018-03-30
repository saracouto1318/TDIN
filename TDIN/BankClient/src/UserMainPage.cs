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
            await Task.Run(() =>
            {
                try
                {
                    user = Services.GetInstance().GetUserInformation();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Program.ChangeForm(this, new AuthenticationPage());
                }
            });
            UpdateUserInformation(user);
        }

        private void UpdateUserInformation(User user)
        {
            if(user == null)
            {
                return;
            }
            userName.Text = user.name;
            nDiginotes.Text = user.numDiginotes + "";
            quotation.Text = "1.00$";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.ChangeForm(this, new EditProfile());
        }
    }
}

using System;
using System.Windows.Forms;

namespace BankClient
{
    public partial class AuthenticationPage : Form
    {
        public AuthenticationPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameLog.Text;
            string password = PasswordLog.Text;

            if(Services.GetInstance().LoginUser(username, password))
            {
                Program.ChangeForm(this, new UserMainPage());
            }
            else
            {
                label2.Visible = true;
            }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            string name = NameOfUser.Text;
            string username = UsernameReg.Text;
            string password = PasswordReg.Text;

            if(Services.GetInstance().RegisterUser(username, password, name))
            {
                Program.ChangeForm(this, new UserMainPage());
            }
            else
            {
                label1.Visible = true;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        { 
            label1.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
        }
    }
}

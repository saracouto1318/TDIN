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

            AuthenticationObj authObj = Program.GetAuthObj();
            
            // Login user
            UserSession session = authObj.Login(username, password);
            if (session != null)
            {
                Program.ChangeForm(this, new MainPage(session));
                return;
            }
            
            label2.Visible = true;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            string name = NameOfUser.Text;
            string username = UsernameReg.Text;
            string password = PasswordReg.Text;
            
            AuthenticationObj authObj = Program.GetAuthObj();

            // Validate username
            if(!authObj.IsUsernameAvailable(username))
            {
                label1.Visible = true;
                return;
            }
            // Register user
            UserSession session = authObj.Register(username, password, name);
            if (session != null)
            {
                Program.ChangeForm(this, new MainPage(session));
                return;
            }

            label1.Visible = true;
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

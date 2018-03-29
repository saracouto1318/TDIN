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
            Console.WriteLine("Login");

            string username = UsernameLog.Text;
            string password = PasswordLog.Text;

            AuthenticationObj authObj = Program.GetAuthObj();

            // Validate username
            if (!authObj.IsValidUsername(username))
            {
                Console.WriteLine("Invalid username");
                label2.Visible = true;
                return;
            }
            // Login user
            String session = authObj.Login(username, password);
            if (session != null)
            {
                Console.WriteLine("Session not null");
                Program.ChangeForm(this, new MainPage(session));
            }
            label2.Visible = true;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Register");

            string name = NameOfUser.Text;
            string username = UsernameReg.Text;
            string password = PasswordReg.Text;
            
            AuthenticationObj authObj = Program.GetAuthObj();

            // Validate username
            if(!authObj.IsValidUsername(username))
            {
                Console.WriteLine("Invalid username");
                label1.Visible = true;
                return;
            }
            // Register user
            String session = authObj.Register(username, password, name);
            if (session != null)
            {
                Console.WriteLine("Session not null");
                Program.ChangeForm(this, new MainPage(session));
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

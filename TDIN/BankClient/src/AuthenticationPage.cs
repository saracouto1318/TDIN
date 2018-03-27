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

                return;
            }
            // Login user
            String session = authObj.Login(username, password);
            if (session != null)
            {
                Program.ChangeForm(this, new MainPage(session));
            }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Register");

            string name = NameOfUser.Text;
            string username = UsernameLog.Text;
            string password = PasswordLog.Text;

            this.label1.Visible = true;

            AuthenticationObj authObj = Program.GetAuthObj();

            // Validate username
            if(!authObj.IsValidUsername(username))
            {
                return;
            }
            // Register user
            String session = authObj.Register(username, password, name);
            if (session != null)
            {
                Program.ChangeForm(this, new MainPage(session));
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

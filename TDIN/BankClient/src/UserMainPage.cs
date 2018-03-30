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
            GetUserInformation();
        }

        private async Task GetUserInformation()
        {
            await Task.Run(() =>
            {
                try
                {
                    User user = Services.GetInstance().GetUserInformation();
                    Console.WriteLine("User {0}", user.name);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Program.ChangeForm(this, new AuthenticationPage());
                }
            });
        }
    }
}

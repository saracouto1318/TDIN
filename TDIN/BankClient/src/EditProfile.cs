using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient
{
    public partial class EditProfile : Form
    {        
        public EditProfile()
        {
            InitializeComponent();
            GetUserInforamtionAsync();
        }
        
        private async void GetUserInforamtionAsync()
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
            if(user == null)
            {
                return;
            }
            userName.Text = user.name;
        }

        private void PasswordBtn_Click(object sender, EventArgs e)
        {
            string password = oldPassText.Text;
            string nPassowrd = nPassText.Text;
            
            if (nPassowrd == "" || password == nPassowrd)
            {
                return;
            }

            Services.GetInstance().ChangePassword(password, nPassowrd);
        }

        private void UsernameBtn_Click(object sender, EventArgs e)
        {
            string nUsername = usernameText.Text;
            User user;

            try
            {
                user = Services.GetInstance().GetUserInformation();
            }
            catch(Exception)
            {
                Program.context.ChangeForm(this, new AuthenticationPage());
                return;
            }
            
            if (nUsername == "" || user == null || nUsername == user.username)
            {
                return;
            }

            Services.GetInstance().ChangeUsername(nUsername);
        }

        private void NameBtn_Click(object sender, EventArgs e)
        {
            string nName = nameText.Text;
            User user;

            try
            {
                user = Services.GetInstance().GetUserInformation();
            }
            catch (Exception)
            {
                Program.context.ChangeForm(this, new AuthenticationPage());
                return;
            }

            if (nName == "" || user == null || nName == user.name)
            {
                return;
            }

            Services.GetInstance().ChangeName(nName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }
    }
}

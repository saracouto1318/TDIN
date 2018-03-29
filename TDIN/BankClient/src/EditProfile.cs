using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient
{
    public partial class MainPage : Form
    {
        private enum MainPageState { ASYNC_CALL, IDDLE };
        private MainPageState status = MainPageState.IDDLE;
        
        private UserSession session;
        private User user;

        public MainPage(UserSession sessionId)
        {
            InitializeComponent();
            this.session = sessionId;
            GetUserInforamtionAsync();
        }
        
        private async void GetUserInforamtionAsync()
        {
            status = MainPageState.ASYNC_CALL;
            await Task.Run(() => GetUserInformation());
            UpdateUserProfile();
            status = MainPageState.IDDLE;
        }

        private void GetUserInformation() {
            user = Program.GetAuthObj().UserInformation(session.sessionId);
        }

        private void UpdateUserProfile()
        {
            userName.Text = user.name;
            NameLabel.Text = user.name;
            UsernameLabel.Text = user.username;
        }

        private void PasswordBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Change Password");

            this.label1.Text = "Old Password";
            this.label2.Text = "New Password";
            this.label1.Visible = true;
            this.label2.Visible = true;
            this.textBox1.Visible = true;
            this.textBox2.Visible = true;
            this.button2.Visible = false;
            this.button3.Visible = false;
            this.button1.Visible = true;

            string oldPass = label1.Text;
            string newPass = label2.Text;

            AuthenticationObj authObj = Program.GetAuthObj();
        }

        private void UsernameBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Change Username");

            this.label1.Text = "Old Username";
            this.label2.Text = "New Username";
            this.label1.Visible = true;
            this.label2.Visible = true;
            this.textBox1.Visible = true;
            this.textBox2.Visible = true;
            this.button1.Visible = false;
            this.button3.Visible = false;
            this.button2.Visible = true;

            string oldUser = label1.Text;
            string newUser = label2.Text;

            AuthenticationObj authObj = Program.GetAuthObj();

        }

        private void NameBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Change Name");

            this.label1.Text = "Username";
            this.label2.Text = "New Name";
            this.label1.Visible = true;
            this.label2.Visible = true;
            this.textBox1.Visible = true;
            this.textBox2.Visible = true;
            this.button1.Visible = false;
            this.button2.Visible = false;
            this.button3.Visible = true;

            string username = label1.Text;
            string newName = label2.Text;

            AuthenticationObj authObj = Program.GetAuthObj();

        }
    }
}

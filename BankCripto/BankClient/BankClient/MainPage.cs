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
    public partial class MainPage : Form
    {
        private enum MainPageState { ASYNC_CALL, IDDLE };
        private MainPageState status = MainPageState.IDDLE;

        private string username = "";
        private string name = "";
        private string sessionId;

        public MainPage(string sessionId)
        {
            InitializeComponent();
        
            this.sessionId = sessionId;

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
            User user = Program.GetAuthObj().UserInformation(sessionId);
            if(user != null)
            {
                username = user.username;
                name = user.name;
            }
        }

        private void UpdateUserProfile()
        {
            userName.Text = name;
            NameLabel.Text = name;
            UsernameLabel.Text = username;
        }
    }
}

using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace BankClient
{
    public partial class CreateOrder : Form
    {
        public TransactionType type;

        public CreateOrder(TransactionType type)
        {
            InitializeComponent();
            this.type = type;
            quote.Text = Services.GetInstance().GetPower().ToString() + " $";
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

        private void MainPage_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void Orderes_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void Create_Click(object sender, EventArgs e)
        {
            int nDiginotes = int.Parse(this.numDiginotes.Text, System.Globalization.CultureInfo.InvariantCulture);

            //Insert transaction
            nDiginotes = Services.GetInstance().CheckCompleteTransaction(nDiginotes, type);
            if(nDiginotes < 0)
            {
                CreateOkBox("You don't have the required funds to execute this transactions", "Error");
                Program.context.ChangeForm(this, new UserMainPage());
            }
            else if(nDiginotes == 0)
            {
                CreateOkBox("Success, your transaction has been completed", "Completed Transaction");
                Program.context.ChangeForm(this, new UserMainPage());
            }
            else if(nDiginotes > 0)
            {
                Program.context.ChangeForm(this, new EditOrder(-1, nDiginotes, type));
            }
        }

        private void NumDiginotes_TextChanged(object sender, EventArgs e)
        {
            Price_Change();
        }

        private void Price_Change()
        {
            float price;
            try
            {
                price = 
                    float.Parse(numDiginotes.Text, System.Globalization.CultureInfo.InvariantCulture) * 
                    Services.GetInstance().GetPower();
            } catch(Exception)
            {
                price = 0;
            }
            this.price.Text = price.ToString() + " $";
        }

        private void CreateOkBox(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Services.GetInstance().DeleteSession();
            Program.context.ChangeForm(this, new AuthenticationPage());
            Program.exit();
        }
    }  
}

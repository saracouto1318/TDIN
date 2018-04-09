using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace BankClient
{
    public partial class EditOrder : Form
    {
        private int id;
        private int nDiginotes;
        private TransactionType type;

        public EditOrder(int id, int nDiginotes, TransactionType type)
        {
            InitializeComponent();
            this.id = id;
            this.nDiginotes = nDiginotes;
            this.type = type;
            diginotes.Text = nDiginotes.ToString();
            GetUserInformationAsync();
        }

        private async void GetUserInformationAsync()
        {
            User user = null;
            float power = 1f;
            await Task.Run(() =>
            {
                try
                {
                    user = Services.GetInstance().GetUserInformation();
                    power = Services.GetInstance().GetPower();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Program.context.ChangeForm(this, new AuthenticationPage());
                }
            });
            UpdateUserInformation(user, power);
        }

        private void UpdateUserInformation(User user, float power)
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

        private void Orders_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void EditOrder_Click(object sender, EventArgs e)
        {
            float quote = float.Parse(this.quote.Text, System.Globalization.CultureInfo.InvariantCulture);
            float currentQuote = Services.GetInstance().GetPower();

            if (quote <= 0 || (type == TransactionType.SELL && quote > currentQuote) || (type == TransactionType.BUY && quote < currentQuote))
            {
                CreateOkBox("Invalid quotation", "Error");
                Program.context.ChangeForm(this, new UserMainPage());
                return;
            }

            string message = "Are you sure you want to edit the quotation?";
            string caption = "Edit Quotation";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);

            if (result == DialogResult.Yes)
            {
                if (quote != currentQuote)
                {
                    Services.GetInstance().SetPower(quote);
                }

                //Edit quote
                if(id < 0)
                {
                    int insertedId = Services.GetInstance().InsertTransaction(nDiginotes, type);
                    if(insertedId < 0)
                    {
                        CreateOkBox("Error creating the new transaction", "Error");
                        Program.context.ChangeForm(this, new UserMainPage());
                    }
                    else
                    {
                        Program.context.ChangeForm(this, new OrdersList());
                    }
                }
            }
        }

        private void CreateOkBox(string message, string caption)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
        }
        
        private void Quote_TextChanged(object sender, EventArgs e)
        {
            Price_Change();
        }

        private void Price_Change()
        {
            float price;
            try
            {
                price =
                    nDiginotes *
                    float.Parse(quote.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                price = 0;
            }
            this.price.Text = price.ToString() + " $";
        }

        private void Logout_Click(object sender, EventArgs e)
        {
            Services.GetInstance().DeleteSession();
            Program.context.ChangeForm(this, new AuthenticationPage());
            Program.exit();
        }
    }
}

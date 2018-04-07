using System;
using System.Windows.Forms;

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

            if (quote <= 0)
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
                        CreateOkBox("Success", "Create transaction");
                        Program.context.ChangeForm(this, new UserMainPage());
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
    }
}

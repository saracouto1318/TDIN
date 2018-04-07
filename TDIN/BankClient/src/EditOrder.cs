using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient.src
{
    public partial class EditOrder : Form
    {
        public EditOrder(int ID, int numDiginotes)
        {
            InitializeComponent();
            this.diginotes.Text = numDiginotes.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float quote = float.Parse(this.quote.Text, System.Globalization.CultureInfo.InvariantCulture);
            string message = "Are you sure you want to edit the quotation?";
            string caption = "Edit Quotation";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.No)
            {
                // Closes the parent form.
                this.Close();
            }
            else
            {
                //Edit quote
            }
        }

        private void price_Change(object sender, EventArgs e)
        {
            float price = float.Parse(this.diginotes.Text, System.Globalization.CultureInfo.InvariantCulture) * float.Parse(this.quote.Text, System.Globalization.CultureInfo.InvariantCulture);
            this.price.Text = price.ToString() + " $";
        }
    }
}

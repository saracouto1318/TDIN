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
    public partial class CreateOrder : Form
    {
        public bool type;
        public int ID;
        public CreateOrder()
        {
            InitializeComponent();
            this.quote.Text = Services.GetInstance().GetPower().ToString() + " $";
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
            int numDiginotes = int.Parse(this.numDiginotes.Text, System.Globalization.CultureInfo.InvariantCulture);
            //Insert transaction
        }

        private void price_Change(object sender, EventArgs e)
        {
            float price = float.Parse(this.numDiginotes.Text, System.Globalization.CultureInfo.InvariantCulture) * float.Parse(this.quote.Text, System.Globalization.CultureInfo.InvariantCulture);
            this.price.Text = price.ToString() + " $";
        }
    }  
}

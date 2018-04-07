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
    public partial class AddFunds : Form
    {
        public AddFunds()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }

        private void add_Click(object sender, EventArgs e)
        {
            float funds = float.Parse(this.fundsAdded.Text, System.Globalization.CultureInfo.InvariantCulture);
            Services.GetInstance().AddingFunds(funds);
        }
    }
}

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
    public partial class OrdersList : Form
    {
        public OrdersList()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }
    }
}

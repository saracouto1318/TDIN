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
    public partial class CreateEditOrder : Form
    {
        public bool type;
        public CreateEditOrder(bool type)
        {
            this.type = type;
            InitializeComponent();
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
    }  
}

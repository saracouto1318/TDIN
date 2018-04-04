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
        public int ID;
        public CreateEditOrder(bool type, int ID)
        {
            this.type = type;
            this.ID = ID;
            if (this.type)
            {
                this.label5.Visible = false;
                this.textBox2.Visible = true;
                this.button3.Visible = true;
                this.button1.Visible = false;
            }
            else
            {
                this.textBox2.Visible = false;
                this.label5.Visible = true;
                this.button1.Visible = true;
                this.button3.Visible = false;
            }
                
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

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "Are you sure you want to edit the quotation of your diginotes?";
            string caption = "Error Detected in Input";
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

            }

        }
    }  
}

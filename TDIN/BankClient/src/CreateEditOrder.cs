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
        public string type;
        public CreateEditOrder(string type)
        {
            this.type = type;
            InitializeComponent();
        }
    }
}

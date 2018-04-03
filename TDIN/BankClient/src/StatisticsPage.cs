using System.Windows.Forms;

namespace BankClient
{
    public partial class StatisticsPage : Form
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new UserMainPage());
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new OrdersList());
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            Program.context.ChangeForm(this, new StatisticsPage());
        }
    }
}

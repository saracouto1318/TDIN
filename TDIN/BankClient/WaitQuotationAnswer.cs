using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient
{
    public class WaitQuotationAnswer
    {
        public WaitQuotationAnswer()
        {
            Program.exit += OnExit;
            Program.authentication += (delegate() {
                Console.WriteLine("Authentication");
                Services.GetInstance().inter.UpdatePower += OnPowerChange;
            });
        }

        public void OnPowerChange(float power)
        {
            if (Services.GetInstance().GetMyTransactions(TransactionType.ALL, true).Count == 0)
                return;
            AsyncMessageBox();
            Console.WriteLine("End message box of user " + Services.GetInstance().user.name);
        }

        public void OnExit()
        {
            if(Services.GetInstance().inter != null)
                Services.GetInstance().inter.UpdatePower -= OnPowerChange;
        }

        public async void AsyncMessageBox()
        {
            string message = "Mr./Mr.s " + Services.GetInstance().user.name + 
                "\nThe diginotes quotation will change to " + Services.GetInstance().power.ToString() + 
                "\nWill you accept the new quotation?";
            string caption = "Accept New Quotation";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            uint timeout = 60000;
            await Task.Run(() => {
                DialogResult result;
                result = MessageBoxTimeout.Show(message, caption, buttons, timeout);

                if (result == DialogResult.Yes)
                {
                    Program.virtualTransaction.ActivateTransation(Services.GetInstance().session.sessionId, true);
                }
                else
                {
                    Program.virtualTransaction.ActivateTransation(Services.GetInstance().session.sessionId, false);
                }
            });
        }
    }
}

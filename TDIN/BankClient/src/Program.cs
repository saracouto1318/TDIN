using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient
{
    static class Program
    {
        private static AuthenticationObj authObj;
        
        [STAThread]
        static void Main()
        {
            RemotingConfiguration.Configure("App.config", false);
            authObj = new AuthenticationObj();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new AuthenticationPage().Show();

            Application.Run();
        }

        public static AuthenticationObj GetAuthObj()
        {
            if (authObj != null)
                authObj = new AuthenticationObj();
            return authObj;
        }

        public static void ChangeForm(Form activeForm, Form changeToForm)
        {
            activeForm.Close();
            changeToForm.Show();
        }
    }
}

class AuthenticationObj : MarshalByRefObject, IUser
{
    public string Hello()
    {
        return null;
    }

    public string Login(string username, string password)
    {
        return null;
    }

    public bool IsValidUsername(string username)
    {
        return false;
    }

    public string Register(string username, string password, string name)
    {
        return null;
    }

    public User UserInformation(string sessionId)
    {
        return null;
    }
}

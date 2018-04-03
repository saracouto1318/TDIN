using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankClient
{
    static class Program
    {
        public static IUser virtualUser;
        public static ITransaction virtualTransaction;
        public static MyApplicationContext context;

        [STAThread]
        static void Main()
        {
            RemotingConfiguration.Configure("BankClient.exe.config", false);
            virtualUser = (IUser)GetRemote.New(typeof(IUser));
            virtualTransaction = (ITransaction)GetRemote.New(typeof(ITransaction));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            context = new MyApplicationContext();
            context.ShowForm(new AuthenticationPage());

            Application.Run(context);
        }
    }

    class MyApplicationContext : ApplicationContext
    {
        private int formsCount;

        public MyApplicationContext()
        {
            formsCount = 0;
        }

        public void ShowForm(Form form)
        {
            formsCount++;
            form.FormClosed += OnCloseForm;
            form.Show();
        }

        public void ChangeForm(Form activeForm, Form changeToForm)
        {
            ShowForm(changeToForm);
            activeForm.Close();
        }

        private void OnCloseForm(object sender, EventArgs e)
        {
            formsCount--;
            if (Application.OpenForms.Count == 0)
            {
                Application.Exit();
            }
        }
    }

    class GetRemote
    {
        private static IDictionary wellKnownTypes;

        public static object New(Type type)
        {
            if (wellKnownTypes == null)
                InitTypeCache();
            WellKnownClientTypeEntry entry = (WellKnownClientTypeEntry)wellKnownTypes[type];
            if (entry == null)
                throw new RemotingException("Type not found!");
            return Activator.GetObject(type, entry.ObjectUrl);
        }

        public static void InitTypeCache()
        {
            Hashtable types = new Hashtable();
            foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            {
                if (entry.ObjectType == null)
                    throw new RemotingException("A configured type could not be found!");
                types.Add(entry.ObjectType, entry);
            }
            wellKnownTypes = types;
        }
    }
}

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

        [STAThread]
        static void Main()
        {
            RemotingConfiguration.Configure("BankClient.exe.config", false);
            virtualUser = (IUser)GetRemote.New(typeof(IUser));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new AuthenticationPage().Show();

            Application.Run();
        }

        public static void ChangeForm(Form activeForm, Form changeToForm)
        {
            activeForm.Close();
            changeToForm.Show();
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

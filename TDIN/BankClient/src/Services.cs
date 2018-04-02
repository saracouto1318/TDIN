using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankClient
{
    public class NotAuthorizedOperationException : Exception
    {
        public override string Message => "That operation requires the user to be logged in";
    }

    public class Services
    {
        private static Services instance;

        public UserSession session;
        public User user;

        private Services() { }

        public static Services GetInstance()
        {
            if (instance == null)
                instance = new Services();
            return instance;
        }

        #region Authentication

        public bool LoginUser(string username, string password)
        {
            // Login user
            UserSession uSession = Program.virtualUser.Login(username, password);
            if (uSession != null)
            {
                session = uSession;
                return true;
            }
            return false;
        }

        public bool RegisterUser(string username, string password, string name)
        {
            // Validate username
            if (!Program.virtualUser.IsUsernameAvailable(username))
            {
                return false;
            }

            // Register user
            UserSession uSession = Program.virtualUser.Register(username, password, name);
            if (uSession != null)
            {
                session = uSession;
                return true;
            }

            return false;
        }

        #endregion

        #region User

        public User GetUserInformation()
        {
            if(session == null)
            {
                throw new NotAuthorizedOperationException();
            }

            if(user == null)
            {
                user = Program.virtualUser.UserInformation(session.sessionId);
            }

            return user;
        }

        public bool ChangeName(string nName)
        {
            if (Program.virtualUser.ChangeName(session.sessionId, nName))
            {
                user.name = nName;
                // Notify change of name

                return true;
            }
            return false;
        }

        public bool ChangeUsername(string nUsername)
        {
            if (Program.virtualUser.ChangeUsername(session.sessionId, nUsername))
            {
                user.username = nUsername;
                // Notify change of username

                return true;
            }
            return false;
        }

        public bool ChangePassword(string password, string nPassword)
        {
            if (Program.virtualUser.ChangePassowrd(session.sessionId, password, nPassword))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}

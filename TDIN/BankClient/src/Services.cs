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
            AuthenticationObj authObj = Program.GetAuthObj();

            // Login user
            UserSession uSession = authObj.Login(username, password);
            if (uSession != null)
            {
                session = uSession;
                return true;
            }
            return false;
        }

        public bool RegisterUser(string username, string password, string name)
        {
            AuthenticationObj authObj = Program.GetAuthObj();

            // Validate username
            if (!authObj.IsUsernameAvailable(username))
            {
                return false;
            }

            // Register user
            UserSession uSession = authObj.Register(username, password, name);
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
                user = Program.GetAuthObj().UserInformation(session.sessionId);
            }

            return user;
        }

        #endregion
    }
}

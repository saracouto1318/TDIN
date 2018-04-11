using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
        public Intermediate inter;
        public float power = -1f;
        public List<Transaction> myTransactions;
        public List<Transaction> otherTransactions;
        public Dictionary<float, int> quotationFlutuation;
        public WaitQuotationAnswer waitQuotationAnswer;

        private Services()
        {
            inter = new Intermediate();

            Program.exit += OnExit;
            Program.authentication += OnAuthentication;
            WaitQuotationAnswer waitQuotationAnswer = new WaitQuotationAnswer();
        }

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
                Program.authentication();
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
                Program.authentication();
                return true;
            }

            return false;
        }

        public void OnAuthentication()
        {
            // Subscribe to events
            inter = new Intermediate();
            inter.UpdatePower += OnPowerChange;
            Program.virtualTransaction = (ITransaction)GetRemote.New(typeof(ITransaction));
            Program.virtualTransaction.UpdatePower += inter.FireUpdatePower;
        }

        public void OnExit()
        {
            if (inter == null)
                return;
            Program.virtualTransaction.UpdatePower -= inter.FireUpdatePower;
        }

        public void DeleteSession()
        {
            Program.virtualUser.DeleteSession(session.username);
        }

        #endregion

        #region User

        public User GetUserInformation()
        {
            if(session == null)
            {
                throw new NotAuthorizedOperationException();
            }

            user = Program.virtualUser.UserInformation(session.sessionId);
            return user;
        }

        public bool ChangeName(string nName)
        {
            if (Program.virtualUser.ChangeName(session.sessionId, nName))
            {
                user.name = nName;
                return true;
            }
            return false;
        }

        public bool ChangeUsername(string nUsername)
        {
            if (Program.virtualUser.ChangeUsername(session.sessionId, nUsername))
            {
                user.username = nUsername;
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

        public bool AddingFunds(float funds)
        {
            if (Program.virtualUser.AddingFunds(session.sessionId, funds))
            {
                user.balance += funds;
                return true;
            }
            return false;
        }

        #endregion

        #region Diginotes

        public float GetPower()
        {
            if(power < 0)
                power = Program.virtualTransaction.GetPower();
            return power;
        }

        public void SetPower(float power)
        {
            Program.virtualTransaction.SetPower(session.sessionId, power);
        }

        public List<int> GetDiginotes(string sessionID)
        {
            return Program.virtualUser.GetDiginotes(sessionID);
        }

        public void OnPowerChange(float power)
        {
            this.power = power;
        }

        #endregion

        #region Transaction

        public List<Transaction> GetMyTransactions(TransactionType type, bool open)
        {
            if (session == null)
            {
                throw new NotAuthorizedOperationException();
            }
            return Program.virtualTransaction.GetMyTransactions(session.sessionId, type, open);
        }

        public List<Transaction> GetOtherTransactions()
        {
            if (session == null)
            {
                throw new NotAuthorizedOperationException();
            }
            return Program.virtualTransaction.GetOtherTransactions(session.sessionId);
        }
        
        public int CheckCompleteTransaction(int nDiginotes, TransactionType type)
        {
            if (session == null)
            {
                throw new NotAuthorizedOperationException();
            }

            Transaction t = NewTransaction(nDiginotes, type);
            if (t == null)
                return nDiginotes;
            return Program.virtualTransaction.CheckCompleteTransaction(session.sessionId, t, type);
        }

        public int InsertTransaction(int nDiginotes, TransactionType type)
        {
            if (session == null)
            {
                throw new NotAuthorizedOperationException();
            }

            Transaction t = NewTransaction(nDiginotes, type);
            if (t == null)
                return -1;
            return Program.virtualTransaction.InsertTransaction(session.sessionId, t, type);
        }

        private Transaction NewTransaction(int nDiginotes, TransactionType type)
        {
            Transaction t = new Transaction
            {
                quantity = nDiginotes,
                date = DateTime.Now
            };
            switch (type)
            {
                case TransactionType.BUY:
                    t.buyer = GetUserInformation().username;
                    break;
                case TransactionType.SELL:
                    t.seller = GetUserInformation().username;
                    break;
                default:
                    return null;
            }
            return t;
        }

        public Dictionary<float, int> GetQuotationFlutuation()
        {
            quotationFlutuation = Program.virtualTransaction.GetQuotationFlutuation();
            return quotationFlutuation;
        }

        #endregion
    }
}

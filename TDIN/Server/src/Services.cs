using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class Services {
    private static Services _instance;
    private Database.Database _db;

    private Task waitActiveTransactions;

    private Services() {
        _db = Database.Database.Initialize();
    }

    public static Services GetInstance() {
        if(_instance == null)
            _instance = new Services();
        return _instance;
    }

    #region Hash

    private byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = SHA256.Create();  //or use SHA256.Create
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    private string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));
        return sb.ToString();
    }

    #endregion

    #region Authentication

    public bool IsUsernameAvailable(string username)
    {
        // Check username against db entries
        return !_db.UserExists(username);
    }

    public bool IsValidPassword(string password)
    {
        // Regex checker for password
        return true;
    }

    public bool IsValidRegister(string username, string password)
    {
        return IsUsernameAvailable(username) && IsValidPassword(password);
    }

    public bool LoginUser(string username, string password)
    {
        string passHash = GetHashString(password);
        return _db.ValidateUser(username, passHash);
    }

    public bool RegisterUser(string username, string password, string name)
    {
        string passHash = GetHashString(password);
        return _db.InsertUser(name, username, passHash);
    }

    #endregion

    #region Session
    public UserSession StoreSession(string username)
    {
        string userSession = System.Guid.NewGuid().ToString();
        if (!StoreSession(username, userSession))
            return null;
        return new UserSession(username, userSession);
    }

    private bool StoreSession(string username, string sessionId)
    {
        _db.DeleteSession(username);
        return _db.InsertSession(username, sessionId);
    }

    public void DeleteSession(string username)
    {
        _db.DeleteSession(username);
    }

    #endregion

    #region User

    public User GetUserInformation(string sessionId)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null)
            return null;
        User user = _db.GetUser(username);
        return user;
    }

    public bool ChangeName(string sessionId, string nName)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null)
            return false;
        return _db.ChangeName(nName, username);
    }

    public bool ChangeUsername(string sessionId, string nUsername)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null)
            return false;
        return _db.ChangeUsername(nUsername, username);
    }

    public bool ChangePassword(string sessionId, string password, string nPassword)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null || !LoginUser(username, password))
            return false;
        string nPassHash = GetHashString(nPassword);
        return _db.ChangePassword(nPassHash, username);
    }

    public bool AddingFunds(string sessionId, float funds)
    {
        if (funds <= 0)
            return false;

        string username = _db.GetUsername(sessionId);
        if (username == null)
            return false;
        return _db.AddingFunds(username, funds);
    }

    #endregion

    #region Diginotes

    public bool ChangeDiginoteValue(string sessionId, float power)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null)
        {
            Console.WriteLine("Null username");
            return false;
        }

        if (waitActiveTransactions != null && !waitActiveTransactions.IsCompleted)
        {
            Console.WriteLine("Wait Active Transactions is not over");
            return false;
        }

        bool success = _db.ChangeDiginoteValue(power);
        Console.WriteLine("Change diginote is {0}", success);
        if (success)
        {
            _db.SetActiveTransactions(false);
            waitActiveTransactions = WaitForAcceptTransactions();
            return true;
        }
        return false;
    }
    
    private async Task WaitForAcceptTransactions()
    {
        await Task.Delay(8000);
        _db.SetActiveTransactions(true);
    }
    
    public float GetDiginoteValue()
    {
        return _db.GetValue();
    }

    public Dictionary<float, int> GetQuotationFlutuation()
    {
        return _db.GetQuotes();
    }

    public List<int> GetDiginotes(string sessionID)
    {
        return _db.GetDiginotes(sessionID);
    }
    #endregion

    #region Transaction

    public List<Transaction> GetMyTransactions(string sessionId, TransactionType type, bool open)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null)
            return null;
        return _db.GetTransactions(type, open, username);
    }

    public List<Transaction> GetOtherTransactions(string sessionId)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null)
            return null;
        return _db.GetOtherTransactions(TransactionType.ALL, true, username);
    }

    public int CheckCompleteTransaction(string sessionId, Transaction transaction, TransactionType type)
    {
        User user = GetUserInformation(sessionId);
        if (user == null)
            return transaction.quantity;

        if(user == null || 
            (user.availableDiginotes < transaction.quantity && type == TransactionType.SELL) || 
            (user.balance < transaction.quantity * GetDiginoteValue() && type == TransactionType.BUY))
        {
            return -1;
        }

        List<Transaction> transactions = _db.GetUnfufilledTransactions(transaction.quantity, type);
        foreach (Transaction t in transactions)
        {
            if(t.seller == null)
            {
                t.seller = user.username;
            }
            else if(t.buyer == null)
            {
                t.buyer = user.username;
            }

            bool success = _db.CompleteTransaction(t, transaction.quantity, type);
            if (success)
            {
                Console.WriteLine("Success completing transaction");
                _db.IncrementQuantity();
                transaction.quantity -= t.quantity;
                if (transaction.quantity <= 0)
                    return 0;
            }
        }

        return transaction.quantity;
    }

    public int InsertTransaction(string sessionId, Transaction transaction, TransactionType type)
    {
        string username = _db.GetUsername(sessionId);
        if (username == null)
            return -1;
        return (int)_db.InsertTransaction(transaction, type);
    }

    public bool ActivateTransaction(string sessionId, bool active)
    {
        string username = _db.GetUsername(sessionId);
        if(username == null)
            return false;
        if (active)
            _db.SetActiveMyTransaction(active, username);
        else
            _db.DeleteMyInactiveTransactions(username);
        return true;
    }

    #endregion
}
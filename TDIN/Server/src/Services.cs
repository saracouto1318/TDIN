using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;

public class Services {
    private static Services _instance;
    private Database.Database _db;

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
        return _db.CheckUser(username, passHash);
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

    #endregion

    #region User

    public User GetUserInformation(string sessionId)
    {
        string username = _db.GetUsernameBySession(sessionId);
        if (username == null)
            return null;
        User user = _db.GetUserInfo(username);
        return user;
    }

    public bool ChangeName(string sessionId, string nName)
    {
        string username = _db.GetUsernameBySession(sessionId);
        if (username == null)
            return false;
        return _db.ChangeName(nName, username);
    }

    public bool ChangeUsername(string sessionId, string nUsername)
    {
        string username = _db.GetUsernameBySession(sessionId);
        if (username == null)
            return false;
        return _db.ChangeUsername(nUsername, username);
    }

    public bool ChangePassword(string sessionId, string password, string nPassword)
    {
        string username = _db.GetUsernameBySession(sessionId);
        if (username == null || !LoginUser(username, password))
            return false;
        string nPassHash = GetHashString(nPassword);
        return _db.ChangePassword(nPassHash, username);
    }

    public bool AddingFunds(string sessionId, float funds)
    {
        string username = _db.GetUsernameBySession(sessionId);
        return _db.AddingFunds(username, funds);
    }

    #endregion

    #region Diginotes

    public bool ChangeDiginoteValue(float power)
    {
        bool success = _db.ChangeDiginoteValue(power);
        if(success)
        {
            //Notify every subscribed client
        }
        return success;
    }

    public float GetDiginoteValue()
    {
        return _db.GetValue();
    }

    #endregion

    #region Transaction

    public List<Transaction> GetMyTransactions(string sessionId)
    {
        string username = _db.GetUsernameBySession(sessionId);
        if (username == null)
            return null;
        return _db.GetTransactions(Database.TransactionType.ALL, true, username);
    }

    public List<Transaction> GetOtherTransactions(string sessionId)
    {
        string username = _db.GetUsernameBySession(sessionId);
        if (username == null)
            return null;
        return _db.GetOtherTransactions(Database.TransactionType.ALL, true, username);
    }

    #endregion
}
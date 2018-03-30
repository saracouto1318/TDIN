using System;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Text;

public class UserAuthenticationService {
    private static UserAuthenticationService instance;

    private UserAuthenticationService() {}

    public static UserAuthenticationService GetInstance() {
        if(instance == null)
            instance = new UserAuthenticationService();
        return instance;
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
        return !Database.Database.GetInstance().UserExists(username);
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
        return Database.Database.GetInstance().CheckUser(username, passHash);
    }

    public bool RegisterUser(string username, string password, string name)
    {
        string passHash = GetHashString(password);
        return Database.Database.GetInstance().InsertUser(name, username, passHash);
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
        Database.Database.GetInstance().DeleteSession(username);
        return Database.Database.GetInstance().InsertSession(username, sessionId);
    }

    #endregion

    #region User

    public User GetUserInformation(string sessionId)
    {
        string username = Database.Database.GetInstance().GetUsernameBySession(sessionId);
        if (username == null)
            return null;
        User user = Database.Database.GetInstance().GetUserInfo(username);
        return user;
    }

    #endregion

}
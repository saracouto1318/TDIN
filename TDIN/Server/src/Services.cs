using System;
using System.Runtime.Remoting;

public class UserAuthenticationService {
    private static UserAuthenticationService instance;

    private UserAuthenticationService() {}

    public static UserAuthenticationService GetInstance() {
        if(instance == null)
            instance = new UserAuthenticationService();
        return instance;
    }

    public bool IsValidUsername(string username) {
        // Check username against db entries
        return true;
    }
    
    public bool IsValidPassword(string password) {
        // Regex checker for password
        return true;
    }

    public bool IsValidRegister(string username, string password) {
        return IsValidUsername(username) && IsValidPassword(password);
    }

    public bool LoginUser(string username, string password) {
        return Database.Database.GetInstance().CheckUser(username, password);
    }

    public bool RegisterUser(string username, string password, string name) {
        if(!IsValidRegister(username, password))
            return false;

        return Database.Database.GetInstance().InsertUser(name, username, password);
    }

    public UserSession StoreSession(string username) {
        string userSession = System.Guid.NewGuid().ToString();
        if (!StoreSession(username, userSession))
            return null;
        return new UserSession(username, userSession);
    }

    private bool StoreSession(string username, string sessionId) {
        Database.Database.GetInstance().DeleteSession(username);
        return Database.Database.GetInstance().InsertSession(username, sessionId);
    }

    public User GetUserInformation(string sessionId) {
        string username = Database.Database.GetInstance().GetUsernameBySession(sessionId);
        if (username == null)
            return null;
        User user = Database.Database.GetInstance().GetUserInfo(username);
        return user;
    }
}
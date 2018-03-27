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
        //ServiceDao dao = ServiceDao.GetInstance();
        //return dao.LoginUser(username, password);;
        //return Database.Database.GetInstance().LoginUser();

        if(!Database.Database.GetInstance().CheckUser(username, password))
            return false;

        return true;
    }

    public bool RegisterUser(string username, string password, string name) {
        if(!IsValidRegister(username, password))
            return false;

        if (Database.Database.GetInstance().CheckUser(username, password))
            return false;

        return Database.Database.GetInstance().InsertUser(name, username, password);
    }

    public string StoreSession(string username) {
        string userSession = System.Guid.NewGuid().ToString();

        StoreSession(username, userSession);
        return userSession;
    }

    public void StoreSession(string username, string sessionId) {
        ServiceDao dao = ServiceDao.GetInstance();
        dao.StoreSession(username, sessionId);
    }

    public User GetUserInformation(string sessionId) {
        ServiceDao dao = ServiceDao.GetInstance();
        return dao.GetUserInformation(sessionId);
    }
}
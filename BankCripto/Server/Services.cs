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
        // Check login pair against db entries
        // Don't forget to hash it
        return true;
    }

    public bool RegisterUser(string username, string password) {
        if(!IsValidRegister(username, password))
            return false;
        
        // Insert data into the db
        return true;
    }

    public string StoreSession(string username) {
        string userSession = System.Guid.NewGuid().ToString();
        // Store session
        StoreSession(username, userSession);
        return userSession;
    }

    public void StoreSession(string username, string sessionid) {
        // Delete all session with username equal to the one given
        // Insert session into the db
    }
}
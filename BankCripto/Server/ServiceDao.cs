using System;
using System.Collections.Generic;

public class ServiceDao {
    private static ServiceDao instance;

    private List<User> registeredUsers;
    private List<UserSession> authenticatedUsers;

    private ServiceDao() {}

    public static ServiceDao GetInstance() {
        if(instance == null)
            instance = new ServiceDao();
        return instance;
    }

    public bool RegisterUser(string username, string password, string name) {
        // Hash password
        // Store new user in the db
        return false;
    }

    public bool LoginUser(string username, string password) {
        // Check login pair against db entries
        // Don't forget to hash it
        return false;
    }

    public void StoreSession(string username, string sessionId) {
        // Delete all session with username equal to the one given
        // Insert session into the db
    }

    public User GetUserInformation(string sessinId) {
        // Get username with matching sessionId
        // Get user's information
        return null;
    }
}
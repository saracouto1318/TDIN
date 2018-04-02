﻿using System;
using System.Runtime.Remoting;

public class Server {
    static void Main() {
        Console.WriteLine("Server starting soon");
        // Initialize services
        Services.GetInstance();
        // Start remoting
        RemotingConfiguration.Configure("Server.exe.config", false);

        while(true)
        {
            Console.Write("Diginote:");
            string line = Console.ReadLine();
            if (float.TryParse(line, out float nValue) && nValue > 0f)
            {
                bool success = Services.GetInstance().ChangeDiginoteValue(nValue);
                Console.Write("Setting value to {0} with {1}", nValue, success ? "success" : "failure");
            }
            Console.WriteLine("Diginote Value: {0}", Services.GetInstance().GetDiginoteValue());
        }
    }

    static void SetDiginoteValue()
    {
        Services services = Services.GetInstance();
        while (true)
        {
            Console.Write("Diginote: ");
            string line = Console.ReadLine();
            if (float.TryParse(line, out float nValue) && nValue > 0f)
            {
                bool success = services.ChangeDiginoteValue(nValue);
                Console.Write("{1} setting value to {0}", nValue, success ? "success" : "failure");
            }
            Console.WriteLine("Diginote Value: {0}", services.GetDiginoteValue());
        }
    }
}
public class AuthenticationObj : MarshalByRefObject, IUser
{
    public string Hello()
    {
        return "Hello user :)";
    }

    public UserSession Login(string username, string password)
    {
        // Authenticate params
        Services authService =
            Services.GetInstance();
        bool isValid = authService.LoginUser(username, password);
        
        if (!isValid)
            return null;

        // Store session
        UserSession userSession = authService.StoreSession(username);
        return userSession;
    }

    public bool IsUsernameAvailable(string username)
    {
        // Validate username
        Services authService =
            Services.GetInstance();
        bool isValid = authService.IsUsernameAvailable(username);

        return isValid;
    }

    public UserSession Register(string username, string password, string name)
    {
        // Valdiate params
        Services authService =
            Services.GetInstance();
        bool isValid = authService.RegisterUser(username, password, name);
        
        if (!isValid)
            return null;

        // Store session
        UserSession userSession = authService.StoreSession(username);
        return userSession;
    }

    public User UserInformation(string sessionId)
    {
        Services authService =
            Services.GetInstance();
        return authService.GetUserInformation(sessionId);
    }

    public bool ChangeUsername(string sessionId, string nUsername)
    {
        Services authService =
            Services.GetInstance();
        return authService.ChangeUsername(sessionId, nUsername);
    }

    public bool ChangeName(string sessionId, string nName)
    {
        Services authService =
            Services.GetInstance();
        return authService.ChangeName(sessionId, nName);
    }

    public bool ChangePassowrd(string sessionId, string password, string nPassword)
    {
        Services authService =
            Services.GetInstance();
        return authService.ChangePassword(sessionId, password, nPassword);
    }
}

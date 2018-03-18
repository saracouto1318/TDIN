using System;
using System.Runtime.Remoting;

class Server
{
    static void Main()
    {
        Console.WriteLine("Server starting soon");
        RemotingConfiguration.Configure("Server.exe.config", false);
        Console.WriteLine("Press return to exit");
        Console.ReadLine();
    }
}

class AuthenticationObj : MarshalByRefObject, IUser
{
    public string Hello()
    {
        return "Hello user :)";
    }

    public string Login(string username, string password) {
        Console.WriteLine("Username {0}", username);
        Console.WriteLine("Password {0}", password);

        // Authenticate params
        UserAuthenticationService authService = UserAuthenticationService.GetInstance();
        bool isValid = authService.LoginUser(username, password);

        if(!isValid)
            return null;

        // Store session
        string userSession = authService.StoreSession(username);
        return userSession;
    }
    
    public bool IsValidUsername(string username) {
        Console.WriteLine("Username {0}", username);

        // Validate username
        UserAuthenticationService authService = UserAuthenticationService.GetInstance();
        bool isValid = authService.IsValidUsername(username);

        return isValid;
    }

    public string Register(string username, string password, string name) {
        Console.WriteLine("Username {0}", username);
        Console.WriteLine("Password {0}", password);

        // Valdiate params
        UserAuthenticationService authService = UserAuthenticationService.GetInstance();
        bool isValid = authService.RegisterUser(username, password);

        if(!isValid)
            return null;
        
        // Store session
        string userSession = authService.StoreSession(username);
        return userSession;
    }
}

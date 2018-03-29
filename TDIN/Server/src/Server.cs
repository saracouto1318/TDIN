using System;
using System.Runtime.Remoting;

public class Server {
    static void Main() {
        Console.WriteLine("Server starting soon");
        RemotingConfiguration.Configure("Server.exe.config", false);
        Console.WriteLine("Press return to exit");
        Console.ReadLine();
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
        Console.WriteLine("Username {0}", username);
        Console.WriteLine("Password {0}", password);

        // Authenticate params
        UserAuthenticationService authService =
            UserAuthenticationService.GetInstance();
        bool isValid = authService.LoginUser(username, password);

        Console.WriteLine("Valid login: {0}", isValid);

        if (!isValid)
            return null;

        // Store session
        UserSession userSession = authService.StoreSession(username);
        if (userSession == null)
            Console.WriteLine("User session null");
        else
            Console.WriteLine("User session {0} {1}", userSession.sessionId, userSession.username);
        return userSession;
    }

    public bool IsValidUsername(string username)
    {
        Console.WriteLine("Username {0}", username);

        // Validate username
        UserAuthenticationService authService =
            UserAuthenticationService.GetInstance();
        bool isValid = authService.IsValidUsername(username);

        return isValid;
    }

    public UserSession Register(string username, string password, string name)
    {
        Console.WriteLine("Username {0}", username);
        Console.WriteLine("Password {0}", password);

        // Valdiate params
        UserAuthenticationService authService =
            UserAuthenticationService.GetInstance();
        bool isValid = authService.RegisterUser(username, password, name);

        Console.WriteLine("Valid register: {0}", isValid);

        if (!isValid)
            return null;

        // Store session
        UserSession userSession = authService.StoreSession(username);
        if (userSession == null)
            Console.WriteLine("User session null");
        else
            Console.WriteLine("User session {0} {1}", userSession.sessionId, userSession.username);
        return userSession;
    }

    public User UserInformation(string sessionId)
    {
        return new User("Username1", "Password1", "Name1", 0, 0);
    }
}

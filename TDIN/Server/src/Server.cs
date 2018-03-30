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
        // Authenticate params
        UserAuthenticationService authService =
            UserAuthenticationService.GetInstance();
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
        UserAuthenticationService authService =
            UserAuthenticationService.GetInstance();
        bool isValid = authService.IsUsernameAvailable(username);

        return isValid;
    }

    public UserSession Register(string username, string password, string name)
    {
        // Valdiate params
        UserAuthenticationService authService =
            UserAuthenticationService.GetInstance();
        bool isValid = authService.RegisterUser(username, password, name);
        
        if (!isValid)
            return null;

        // Store session
        UserSession userSession = authService.StoreSession(username);
        return userSession;
    }

    public User UserInformation(string sessionId)
    {
        return new User("Username1", "Password1", "Name1", 0, 0);
    }
}

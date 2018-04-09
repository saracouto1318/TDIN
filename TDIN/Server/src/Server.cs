using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

public class Server {
    static void Main() {
        Console.WriteLine("Server starting soon");
        // Initialize services
        Services.GetInstance();
        // Start remoting
        RemotingConfiguration.Configure("Server.exe.config", false);
        Console.WriteLine("Press any key to exit...");
        Console.Read();
    }
}
public class UserManager : MarshalByRefObject, IUser
{
    public override object InitializeLifetimeService()
    {
        return null;
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
        Services service =
            Services.GetInstance();
        bool isValid = service.RegisterUser(username, password, name);
        
        if (!isValid)
            return null;

        // Store session
        UserSession userSession = service.StoreSession(username);
        return userSession;
    }

    public User UserInformation(string sessionId)
    {
        Services service =
            Services.GetInstance();
        return service.GetUserInformation(sessionId);
    }

    public bool ChangeUsername(string sessionId, string nUsername)
    {
        Services service =
            Services.GetInstance();
        return service.ChangeUsername(sessionId, nUsername);
    }

    public bool ChangeName(string sessionId, string nName)
    {
        Services service =
            Services.GetInstance();
        return service.ChangeName(sessionId, nName);
    }

    public bool ChangePassowrd(string sessionId, string password, string nPassword)
    {
        Services service =
            Services.GetInstance();
        return service.ChangePassword(sessionId, password, nPassword);
    }

    public bool AddingFunds(string sessionId, float funds)
    {
        return Services.GetInstance().AddingFunds(sessionId, funds);
    }

    public List<int> GetDiginotes(string sessionID)
    {
        return Services.GetInstance().GetDiginotes(sessionID);
    }
}

public class TransactionManager : MarshalByRefObject, ITransaction
{
    public event ValueHandler UpdatePower;

    public override object InitializeLifetimeService()
    {
        return null;
    }

    public float GetPower()
    {
        return Services.GetInstance().GetDiginoteValue();
    }

    public void SetPower(string sessionId, float power)
    {
        Console.WriteLine("Power change to {0}", power);
        if (power <= 0)
        {
            return;
        }
        if (Services.GetInstance().ChangeDiginoteValue(sessionId, power))
        {
            UpdatePower(power);
        }
    }

    public List<Transaction> GetMyTransactions(string sessionId, TransactionType type, bool open)
    {
        return Services.GetInstance().GetMyTransactions(sessionId, type, open);
    }

    public List<Transaction> GetOtherTransactions(string sessionId)
    {
        return Services.GetInstance().GetOtherTransactions(sessionId);
    }

    public int CheckCompleteTransaction(string sessionId, Transaction transaction, TransactionType type)
    {
        return Services.GetInstance().CheckCompleteTransaction(sessionId, transaction, type);
    }

    public int InsertTransaction(string sessionId, Transaction transaction, TransactionType type)
    {
        return Services.GetInstance().InsertTransaction(sessionId, transaction, type);
    }

    public Dictionary<float, int> GetQuotationFlutuation()
    {
        return Services.GetInstance().GetQuotationFlutuation();
    }

    public bool ActivateTransation(string sessionId, bool activate)
    {
        return Services.GetInstance().ActivateTransaction(sessionId, activate);
    }
}
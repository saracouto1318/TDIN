using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

public delegate void ValueHandler(float value);

public interface IUser
{
    UserSession Login(string username, string password);
    bool IsUsernameAvailable(string username);
    UserSession Register(string username, string password, string name);
    User UserInformation(string sessionId);
    bool ChangeUsername(string sessionId, string nUsername);
    bool ChangeName(string sessionId, string nName);
    bool ChangePassowrd(string sessionId, string password, string nPassword);
    bool AddingFunds(string sessionId, float funds);

}

public interface ITransaction
{
    event ValueHandler UpdatePower;
    
    float GetPower();
    List<Transaction> GetMyTransactions(string sessionId);
    List<Transaction> GetOtherTransactions(string sessionId);
    int CheckCompleteBuyTransaction(string sessionId, Transaction transaction);
    int CheckCompleteSellTransaction(string sessionId, Transaction transaction);
    bool InsertBuyTransaction(string sessionId, Transaction transaction);
    bool InsertSellTransaction(string sessionId, Transaction transaction);
}

public class Intermediate : MarshalByRefObject
{
    public event ValueHandler UpdatePower;

    public void FireUpdatePower(float power)
    {
        UpdatePower(power);
    }

    public override object InitializeLifetimeService()
    {
        return null;
    }
}
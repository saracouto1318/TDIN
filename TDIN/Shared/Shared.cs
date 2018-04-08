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
    void SetPower(float power);
    Dictionary<float, int> GetQuotationFlutuation();
    List<Transaction> GetMyTransactions(string sessionId, TransactionType type, bool open);
    List<Transaction> GetOtherTransactions(string sessionId);
    int CheckCompleteTransaction(string sessionId, Transaction transaction, TransactionType type);
    int InsertTransaction(string sessionId, Transaction transaction, TransactionType type);
    bool ActivateTransation(string sessionId, bool activate);
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
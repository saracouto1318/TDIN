using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

public delegate void ValueHandler(float value);
public delegate void SellingHandler(int id, string username, float price, int quantity);
public delegate void BuyingHandler(int id, string username, float price, int quantity);
public delegate void CompleteTransactionHandler(int id);

public interface IUser
{
    UserSession Login(string username, string password);
    bool IsUsernameAvailable(string username);
    UserSession Register(string username, string password, string name);
    User UserInformation(string sessionId);
    bool ChangeUsername(string sessionId, string nUsername);
    bool ChangeName(string sessionId, string nName);
    bool ChangePassowrd(string sessionId, string password, string nPassword);

}

public interface ITransaction
{
    event ValueHandler UpdatePower;
    event SellingHandler NewSellTransaction;
    event BuyingHandler NewBuyTransaction;
    event CompleteTransactionHandler CompleteTransaction;
    
    float GetPower();
    List<Transaction> GetMyTransactions(string sessionId);
    List<Transaction> GetOtherTransactions(string sessionId);
}

public class Intermediate : MarshalByRefObject
{
    public event ValueHandler UpdatePower;
    public event SellingHandler NewSellTransaction;
    public event BuyingHandler NewBuyTransaction;
    public event CompleteTransactionHandler CompleteTransaction;

    public void FireUpdatePower(float power)
    {
        UpdatePower(power);
    }

    public void FireNewSellTransaction(int id, string username, float price, int quantity)
    {
        NewSellTransaction(id, username, price, quantity);
    }

    public void FireNewBuyTransaction(int id, string username, float price, int quantity)
    {
        NewBuyTransaction(id, username, price, quantity);
    }

    public void FireCompleteTransaction(int id)
    {
        CompleteTransaction(id);
    }

    public override object InitializeLifetimeService()
    {
        return null;
    }
}
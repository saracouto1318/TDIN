using System;
using System.Runtime.Remoting;

public class User : MarshalByRefObject {
    public string username;
    public string password;
    public string name;
    public int availableDiginotes;
    public int totalDiginotes;
    public float balance;

    public User()
    {
        name = "";
        username = "";
        password = "";
        availableDiginotes = 0;
        totalDiginotes = 0;
        balance = 0;
    }
}

public class UserSession : MarshalByRefObject {
    public string username;
    public string sessionId;

    public UserSession(string username, string sessionId) {
        this.username = username;
        this.sessionId = sessionId;
    }
}

public enum TransactionType { ALL, SELL, BUY };

public class Transaction : MarshalByRefObject
{
    public int ID;
    public int quantity;
    public string buyer;
    public string seller;
    public DateTime date;
    public float quotation;

    public Transaction()
    {
        ID = 0;
        quantity = 0;
        buyer = null;
        seller = null;
        quotation = -1f;
    }
}
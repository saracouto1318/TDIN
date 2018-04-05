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

    public User(string username, string password, string name, int availableDiginotes, int totalDiginotes, int numTransactions, float balance) {
        this.name = name;
        this.username = username;
        this.password = password;
        this.availableDiginotes = availableDiginotes;
        this.totalDiginotes = totalDiginotes;
        this.balance = balance;
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

public class Transaction : MarshalByRefObject
{
    public int ID;
    public int quantity;
    public float value;
    public string buyer;
    public string seller;
    public DateTime date;

    public Transaction()
    {
        ID = 0;
        quantity = 0;
        value = 0;
        buyer = "";
        seller = "";
    }

    public Transaction(int iD, string buyer, string seller, float value, int quantity, DateTime date)
    {
        ID = iD;
        this.quantity = quantity;
        this.value = value;
        this.buyer = buyer;
        this.seller = seller;
        this.date = date;
    }
}
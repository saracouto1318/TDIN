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
    public string buyer;
    public string seller;
    public DateTime date;

    public Transaction()
    {
        ID = 0;
        quantity = 0;
        buyer = null;
        seller = null;
    }

    public Transaction(int ID, string buyer, string seller, int quantity, DateTime date)
    {
        this.ID = ID;
        this.quantity = quantity;
        this.buyer = buyer;
        this.seller = seller;
        this.date = date;
    }
}
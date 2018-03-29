using System;
using System.Runtime.Remoting;

public class User : MarshalByRefObject {
    public string username;
    public string password;
    public string name;
    public int numDiginotes;
    public int numTransactions;

    public User()
    {
        name = "";
        username = "";
        password = "";
        numDiginotes = 0;
        numTransactions = 0;
    }

    public User(string username, string password, string name, int numDiginotes, int numTransactions) {
        this.name = name;
        this.username = username;
        this.password = password;
        this.numDiginotes = numDiginotes;
        this.numTransactions = numTransactions;
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
    public double value;
    public string buyer;
    public string seller;

    public Transaction()
    {
        ID = 0;
        quantity = 0;
        value = 0;
        buyer = "";
        seller = "";
    }

    public Transaction(int iD, int quantity, double value, string buyer, string seller)
    {
        ID = iD;
        this.quantity = quantity;
        this.value = value;
        this.buyer = buyer;
        this.seller = seller;
    }
}
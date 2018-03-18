using System;
using System.Runtime.Remoting;

public class User : MarshalByRefObject {
    public string username;
    public string password;
    public string name;

    public User(string username, string password, string name) {
        this.name = name;
        this.username = username;
        this.password = password;
    }
}
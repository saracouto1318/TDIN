using System;
using System.Runtime.Remoting;

class Client {
    static void Main(string[] args) {
        Console.WriteLine("Client starting soon");
        RemotingConfiguration.Configure("Client.exe.config", false);
        AuthenticationObj obj = new AuthenticationObj();

        Console.WriteLine(obj.Hello());
        
        Console.WriteLine("Name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Username: ");
        string username = Console.ReadLine();
        Console.WriteLine("Password: ");
        string password = Console.ReadLine();

        // Validate username
        Console.WriteLine(obj.IsValidUsername(username));
        // Register user
        Console.WriteLine(obj.Register(username, password, name));
        // Login user
        Console.WriteLine(obj.Login(username, password));

        // Get user info
        User user = obj.UserInformation("id");
        Console.WriteLine("{0}, {1}, name {2}", user.username, user.password, user.name);
    }
}

class AuthenticationObj : MarshalByRefObject, IUser {
    public string Hello() {
        return null;
    }
        
    public string Login(string username, string password) {
        return null;
    }
    
    public bool IsValidUsername(string username) {
        return false;
    }

    public string Register(string username, string password, string name) {
        return null;
    }

    public User UserInformation(string sessionId) {
        return null;
    }
}

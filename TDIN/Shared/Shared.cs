using System;
using System.Runtime.Remoting;

public interface IUser
{
    UserSession Login(string username, string password);
    bool IsUsernameAvailable(string username);
    UserSession Register(string username, string password, string name);
    User UserInformation(string sessionId);
}

public interface IDiginotes
{
    
}

public interface ITransactions
{
    
}
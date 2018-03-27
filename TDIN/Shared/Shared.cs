using System;
using System.Runtime.Remoting;

public interface IUser
{
    string Login(string username, string password);
    bool IsValidUsername(string username);
    string Register(string username, string password, string name);
    User UserInformation(string sessionId);
}

public interface IDiginotes
{
    
}

public interface ITransactions
{
    
}
using System;
using System.Runtime.Remoting;

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

public interface IDiginotes
{
    
}

public interface ITransactions
{
    
}
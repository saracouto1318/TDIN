using System;
using System.Runtime.Remoting;

class Client
{
    static void Main(string[] args)
    {
        Console.WriteLine("Client starting soon");
        RemotingConfiguration.Configure("Client.exe.config", false);
        RemObj obj = new RemObj();
        Console.WriteLine(obj.Hello());
    }
}

class RemObj : MarshalByRefObject
{
    public RemObj()
    {
        Console.WriteLine("Constructor called");
    }

    public string Hello()
    {
        return null;
    }
}

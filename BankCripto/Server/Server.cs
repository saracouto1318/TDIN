using System;
using System.Runtime.Remoting;

class Server
{
    static void Main()
    {
        Console.WriteLine("Server starting soon");
        RemotingConfiguration.Configure("Server.exe.config", false);
        Console.WriteLine("Press return to exit");
        Console.ReadLine();
    }
}

class RemObj : MarshalByRefObject
{
    public string Hello()
    {
        return "Hello user :)";
    }
}

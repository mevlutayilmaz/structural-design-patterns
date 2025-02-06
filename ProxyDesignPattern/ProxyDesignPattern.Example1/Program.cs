
Console.WriteLine("Client: Executing the client code with a real subject:");
RealSubject realSubject = new();
realSubject.Request();

Console.WriteLine();

Console.WriteLine("Client: Executing the same client code with a proxy:");
Proxy proxy = new(realSubject);
proxy.Request();

public interface ISubject
{
    void Request();
}

public class RealSubject : ISubject
{
    public void Request()
        => Console.WriteLine("RealSubject: Handling Request.");
}

public class Proxy : ISubject
{
    private RealSubject _realSubject;

    public Proxy(RealSubject realSubject)
    {
        _realSubject = realSubject;
    }

    public void Request()
    {
        if (CheckAccess())
        {
            _realSubject.Request();
            LogAccess();
        }
    }

    public bool CheckAccess()
    {
        Console.WriteLine("Proxy: Checking access prior to firing a real request.");
        return true;
    }

    public void LogAccess()
        => Console.WriteLine("Proxy: Logging the time of request.");
}
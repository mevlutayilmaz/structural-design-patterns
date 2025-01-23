Adaptee adaptee = new();
ITarget target = new Adapter(adaptee);

Console.WriteLine(target.GetRequest());

public interface ITarget
{
    string GetRequest();
}

class Adaptee
{
    public string GetSpecificRequest()
    {
        return "Specific request.";
    }
}

class Adapter : ITarget
{
    private readonly Adaptee _adaptee;

    public Adapter(Adaptee adaptee)
    {
        _adaptee = adaptee;
    }

    public string GetRequest()
    {
        return $"This is '{_adaptee.GetSpecificRequest()}'";
    }
}
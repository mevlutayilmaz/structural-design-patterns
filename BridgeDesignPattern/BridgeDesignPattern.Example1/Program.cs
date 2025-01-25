
RefinedAbstraction abstraction1 = new(new ConcreteImplementotA());
RefinedAbstraction abstraction2 = new(new ConcreteImplementotB());

Console.WriteLine(abstraction1.Operation());
Console.WriteLine(abstraction2.Operation());

public abstract class Abstraction
{
    protected IImplementor _implementor;

    public Abstraction(IImplementor implementor)
    {
        _implementor = implementor;
    }

    public abstract string Operation();
}

public class RefinedAbstraction : Abstraction
{
    public RefinedAbstraction(IImplementor implementor) : base(implementor)
    {
    }

    public override string Operation()
        => "Abstract: Base operation with:\n" + _implementor.OperationImplementor();
}

public interface IImplementor
{
    string OperationImplementor();
}

public class ConcreteImplementotA : IImplementor
{
    public string OperationImplementor()
        => "ConcreteImplementationA: The result in platform A.\n";
}

public class ConcreteImplementotB : IImplementor
{
    public string OperationImplementor()
        => "ConcreteImplementationB: The result in platform B.\n";
}
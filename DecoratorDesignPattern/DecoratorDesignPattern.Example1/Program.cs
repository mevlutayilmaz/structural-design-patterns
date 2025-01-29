ConcreteComponent simple = new();
Console.WriteLine(simple.Operation());

ConcreteDecoratorA decorator1 = new(simple);
ConcreteDecoratorB decorator2 = new(decorator1);
Console.WriteLine(decorator2.Operation());

public abstract class Component
{
    public abstract string Operation();
}

public class ConcreteComponent : Component
{
    public override string Operation()
        => "ConcreteComponent";
}

public abstract class Decorator : Component
{
    protected Component _component;

    public Decorator(Component component)
    {
        _component = component;
    }

    public void SetComponent(Component component)
        => _component = component;

    public override string Operation()
        => _component != null ? _component.Operation() : string.Empty;
}

public class ConcreteDecoratorA : Decorator
{
    public ConcreteDecoratorA(Component component) : base(component)
    {
    }

    public override string Operation()
        => $"ConcreteDecoratorA({base.Operation()})";
}
public class ConcreteDecoratorB : Decorator
{
    public ConcreteDecoratorB(Component component) : base(component)
    {
    }

    public override string Operation()
        => $"ConcreteDecoratorB({base.Operation()})";
}
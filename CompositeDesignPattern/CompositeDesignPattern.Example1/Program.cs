Leaf leaf1 = new() { Count = 1 };
Leaf leaf2 = new() { Count = 2 };
Leaf leaf3 = new() { Count = 3 };

Composite composite1 = new() { Count = 1 };
composite1.Add(leaf1);

Composite composite2 = new() { Count = 2 };
composite2.Add(leaf1);
composite2.Add(leaf2);
composite2.Add(composite1);

Composite composite3 = new() { Count = 3 };
composite3.Add(leaf1);
composite3.Add(leaf2);
composite3.Add(leaf3);
composite3.Add(composite2);

Console.WriteLine(composite1.Operation());
Console.WriteLine(composite2.Operation());
Console.WriteLine(composite3.Operation());

Console.WriteLine($"{nameof(Leaf)}: {leaf1.IsComponent()}");
Console.WriteLine($"{nameof(Composite)}: {composite1.IsComponent()}");

public abstract class Component
{
    public int Count { get; set; }
    public abstract string Operation();
    public virtual void Add(Component component)
        => throw new NotImplementedException();

    public virtual void Remove(Component component)
        => throw new NotImplementedException();

    public virtual bool IsComponent()
        => true;
}

public class Leaf : Component
{
    public override string Operation()
        => "Leaf" + Count;

    public override bool IsComponent()
        => false;
}

public class Composite : Component
{
    protected List<Component> _children = new();

    public override void Add(Component component)
        => _children.Add(component);

    public override void Remove(Component component)
        => _children.Remove(component);

    public override string Operation()
    {
        int i = 0;
        string result = $"Branch{Count}(";

        foreach (Component component in _children)
        {
            result += component.Operation();
            if (i != _children.Count - 1)
            {
                result += "+";
            }
            i++;
        }

        return result + ")";
    }
}
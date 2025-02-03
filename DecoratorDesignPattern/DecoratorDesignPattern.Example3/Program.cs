
ICoffee simpleCoffee = new SimpleCoffee();
Console.WriteLine($"{simpleCoffee.GetDescription()} - Cost: {simpleCoffee.GetCost()} $");

ICoffee milkCoffee = new MilkDecorator(simpleCoffee);
Console.WriteLine($"{milkCoffee.GetDescription()} - Cost: {milkCoffee.GetCost()} $");

ICoffee sugarMilkCoffee = new SugarDecorator(milkCoffee);
Console.WriteLine($"{sugarMilkCoffee.GetDescription()} - Cost: {sugarMilkCoffee.GetCost()} $");

ICoffee chocolateEspresso = new ChocolateDecorator(new Espresso());
Console.WriteLine($"{chocolateEspresso.GetDescription()} - Cost: {chocolateEspresso.GetCost()} $");

ICoffee complexCoffee = new SugarDecorator(new MilkDecorator(new ChocolateDecorator(new Espresso())));
Console.WriteLine($"{complexCoffee.GetDescription()} - Cost: {complexCoffee.GetCost()} $");

#region Component
public interface ICoffee
{
    double GetCost();
    string GetDescription();
}
#endregion

#region Concrete Component
public class SimpleCoffee : ICoffee
{
    public double GetCost()
        => 5;

    public string GetDescription()
        => "Simple Coffee";
}

public class Espresso : ICoffee
{
    public double GetCost() => 8;

    public string GetDescription() => "Espresso";
}
#endregion

#region Decorator
public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee _coffee;

    public CoffeeDecorator(ICoffee coffee)
    {
        _coffee = coffee;
    }

    public virtual double GetCost()
        => _coffee.GetCost();

    public virtual string GetDescription()
        => _coffee.GetDescription();
}
#endregion

#region Concrete Decorator
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee) { }

    public override double GetCost()
        => base.GetCost() + 2;

    public override string GetDescription()
        => base.GetDescription() + ", Milky";
}

public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee) { }

    public override double GetCost()
        => base.GetCost() + 1;

    public override string GetDescription()
        => base.GetDescription() + ", Sugary";
}

public class ChocolateDecorator : CoffeeDecorator
{
    public ChocolateDecorator(ICoffee coffee) : base(coffee) { }

    public override double GetCost()
        => base.GetCost() + 3;

    public override string GetDescription()
        => base.GetDescription() + ", Chocolate";
}
#endregion
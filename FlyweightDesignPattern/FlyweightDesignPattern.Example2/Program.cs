
UnitFlyweightFactory factory = new();

var soldier1 = factory.GetUnit("Soldier");
var soldier2 = factory.GetUnit("Soldier");
var archer1 = factory.GetUnit("Archer");
var cavalry1 = factory.GetUnit("Cavalry");
var soldier3 = factory.GetUnit("Soldier");

soldier1.Draw(10, 5);
soldier2.Draw(12, 7);
archer1.Draw(8, 3);
cavalry1.Draw(15, 2);
soldier3.Draw(11, 6);

Console.WriteLine($"Number of Unit Types Created: {factory.GetCount()}");

public class Unit
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Speed { get; set; }
}

#region Flyweight
public class UnitFlyweight
{
    protected Unit _unit;

    public UnitFlyweight(Unit unit)
    {
        _unit = unit;
    }

    public void Draw(int x, int y)
        => Console.WriteLine($"{_unit.Name} is being drawn. Location: ({x}, {y}), Health: {_unit.Health}, Attack: {_unit.Attack}, Defense: {_unit.Defense}, Speed: {_unit.Speed}");
}
#endregion

#region Flyweight Factory
public class UnitFlyweightFactory
{
    private Dictionary<string, UnitFlyweight> _units = new();

    public UnitFlyweightFactory(params Unit[] args)
    {
        foreach (var unit in args)
        {
            _units[unit.Name] = new UnitFlyweight(unit);
        }
    }

    public UnitFlyweight GetUnit(string unitType)
    {
        if (_units.ContainsKey(unitType))
        {
            return _units[unitType];
        }

        UnitFlyweight unit = null;

        switch (unitType)
        {
            case "Soldier":
                unit = new UnitFlyweight(new() { Name = "Soldier", Health = 100, Attack = 20, Defense = 10, Speed = 5 });
                break;
            case "Archer":
                unit = new UnitFlyweight(new() { Name = "Archer", Health = 80, Attack = 30, Defense = 5, Speed = 7 });
                break;
            case "Cavalry":
                unit = new UnitFlyweight(new() { Name = "Cavalry", Health = 120, Attack = 25, Defense = 15, Speed = 10 });
                break;
            default:
                throw new ArgumentException("Invalid unit type: " + unitType);
        }

        _units[unitType] = unit;
        return unit;
    }

    public int GetCount()
        => _units.Count;
}
#endregion
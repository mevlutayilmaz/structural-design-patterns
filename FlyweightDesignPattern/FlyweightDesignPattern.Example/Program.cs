using System.Text.Json;

var factory = new FlyweightFactory(
    new Car { Company = "Chevrolet", Model = "Camaro2018", Color = "pink" },
    new Car { Company = "Mercedes Benz", Model = "C300", Color = "black" },
    new Car { Company = "Mercedes Benz", Model = "C500", Color = "red" },
    new Car { Company = "BMW", Model = "M5", Color = "red" },
    new Car { Company = "BMW", Model = "X6", Color = "white" }
    );
factory.listFlyweights();

addCarToPoliceDatabase(factory, new Car
{
    Number = "CL234IR",
    Owner = "James Doe",
    Company = "BMW",
    Model = "M5",
    Color = "red"
});

addCarToPoliceDatabase(factory, new Car
{
    Number = "CL234IR",
    Owner = "James Doe",
    Company = "BMW",
    Model = "X1",
    Color = "red"
});

factory.listFlyweights();

void addCarToPoliceDatabase(FlyweightFactory factory, Car car)
{
    Console.WriteLine("\nClient: Adding a car to database.");

    var flyweight = factory.GetFlyweight(new Car
    {
        Color = car.Color,
        Model = car.Model,
        Company = car.Company
    });

    flyweight.Operation(car);
}

public class Car
{
    public string Owner { get; set; }
    public string Number { get; set; }
    public string Company { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
}

public class Flyweight
{
    protected Car _sharedState;

    public Flyweight(Car sharedState)
    {
        _sharedState = sharedState;
    }

    public void Operation(Car uniqueState)
    {
        string s = JsonSerializer.Serialize(_sharedState);
        string u = JsonSerializer.Serialize(uniqueState);
        Console.WriteLine($"Flyweight: Displaying shared {s} and unique {u} state.");
    }
}

public class FlyweightFactory
{
    private List<Tuple<Flyweight, string>> flyweights = new();

    public FlyweightFactory(params Car[] args)
    {
        foreach (var elem in args)
        {
            flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(elem), getKey(elem)));
        }
    }

    public string getKey(Car key)
    {
        List<string> elements = new();

        elements.Add(key.Model);
        elements.Add(key.Color);
        elements.Add(key.Company);

        if (key.Owner != null && key.Number != null)
        {
            elements.Add(key.Number);
            elements.Add(key.Owner);
        }

        elements.Sort();

        return string.Join("_", elements);
    }

    public Flyweight GetFlyweight(Car sharedState)
    {
        string key = getKey(sharedState);

        if (flyweights.Where(t => t.Item2 == key).Count() == 0)
        {
            Console.WriteLine("FlyweightFactory: Can't find a flyweight, creating new one.");
            flyweights.Add(new Tuple<Flyweight, string>(new Flyweight(sharedState), key));
        }
        else
        {
            Console.WriteLine("FlyweightFactory: Reusing existing flyweight.");
        }
        return flyweights.Where(t => t.Item2 == key).FirstOrDefault().Item1;
    }

    public void listFlyweights()
    {
        var count = flyweights.Count;
        Console.WriteLine($"\nFlyweightFactory: I have {count} flyweights:");
        foreach (var flyweight in flyweights)
        {
            Console.WriteLine(flyweight.Item2);
        }
    }
}
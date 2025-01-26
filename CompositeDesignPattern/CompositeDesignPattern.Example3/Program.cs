
Product product1 = new() { Name = "iPhone 13", Price = 25000 };
Product product2 = new() { Name = "Samsung Galaxy S21", Price = 20000 };
Product product3 = new() { Name = "MacBook Pro", Price = 45000 };
Product product4 = new() { Name = "Dell XPS 13", Price = 40000 };
Product product5 = new() { Name = "Arçelik Buzdolabı", Price = 15000 };
Product product6 = new() { Name = "Bosch Buzdolabı", Price = 17000 };

Category phoneCategory = new() { Name = "Telefon" };
phoneCategory.Add(product1);
phoneCategory.Add(product2);

Category laptopCategory = new() { Name = "Laptop" };
laptopCategory.Add(product3);
laptopCategory.Add(product4);

Category electronicCategory = new() { Name = "Elektronik" };
electronicCategory.Add(phoneCategory);
electronicCategory.Add(laptopCategory);

Category fridgeCategory = new() { Name = "Buzdolabı" };
fridgeCategory.Add(product5);
fridgeCategory.Add(product6);

Category homeAppliancesCategory = new() { Name = "Ev Aletleri" };
homeAppliancesCategory.Add(fridgeCategory);

Category rootCategory = new() { Name = "Ana Kategori" };
rootCategory.Add(electronicCategory);
rootCategory.Add(homeAppliancesCategory);

rootCategory.Display(0);

#region Component
public abstract class CatalogComponent
{
    public string Name { get; set; }
    public abstract void Display(int depth);
    public virtual void Add(CatalogComponent component)
        => throw new NotImplementedException();
    public virtual void Remove(CatalogComponent component)
        => throw new NotImplementedException();
}
#endregion

#region Leaf
public class Product : CatalogComponent
{
    public decimal Price { get; set; }

    public override void Display(int depth)
        => Console.WriteLine($"{new string(' ', depth)}- Product: {Name} {Price}$");
}
#endregion

#region Composite
public class Category : CatalogComponent
{
    protected List<CatalogComponent> _children = new();

    public override void Display(int depth)
    {
        Console.WriteLine($"{new string(' ', depth)}- Category: {Name}");
        foreach (CatalogComponent component in _children)
            component.Display(depth + 2);
    }

    public override void Add(CatalogComponent component)
        => _children.Add(component);

    public override void Remove(CatalogComponent component)
        => _children.Remove(component);
}
#endregion
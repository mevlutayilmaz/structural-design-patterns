
OrderFacade orderFacade = new();
bool orderSuccess = orderFacade.PlaceOrder("Laptop", 2, "1234-5678-9012-3456", "Ankara, Türkiye");
Console.WriteLine(orderSuccess ? "The order was created successfully." : "The order could not be created.");

Console.WriteLine("\n--- A different order ---");
bool orderFail = orderFacade.PlaceOrder("Mouse", 150, "9876-5432-1098-7654", "İstanbul, Türkiye");
Console.WriteLine(orderSuccess ? "The order was created successfully." : "The order could not be created.");

#region Facade
public class OrderFacade
{
    protected InventorySystem _inventorySystem;
    protected PaymentSystem _paymentSystem;
    protected ShippingSystem _shippingSystem;

    public OrderFacade()
    {
        _inventorySystem = new InventorySystem();
        _paymentSystem = new PaymentSystem();
        _shippingSystem = new ShippingSystem();
    }

    public bool PlaceOrder(string product, int quantity, string cardNumber, string address)
    {
        Console.WriteLine("The order is being placed...");

        if (!_inventorySystem.CheckStock(product, quantity))
        {
            Console.WriteLine("There are not enough products in stock.");
            return false;
        }


        double amount = 10.0 * quantity;
        if (!_paymentSystem.ProcessPayment(cardNumber, amount))
        {
            Console.WriteLine("Payment failed.");
            return false;
        }

        if (!_shippingSystem.ShipOrder(address, $"{quantity} {product}"))
        {
            Console.WriteLine("Shipping process failed.");
            return false;
        }

        Console.WriteLine("The order was completed successfully.");
        return true;
    }
}
#endregion

#region Subsystem
public class InventorySystem
{
    public bool CheckStock(string product, int quantity)
    {
        Console.WriteLine($"Checking inventory: {product}, Quantity: {quantity}");
        return quantity <= 100;
    }
}

public class PaymentSystem
{
    public bool ProcessPayment(string cardNumber, double amount)
    {
        Console.WriteLine($"Payment is being processed: Card Number: {cardNumber}, Amount: {amount}");
        return true;
    }
}

public class ShippingSystem
{
    public bool ShipOrder(string address, string products)
    {
        Console.WriteLine($"Creating cargo: Address: {address}, Products: {products}");
        return true;
    }
}
#endregion
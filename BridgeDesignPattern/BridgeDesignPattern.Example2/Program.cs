
Warrior warrior = new(new AttackBehaviour());
warrior.Move();
warrior.Interact();

Archer archer = new(new AttackBehaviour());
archer.Move();
archer.Interact();

Merchant merchant = new(new TradeBehaviour());
merchant.Move();
merchant.Interact();

#region Abstraction
public abstract class Character
{
    protected IInteractionBehaviour _interactionBehaviour;

    public Character(IInteractionBehaviour interactionBehaviour)
    {
        _interactionBehaviour = interactionBehaviour;
    }

    public abstract void Move();
    public virtual void Interact()
        => _interactionBehaviour.Interact();
}
#endregion

#region Refined Abstraction
public class Warrior : Character
{
    public Warrior(IInteractionBehaviour interactionBehaviour) : base(interactionBehaviour)
    {
    }

    public override void Move()
        => Console.WriteLine("Warrior moved.");
}

public class Archer : Character
{
    public Archer(IInteractionBehaviour interactionBehaviour) : base(interactionBehaviour)
    {
    }

    public override void Move()
        => Console.WriteLine("Archer moved.");
}

public class Merchant : Character
{
    public Merchant(IInteractionBehaviour interactionBehaviour) : base(interactionBehaviour)
    {
    }

    public override void Move()
        => Console.WriteLine("Merchant moved.");
}
#endregion

#region Implementor
public interface IInteractionBehaviour
{
    void Interact();
}
#endregion

#region Concrete Implementor
public class AttackBehaviour : IInteractionBehaviour
{
    public void Interact()
        => Console.WriteLine("Character attacked.");
}

public class TradeBehaviour : IInteractionBehaviour
{
    public void Interact()
        => Console.WriteLine("Character traded.");
}
#endregion
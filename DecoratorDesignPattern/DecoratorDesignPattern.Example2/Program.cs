
Console.WriteLine("Repository");
Repository<Employee> repository = new();
repository.Get(3);
repository.GetAll();
repository.Add(new Employee());
repository.Delete(new Employee());
repository.Update(new Employee());

Console.WriteLine("\nSecurityRepositoryDecorator");
Console.WriteLine("****************");
SecurityRepositoryDecorator<Employee> securityRepositoryDecorator = new(repository);
securityRepositoryDecorator.Get(3);
securityRepositoryDecorator.GetAll();
securityRepositoryDecorator.Add(new Employee());
securityRepositoryDecorator.Delete(new Employee());
securityRepositoryDecorator.Update(new Employee());

Console.WriteLine("\nLoggingRepositoryDecorator");
Console.WriteLine("****************");
LoggingRepositoryDecorator<Employee> loggingRepositoryDecorator = new(repository);
loggingRepositoryDecorator.Get(3);
loggingRepositoryDecorator.GetAll();
loggingRepositoryDecorator.Add(new Employee());
loggingRepositoryDecorator.Delete(new Employee());
loggingRepositoryDecorator.Update(new Employee());

Console.WriteLine("\nSendCRMRepositoryDecorator");
Console.WriteLine("****************");
SendCRMRepositoryDecorator<Employee> sendCRMRepositoryDecorator = new(repository);
sendCRMRepositoryDecorator.Get(3);
sendCRMRepositoryDecorator.GetAll();
sendCRMRepositoryDecorator.Add(new Employee());
sendCRMRepositoryDecorator.Delete(new Employee());
sendCRMRepositoryDecorator.Update(new Employee());

Console.WriteLine("\nSendMailRepositoryDecorator");
Console.WriteLine("****************");
SendMailRepositoryDecorator<Employee> sendMailRepositoryDecorator = new(repository);
sendMailRepositoryDecorator.Get(3);
sendMailRepositoryDecorator.GetAll();
sendMailRepositoryDecorator.Add(new Employee());
sendMailRepositoryDecorator.Delete(new Employee());
sendMailRepositoryDecorator.Update(new Employee());

class Employee
{ }

#region Component
public interface IRepository<T> where T : class
{
    T Get(int id);
    T GetAll();
    void Add(T model);
    void Update(T model);
    void Delete(T model);
}
#endregion

#region Concrete Component
public class Repository<T> : IRepository<T> where T : class
{
    public T Get(int id)
    {
        Console.WriteLine($"Record fetched for id {id}.");
        return null;
    }

    public T GetAll()
    {
        Console.WriteLine("All data was pulled.");
        return null;
    }

    public void Add(T model)
        => Console.WriteLine("Data added.");

    public void Delete(T model)
        => Console.WriteLine("Data deleted.");

    public void Update(T model)
        => Console.WriteLine("Data updated");
}
#endregion

#region Decorator
public class DecoratorRepository<T> : IRepository<T> where T : class
{
    protected IRepository<T> _repository;

    public DecoratorRepository(IRepository<T> repository)
    {
        _repository = repository;
    }

    virtual public void Add(T model)
        => _repository.Add(model);

    virtual public void Delete(T model)
        => _repository.Delete(model);

    virtual public T Get(int id)
        => _repository.Get(id);

    virtual public T GetAll()
        => _repository.GetAll();

    virtual public void Update(T model)
        => _repository.Update(model);
}
#endregion

#region Concrete Decorator
public class SecurityRepositoryDecorator<T> : DecoratorRepository<T> where T : class
{
    public SecurityRepositoryDecorator(IRepository<T> repository) : base(repository)
    {
    }

    public override T Get(int id)
    {
        Console.WriteLine("Security check in progress....");
        return base.Get(id);
    }

    public override T GetAll()
    {
        Console.WriteLine("Security check in progress....");
        return base.GetAll();
    }
}

public class LoggingRepositoryDecorator<T> : DecoratorRepository<T> where T : class
{
    public LoggingRepositoryDecorator(IRepository<T> repository) : base(repository)
    {
    }

    public override void Add(T model)
    {
        base.Add(model);
        Console.WriteLine($"LOG : {typeof(T).Name} added.");
    }
    public override void Delete(T model)
    {
        base.Delete(model);
        Console.WriteLine($"LOG : {typeof(T).Name} deleted.");
    }
    public override void Update(T model)
    {
        base.Update(model);
        Console.WriteLine($"LOG : {typeof(T).Name} updated.");
    }
}

public class SendCRMRepositoryDecorator<T> : DecoratorRepository<T> where T : class
{
    public SendCRMRepositoryDecorator(IRepository<T> repository) : base(repository)
    {
    }

    public override void Delete(T model)
    {
        base.Delete(model);
        Console.WriteLine("The deletion of the record has been processed in the CRM database.");
    }
    public override void Update(T model)
    {
        base.Update(model);
        Console.WriteLine("The update of the record has been committed to the CRM database.");
    }
}

public class SendMailRepositoryDecorator<T> : DecoratorRepository<T> where T : class
{
    public SendMailRepositoryDecorator(IRepository<T> repository) : base(repository)
    {
    }

    public override void Update(T model)
    {
        base.Update(model);
        Console.WriteLine($"{DateTime.Now} | Email sent...");
    }
}
#endregion
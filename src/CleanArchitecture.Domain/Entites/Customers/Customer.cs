using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Domain.Entites.Customers;

public class Customer : DomainEntity
{
    public CustomerId Id { get; private init; }
    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Address { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Customer() { }
    
    public static Customer Create(CustomerId id, string email, string name, string? address = null)
    {
        var customer = new Customer
        {
            Id = id,
            Email = email,
            Name = name,
            Address = address
        };
        
        customer.Raise(new CustomerCreatedDomainEvent(Guid.NewGuid(), customer.Id));

        return customer;
    }

    public void Update(string email, string name, string? address)
    {
        Email = email;
        Name = name;
        Address = address;
        
        Raise(new CustomerUpdatedDomainEvent(Guid.NewGuid(), Id));
    }
    
    public void Delete() =>
        Raise(new CustomerDeletedDomainEvent(Guid.NewGuid(), Id));
}
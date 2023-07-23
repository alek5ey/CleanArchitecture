using CleanArchitecture.Domain.DomainEvents.Products;
using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Domain.Entites.Products;

public class Product : DomainEntity
{
    public ProductId Id { get; private init; }
    public string Name { get; private set; }
    public Money Money { get; private set; }
    public Sku Sku { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Product() { }

    public static Product Create(ProductId id, string name, Money money, Sku sku)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Money = money,
            Sku = sku
        };
        
        product.Raise(new ProductCreatedDomainEvent(Guid.NewGuid(), product.Id));

        return product;
    }

    public void Update(string name, Money money, Sku sku)
    {
        Name = name;
        Money = money;
        Sku = sku;
        
        Raise(new ProductUpdatedDomainEvent(Guid.NewGuid(), Id));
    }
    
    public void Delete() =>
        Raise(new ProductDeletedDomainEvent(Guid.NewGuid(), Id));
}
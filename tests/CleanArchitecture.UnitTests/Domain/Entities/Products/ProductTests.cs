using CleanArchitecture.Domain.DomainEvents.Products;
using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.UnitTests.Domain.Entities.Products;

public class ProductTests
{
    [Fact]
    public void Create_WithParams_Successful()
    {
        var faker = new Faker();
        var productId = new ProductId(Guid.NewGuid());
        var name = faker.Commerce.Product();
        var money = Money.Create(faker.Finance.Currency().Code!, faker.Random.Decimal(0m, 100m));
        var sku = Sku.Create(faker.Random.AlphaNumeric(Sku.DefaultLength));

        var product = CreateProductSample(
            productId,
            name,
            money,
            sku);

        product.Id.Should().Be(productId);
        product.Name.Should().Be(name);
        product.Money.Should().Be(money);
        product.Sku.Should().Be(sku);
        product.HasDomainEvents.Should().BeTrue();
        product.GetDomainEvents().Should().HaveCount(1);
        product.GetDomainEvents().First().Should().BeOfType<ProductCreatedDomainEvent>();
        ((ProductCreatedDomainEvent)product.GetDomainEvents().First()).ProductId.Should().Be(productId);
    }
    
    [Fact]
    public void Update_WithParams_Successful()
    {
        var productId = new ProductId(Guid.NewGuid());
        var product = CreateProductSample(productId);
        var updatedProduct = CreateProductSample();
        
        product.Update(
            updatedProduct.Name,
            updatedProduct.Money,
            updatedProduct.Sku);
        
        product.Id.Should().Be(productId);
        product.Name.Should().Be(updatedProduct.Name);
        product.Money.Should().Be(updatedProduct.Money);
        product.Sku.Should().Be(updatedProduct.Sku);
        product.HasDomainEvents.Should().BeTrue();
        product.GetDomainEvents().Should().HaveCount(2);
        product.GetDomainEvents().Should().ContainItemsAssignableTo<ProductUpdatedDomainEvent>();
        ((ProductUpdatedDomainEvent)product.GetDomainEvents().Last()).ProductId.Should().Be(productId);
    }
    
    [Fact]
    public void Delete_Successful()
    {
        var product = CreateProductSample();
        
        product.Delete();
        
        product.HasDomainEvents.Should().BeTrue();
        product.GetDomainEvents().Should().HaveCount(2);
        product.GetDomainEvents().Should().ContainItemsAssignableTo<ProductDeletedDomainEvent>();
        ((ProductDeletedDomainEvent)product.GetDomainEvents().Last()).ProductId.Should().Be(product.Id);
    }

    private static Product CreateProductSample(
        ProductId? productId = null,
        string? name = null,
        Money? money = null,
        Sku? sku = null)
    {
        var faker = new Faker();
        productId ??= new ProductId(Guid.NewGuid());
        name ??= faker.Commerce.Product();
        money ??= Money.Create(faker.Finance.Currency().Code!, faker.Random.Decimal(0m, 100m));
        sku ??= Sku.Create(faker.Random.AlphaNumeric(Sku.DefaultLength));

        return new Faker<Product>()
            .CustomInstantiator(_ => Product.Create(productId, name, money, sku))
            .Generate();
    }
}
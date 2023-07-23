using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.UnitTests.Domain.Entities.Products;

public class SkuTests
{
    [Fact]
    public void Create_WithValue_Successful()
    {
        var value = new Faker().Random.AlphaNumeric(Sku.DefaultLength);
        
        var sku = Sku.Create(value);

        sku.Value.Should().Be(value);
    }
}
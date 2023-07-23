using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.UnitTests.Domain.Entities.Products;

public class MoneyTests
{
    [Fact]
    public void Create_WithCurrencyAndAmount_Successful()
    {
        var faker = new Faker();
        var currency = faker.Finance.Currency().Code!;
        var amount = faker.Random.Decimal(0m, 100m);
        
        var money = Money.Create(currency, amount);

        money.Currency.Should().Be(currency);
        money.Amount.Should().Be(amount);
    }
}
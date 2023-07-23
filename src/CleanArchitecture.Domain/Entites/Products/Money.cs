namespace CleanArchitecture.Domain.Entites.Products;

public class Money
{
    public string Currency { get; private set; }
    public decimal Amount { get; private set; }

    private Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }
    
    public static Money Create(string currency, decimal amount) =>
        new(currency, amount);
}
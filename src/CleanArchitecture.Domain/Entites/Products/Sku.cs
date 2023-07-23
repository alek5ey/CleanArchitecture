namespace CleanArchitecture.Domain.Entites.Products;

public class Sku
{
    public const int DefaultLength = 15;

    public string Value { get; init; }
    
    private Sku(string value)
    {
        Value = value;
    }

    public static Sku Create(string value) => new (value);
}
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.Persistence.Extensions;

public static class SeedExtensions
{
    public static async Task SeedDatabaseAsync(this ApplicationDbContext dbContext)
    {
        var sampleProducts = new List<Product>
        {
            Product.Create(new(Guid.Parse("a56447d1-9197-46fd-a806-933e7b6a1524")), "banana", Money.Create("USD", 1.61m), Sku.Create("81748703534556")),
            Product.Create(new(Guid.Parse("f8d2f47d-dbe1-4792-8b11-5d5d5cbfbe64")), "cherry", Money.Create("USD", 11.5m), Sku.Create("792448264641000")),
            Product.Create(new(Guid.Parse("e2164e66-5faf-4d92-af16-c0ca0d0f00e2")), "orange", Money.Create("USD", 4.18m), Sku.Create("649677385392957")),
            Product.Create(new(Guid.Parse("f1aa8861-3745-42af-b1c0-ce4fd5c49689")), "raspberry", Money.Create("USD", 8.25m), Sku.Create("263595337472523")),
            Product.Create(new(Guid.Parse("1ea4e2e1-475f-4887-b4f9-3a1116cdfb48")), "apple", Money.Create("USD", 4.9m), Sku.Create("73582450250489")),
            Product.Create(new(Guid.Parse("b93aad8d-6d48-423c-a98d-7a692e017268")), "pear", Money.Create("USD", 2.7m), Sku.Create("866814866279924"))
        };
        var sampleCustomers = new List<Customer>
        {
            Customer.Create(new(Guid.Parse("47e43833-b417-418d-8779-14b1523a6903")), "alex@cleanarchitecture.xyz", "alex", "689 Cherry Hill Rd.Brooklyn, NY 11211"),
            Customer.Create(new(Guid.Parse("bcb463ad-bfdf-4877-b4a7-683fc36291b6")), "emma@cleanarchitecture.xyz", "emma", "23 Garfield St.New York, NY 10009")
        };
        var sampleOrders = new List<Order>
        {
            Order.Create(new(Guid.Parse("93d62b9a-35cc-4fc1-9d5c-3f8b1d78939d")), sampleCustomers[0].Id, DateTimeOffset.Now, "Test comment")
        };
        var sampleOrderLiness = new List<OrderLine>
        {
            OrderLine.Create(new(Guid.Parse("f04df43d-82b1-4f22-b405-1703fcfe1228")), sampleOrders[0].Id, sampleProducts[0].Id, sampleProducts[0].Money, 1),
            OrderLine.Create(new(Guid.Parse("a0482230-3cd3-4ba6-93f8-717f31f5c7bd")), sampleOrders[0].Id, sampleProducts[1].Id, sampleProducts[1].Money, 2)
        };
        
        await dbContext.Products.AddRangeAsync(sampleProducts);
        await dbContext.Customers.AddRangeAsync(sampleCustomers);
        await dbContext.Orders.AddRangeAsync(sampleOrders);
        await dbContext.OrderLines.AddRangeAsync(sampleOrderLiness);

        await dbContext.SaveChangesAsync();
    }
}
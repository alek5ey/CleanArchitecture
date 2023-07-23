using CleanArchitecture.Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasConversion(
            productId => productId.Id,
            value => new(value));
        
        builder.Property(p => p.Name);
        
        builder.Property(p => p.Sku).HasConversion(
            sku => sku.Value,
            value => Sku.Create(value));

        builder.OwnsOne(p => p.Money, priceBuilder =>
        {
            priceBuilder.Property(m => m.Currency).HasMaxLength(3);
            priceBuilder.Property(m => m.Amount);
        });
    }
}
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

internal class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(oi => oi.Id);
        
        builder.Property(oi => oi.Id).HasConversion(
            orderLineId => orderLineId.Id,
            value => new(value));
        
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);

        builder.OwnsOne(oi => oi.Price);
        
        builder.Property(oi => oi.Quantity);
    }
}
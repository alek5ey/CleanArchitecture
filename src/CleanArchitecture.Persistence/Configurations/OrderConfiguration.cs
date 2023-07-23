using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).HasConversion(
            orderId => orderId.Id,
            value => new(value));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderLines)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);
        
        builder.Property(c => c.CreateTime).HasConversion(
            createTime => createTime.UtcDateTime,
            value => new(value));
        
        builder.Property(c => c.Comment).HasMaxLength(1000);
    }
}
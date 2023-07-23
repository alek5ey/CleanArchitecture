using CleanArchitecture.Domain.Entites.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Id,
            value => new(value));

        builder.Property(c => c.Name).HasMaxLength(200);

        builder.Property(c => c.Email).HasMaxLength(320);
        builder.HasIndex(c => c.Email).IsUnique();
        
        builder.Property(c => c.Address).HasMaxLength(1000);
    }
}
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<OrderLine> OrderLines { get; set; }
}
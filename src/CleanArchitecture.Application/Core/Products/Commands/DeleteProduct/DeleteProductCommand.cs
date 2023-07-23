using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Commands.DeleteProduct;

public record DeleteProductCommand(ProductId ProductId) : ICommand<Result>;
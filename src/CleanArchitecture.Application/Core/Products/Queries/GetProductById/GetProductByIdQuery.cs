using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Products.Responses;
using CleanArchitecture.Domain.Entites.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Queries.GetProductById;

public record GetProductByIdQuery(ProductId ProductId) : IQuery<Result<ProductResponse>>;
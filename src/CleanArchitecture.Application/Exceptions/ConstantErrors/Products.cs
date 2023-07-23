using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.Application.Exceptions.ConstantErrors;

internal static class Products
{
    internal static ValidationError IdIsRequired =>
        new($"{nameof(Products)}.{nameof(IdIsRequired)}","The product id is required.");
    
    internal static ValidationError NameIsRequired =>
        new($"{nameof(Products)}.{nameof(NameIsRequired)}","The product name is required.");
    
    internal static ValidationError CurrencyNameIsRequired =>
        new($"{nameof(Products)}.{nameof(CurrencyNameIsRequired)}","The currency name is required.");
    
    internal static ValidationError SkuIdIsRequired =>
        new($"{nameof(Products)}.{nameof(SkuIdIsRequired)}","The sku id is required.");
    
    internal static ValidationError SkuMustBeValid =>
        new($"{nameof(Products)}.{nameof(SkuMustBeValid)}",$"The sku must be a {Sku.DefaultLength} length.");
    
    internal static ValidationError AmountIsGreatZero =>
        new($"{nameof(Products)}.{nameof(AmountIsGreatZero)}","The amount great zero is required.");
    
    internal static NotFoundError NotFoundById =>
        new($"{nameof(Products)}.{nameof(NotFoundById)}","The product with the specified identifier was not found.");
}
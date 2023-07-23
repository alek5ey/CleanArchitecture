namespace CleanArchitecture.Application.Exceptions.ConstantErrors;

internal static class Orders
{
    internal static ValidationError IdIsRequired =>
        new($"{nameof(Orders)}.{nameof(IdIsRequired)}","The order id is required.");
    
    internal static NotFoundError NotFoundById =>
        new($"{nameof(Orders)}.{nameof(NotFoundById)}", "The order with the specified identifier was not found.");
    
    internal static ValidationError OrderAlreadyContainsProduct =>
        new($"{nameof(Orders)}.{nameof(OrderAlreadyContainsProduct)}","Order already contains this product.");
}
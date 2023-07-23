namespace CleanArchitecture.Application.Exceptions.ConstantErrors;

internal static class OrderLines
{
    internal static ValidationError IdIsRequired =>
        new($"{nameof(OrderLines)}.{nameof(IdIsRequired)}","The order line id is required.");
    
    internal static NotFoundError NotFoundById =>
        new($"{nameof(OrderLines)}.{nameof(NotFoundById)}", "The order line with the specified identifier was not found.");
    
    internal static ValidationError QuantityNotValid =>
        new($"{nameof(OrderLines)}.{nameof(QuantityNotValid)}","The order line quantity is invalid.");
}
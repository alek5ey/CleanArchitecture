namespace CleanArchitecture.Application.Exceptions.ConstantErrors;

internal static class Customers
{
    internal static ValidationError IdIsRequired =>
        new($"{nameof(Customers)}.{nameof(IdIsRequired)}","The customer id is required.");

    internal static ValidationError EmailIsRequired =>
        new($"{nameof(Customers)}.{nameof(EmailIsRequired)}","The customer email is required.");
    internal static ValidationError EmailIsInvalid =>
        new($"{nameof(Customers)}.{nameof(EmailIsInvalid)}","The customer email is invalid.");

    internal static ValidationError NameIsRequired =>
        new($"{nameof(Customers)}.{nameof(NameIsRequired)}","The customer name is required.");

    internal static NotFoundError NotFoundById =>
        new($"{nameof(Customers)}.{nameof(NotFoundById)}", "The customer with the specified identifier was not found.");
}
using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.UnitTests.Domain.Primitives;

public class DomainErrorTests
{
    [Fact]
    public void Create_WithCodeAndMessage_Successful()
    {
        var code = "Test.NotFoundById";
        var message = "The sample with the specified identifier was not found.";
        var mockDomainError = new Mock<DomainError>(code, message);

        var domainError = mockDomainError.Object;

        domainError.Code.Should().Be(code);
        domainError.Message.Should().Be(message);
    }
}
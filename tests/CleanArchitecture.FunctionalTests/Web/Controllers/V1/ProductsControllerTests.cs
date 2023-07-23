using System.Net;
using System.Text;
using System.Text.Json;
using Ardalis.HttpClientTestExtensions;
using CleanArchitecture.Application.Contracts.Products.Requests;
using CleanArchitecture.Application.Contracts.Products.Responses;
using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.FunctionalTests.Web.Helpers;
using CleanArchitecture.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.FunctionalTests.Web.Controllers.V1;

public class ProductsControllerTests : IClassFixture<ApiTestFixture>
{
    private readonly HttpClient _client;
    private readonly ApplicationDbContext _context;
    private const string BasePath = "/api/v1/products";

    public ProductsControllerTests(ApiTestFixture fixture)
    {
        _client = fixture.CreateClient();
        _context = fixture.Services.GetRequiredService<ApplicationDbContext>();
    }

    [Fact]
    public async Task Get_ByParams_ShouldSuccessful()
    {
        var @params = new Dictionary<string, string>()
        {
            ["page"] = "1",
            ["pageSize"] = "30"
        };
        var paramsQuery = string.Join('&', @params.Select(p => $"{p.Key}={p.Value}"));
        
        var result = await _client.GetAndDeserializeAsync<SerializablePagedResponse<ProductResponse>>($"{BasePath}?{paramsQuery}");

        result.Should().NotBeNull();
        result.Page.Should().Be(1);
        result.PageSize.Should().Be(30);
        result.Items.Should().NotBeEmpty();
        result.TotalCount.Should().Be(result.Items.Count);
    }
    
    [Fact]
    public async Task Get_ByName_ShouldFoundSuccessful()
    {
        var @params = new Dictionary<string, string>()
        {
            ["name"] = "banana"
        };
        var paramsQuery = string.Join('&', @params.Select(p => $"{p.Key}={p.Value}"));
        
        var result = await _client.GetAndDeserializeAsync<SerializablePagedResponse<ProductResponse>>($"{BasePath}?{paramsQuery}");

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(1);
        result.Items.Count.Should().Be(1);
        result.Items.First()!.Name.Should().Be("banana");
    }
    
    [Fact]
    public async Task Get_ByName_ShouldNotFoundError()
    {
        var @params = new Dictionary<string, string>()
        {
            ["name"] = "foo"
        };
        var paramsQuery = string.Join('&', @params.Select(p => $"{p.Key}={p.Value}"));
        
        var result = await _client.GetAndDeserializeAsync<SerializablePagedResponse<ProductResponse>>($"{BasePath}?{paramsQuery}");

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(0);
        result.Items.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Get_ById_ShouldSuccessful()
    {
        var productId = "a56447d1-9197-46fd-a806-933e7b6a1524"; // banana
        
        var result = await _client.GetAndDeserializeAsync<ProductResponse>($"{BasePath}/{productId}");

        result.Should().NotBeNull();
        result.Id.Should().Be(productId);
        result.Name.Should().Be("banana");
    }
    
    [Fact]
    public async Task Get_ById_ShouldNotFoundError()
    {
        var productId = Guid.Empty;
        
        var result = await _client.GetAsync($"{BasePath}/{productId}");

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task Create_ByParams_ShouldSuccessful()
    {
        var createProductRequest = new Faker<CreateProductRequest>()
            .RuleFor(r => r.Name, f => f.Commerce.Product())
            .RuleFor(r => r.Sku, f => f.Random.AlphaNumeric(Sku.DefaultLength))
            .RuleFor(r => r.Currency, f => f.Finance.Currency().Code)
            .RuleFor(r => r.Amount, f => f.Random.Decimal(0m, 100m))
            .Generate();
        var content = new StringContent(JsonSerializer.Serialize(createProductRequest), Encoding.UTF8, "application/json");
        
        var result = await _client.PostAsync($"{BasePath}", content);
        var addedEntity = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == createProductRequest.Name);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        addedEntity.Should().NotBeNull();
        addedEntity!.Id.Id.Should().NotBe(Guid.Empty);
        addedEntity.Name.Should().Be(createProductRequest.Name);
        addedEntity.Sku.Value.Should().Be(createProductRequest.Sku);
        addedEntity.Money.Currency.Should().Be(createProductRequest.Currency);
        addedEntity.Money.Amount.Should().Be(createProductRequest.Amount);
    }
    
    [Fact]
    public async Task Update_ByParams_ShouldSuccessful()
    {
        var productId = "f8d2f47d-dbe1-4792-8b11-5d5d5cbfbe64"; // cherry
        var updateProductRequest = new Faker<UpdateProductRequest>()
            .RuleFor(r => r.Name, f => f.Commerce.Product())
            .RuleFor(r => r.Sku, f => f.Random.AlphaNumeric(Sku.DefaultLength))
            .RuleFor(r => r.Currency, f => f.Finance.Currency().Code)
            .RuleFor(r => r.Amount, f => f.Random.Decimal(0m, 100m))
            .Generate();
        var content = new StringContent(JsonSerializer.Serialize(updateProductRequest), Encoding.UTF8, "application/json");
        
        var result = await _client.PutAsync($"{BasePath}/{productId}", content);
        var updatedEntity = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == updateProductRequest.Name);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        updatedEntity!.Id.Id.Should().Be(productId);
        updatedEntity.Name.Should().Be(updateProductRequest.Name);
        updatedEntity.Sku.Value.Should().Be(updateProductRequest.Sku);
        updatedEntity.Money.Currency.Should().Be(updateProductRequest.Currency);
        updatedEntity.Money.Amount.Should().Be(updateProductRequest.Amount);
    }
    
    [Fact]
    public async Task Update_ByParams_ShouldNotFoundError()
    {
        var productId = Guid.NewGuid();
        var createProductRequest = new Faker<CreateProductRequest>()
            .RuleFor(r => r.Name, f => f.Commerce.Product())
            .RuleFor(r => r.Sku, f => f.Random.AlphaNumeric(Sku.DefaultLength))
            .RuleFor(r => r.Currency, f => f.Finance.Currency().Code)
            .RuleFor(r => r.Amount, f => f.Random.Decimal(0m, 100m))
            .Generate();
        var content = new StringContent(JsonSerializer.Serialize(createProductRequest), Encoding.UTF8, "application/json");
        
        var result = await _client.PutAsync($"{BasePath}/{productId}", content);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task Delete_ById_ShouldSuccessful()
    {
        var productId = "e2164e66-5faf-4d92-af16-c0ca0d0f00e2"; // orange
        
        var result = await _client.DeleteAsync($"{BasePath}/{productId}");
        var deletedEntity = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == new ProductId(Guid.Parse(productId)));

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        deletedEntity.Should().BeNull();
    }
    
    [Fact]
    public async Task Delete_ById_ShouldNotFoundError()
    {
        var productId = Guid.NewGuid();
        
        var result = await _client.DeleteAsync($"{BasePath}/{productId}");

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
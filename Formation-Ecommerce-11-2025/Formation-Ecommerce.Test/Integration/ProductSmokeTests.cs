using System.Net;

namespace Formation_Ecommerce_11_2025.Test.Integration;

/// <summary>
/// Smoke tests d'intégration sur les routes liées aux produits.
/// </summary>
public class ProductSmokeTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProductSmokeTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Get_product_index_returns_ok()
    {
        var response = await _client.GetAsync("/Product/ProductIndex");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

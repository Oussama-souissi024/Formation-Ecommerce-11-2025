using System.Net;

namespace Formation_Ecommerce_11_2025.Test.Integration;

/// <summary>
/// Smoke tests d'intégration pour vérifier que les routes principales du module Auth répondent.
/// </summary>
public class AuthSmokeTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthSmokeTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Get_auth_login_returns_ok()
    {
        var response = await _client.GetAsync("/Auth/Login");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}

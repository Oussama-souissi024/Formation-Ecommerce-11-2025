using System.Net;

namespace Formation_Ecommerce_11_2025.Test.Integration;

/// <summary>
/// Tests d'intégration orientés "navigation" : vérifie les redirections quand l'utilisateur n'est pas authentifié.
/// </summary>
public class HomeRedirectTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public HomeRedirectTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Get_root_when_not_authenticated_redirects_to_auth_login()
    {
        var response = await _client.GetAsync("/");

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        Assert.Contains("/Auth/Login", response.Headers.Location!.ToString());
    }
}

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Formation_Ecommerce_11_2025.Test.Integration;

/// <summary>
/// Smoke tests d'intégration du point d'entrée (route "/").
/// </summary>
public class HomeSmokeTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public HomeSmokeTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task Get_root_returns_ok_or_redirect()
    {
        var response = await _client.GetAsync("/");

        Assert.True(
            response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Redirect,
            $"Unexpected status code: {(int)response.StatusCode} ({response.StatusCode})");
    }
}

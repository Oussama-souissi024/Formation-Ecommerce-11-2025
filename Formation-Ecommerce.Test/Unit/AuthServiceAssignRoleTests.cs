using Formation_Ecommerce_11_2025.Application.Athentication.Services;
using Formation_Ecommerce_11_2025.Core.Entities.Identity;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires de <see cref="AuthService" /> centrés sur l'affectation d'un rôle à un utilisateur.
/// </summary>
public class AuthServiceAssignRoleTests
{
    [Fact]
    public async Task AssignRole_when_user_not_found_returns_false()
    {
        var repo = new FakeAuthRepository { UserByEmail = null };
        var service = new AuthService(repo);

        var ok = await service.AssingnRole("missing@x.com", "Admin");

        Assert.False(ok);
    }

    [Fact]
    public async Task AssignRole_when_role_does_not_exist_and_create_fails_returns_false()
    {
        var repo = new FakeAuthRepository
        {
            UserByEmail = new ApplicationUser { Email = "a@a.com", UserName = "a@a.com" },
            RoleExistsResult = false,
            CreateRoleResult = false
        };
        var service = new AuthService(repo);

        var ok = await service.AssingnRole("a@a.com", "Admin");

        Assert.False(ok);
        Assert.Equal("Admin", repo.LastRoleChecked);
        Assert.Equal("Admin", repo.LastRoleCreated);
    }

    [Fact]
    public async Task AssignRole_when_role_exists_adds_user_to_role()
    {
        var repo = new FakeAuthRepository
        {
            UserByEmail = new ApplicationUser { Email = "a@a.com", UserName = "a@a.com" },
            RoleExistsResult = true,
            AddUserToRoleResult = true
        };
        var service = new AuthService(repo);

        var ok = await service.AssingnRole("a@a.com", "Customer");

        Assert.True(ok);
        Assert.Equal("Customer", repo.LastRoleChecked);
        Assert.Equal("Customer", repo.LastRoleAddedToUser);
    }
}

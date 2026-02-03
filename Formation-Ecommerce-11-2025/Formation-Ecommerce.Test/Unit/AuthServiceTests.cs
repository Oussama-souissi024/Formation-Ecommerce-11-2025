using Formation_Ecommerce_11_2025.Application.Athentication.Dtos;
using Formation_Ecommerce_11_2025.Application.Athentication.Services;
using Formation_Ecommerce_11_2025.Core.Entities.Identity;
using Formation_Ecommerce_11_2025.Test.Fakes;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires de <see cref="AuthService" /> (inscription, connexion, déconnexion, confirmation email, reset password).
/// </summary>
public class AuthServiceTests
{
    [Fact]
    public async Task Register_when_user_already_exists_returns_expected_message()
    {
        var repo = new FakeAuthRepository
        {
            UserByEmail = new ApplicationUser { Email = "a@a.com", UserName = "a@a.com" }
        };
        var service = new AuthService(repo);

        var result = await service.Register(new RegistrationRequestDto
        {
            Email = "a@a.com",
            PhoneNumber = "0600000000",
            Password = "P@ssw0rd",
            Role = "Customer"
        });

        Assert.Equal("Un utilisateur avec cet email existe déjà.", result);
    }

    [Fact]
    public async Task Register_when_role_is_null_defaults_to_customer()
    {
        var repo = new FakeAuthRepository
        {
            UserByEmail = null,
            RoleExistsResult = true,
            CreateUserResult = true,
            AddUserToRoleResult = true
        };
        var service = new AuthService(repo);

        var result = await service.Register(new RegistrationRequestDto
        {
            Email = "b@b.com",
            PhoneNumber = "0600000000",
            Password = "P@ssw0rd",
            Role = null
        });

        Assert.Equal("Inscription réussie!", result);
        Assert.Equal("Customer", repo.LastRoleChecked);
        Assert.Equal("Customer", repo.LastRoleAddedToUser);
    }

    [Fact]
    public async Task Login_when_user_not_found_returns_error()
    {
        var repo = new FakeAuthRepository
        {
            UserByEmail = null
        };
        var service = new AuthService(repo);

        var response = await service.Login(new LoginRequestDto { Email = "missing@x.com", Password = "x" });

        Assert.False(response.IsSuccess);
        Assert.Equal("Utilisateur non trouvé", response.Error);
    }

    [Fact]
    public async Task Login_when_signin_fails_returns_error()
    {
        var repo = new FakeAuthRepository
        {
            UserByEmail = new ApplicationUser { Email = "a@a.com", UserName = "a@a.com" },
            PasswordSignInResult = false
        };
        var service = new AuthService(repo);

        var response = await service.Login(new LoginRequestDto { Email = "a@a.com", Password = "bad" });

        Assert.False(response.IsSuccess);
        Assert.Equal("Authentification échouée", response.Error);
    }

    [Fact]
    public async Task Logout_when_repository_throws_returns_false()
    {
        var repo = new FakeAuthRepository { ThrowOnSignOut = true };
        var service = new AuthService(repo);

        var result = await service.Logout();

        Assert.False(result);
    }

    [Fact]
    public async Task ConfirmEmail_when_user_not_found_returns_false()
    {
        var repo = new FakeAuthRepository { UserById = null };
        var service = new AuthService(repo);

        var result = await service.ConfirmEmail("missing", "token");

        Assert.False(result);
    }

    [Fact]
    public async Task ResetPassword_when_user_not_found_returns_false()
    {
        var repo = new FakeAuthRepository { UserById = null };
        var service = new AuthService(repo);

        var result = await service.ResetPassword("missing", "token", "NewP@ssw0rd");

        Assert.False(result);
    }
}

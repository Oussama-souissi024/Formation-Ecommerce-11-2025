using Formation_Ecommerce_11_2025.Core.Entities.Identity;
using Formation_Ecommerce_11_2025.Core.Interfaces.Repositories;

namespace Formation_Ecommerce_11_2025.Test.Fakes;

/// <summary>
/// Fake (double de test) de <see cref="IAuthRepository" />.
/// </summary>
/// <remarks>
/// Objectif pédagogique :
/// - Éviter un framework de mock pour garder des tests lisibles.
/// - Piloter le comportement via des propriétés (ex: RoleExistsResult).
/// - Tracer certains appels (ex: LastRoleChecked) pour pouvoir faire des assertions.
/// </remarks>
public class FakeAuthRepository : IAuthRepository
{
    public ApplicationUser? UserByEmail { get; set; }
    public ApplicationUser? UserById { get; set; }

    public string? LastRoleChecked { get; private set; }
    public string? LastRoleCreated { get; private set; }
    public string? LastRoleAddedToUser { get; private set; }

    public bool CreateUserResult { get; set; } = true;
    public bool RoleExistsResult { get; set; } = true;
    public bool CreateRoleResult { get; set; } = true;
    public bool AddUserToRoleResult { get; set; } = true;
    public bool PasswordSignInResult { get; set; } = true;

    public bool ThrowOnSignOut { get; set; }

    public string EmailConfirmationToken { get; set; } = "email-token";
    public bool ConfirmEmailResult { get; set; } = true;

    public string PasswordResetToken { get; set; } = "reset-token";
    public bool ResetPasswordResult { get; set; } = true;

    public bool? ConfirmedEmailResult { get; set; } = true;

    public Task<ApplicationUser?> GetUserByEmailAsync(string email) => Task.FromResult(UserByEmail);

    public Task<ApplicationUser?> GetUserByUsernameAsync(string username) => Task.FromResult<ApplicationUser?>(null);

    public Task<ApplicationUser?> GetUserByIdAsync(string userId) => Task.FromResult(UserById);

    public Task<bool> CreateUserAsync(ApplicationUser user, string password)
    {
        UserByEmail = user;
        UserById = user;
        return Task.FromResult(CreateUserResult);
    }

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => Task.FromResult(true);

    public Task<bool> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure) => Task.FromResult(PasswordSignInResult);

    public Task<IList<string>> GetUserRolesAsync(ApplicationUser user) => Task.FromResult<IList<string>>(new List<string>());

    public Task<bool> RoleExistsAsync(string roleName)
    {
        LastRoleChecked = roleName;
        return Task.FromResult(RoleExistsResult);
    }

    public Task<bool> CreateRoleAsync(string roleName)
    {
        LastRoleCreated = roleName;
        return Task.FromResult(CreateRoleResult);
    }

    public Task<bool> AddUserToRoleAsync(ApplicationUser user, string roleName)
    {
        LastRoleAddedToUser = roleName;
        return Task.FromResult(AddUserToRoleResult);
    }

    public Task<bool> SignOutAsync()
    {
        if (ThrowOnSignOut)
        {
            throw new InvalidOperationException("SignOut failed");
        }

        return Task.FromResult(true);
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user) => Task.FromResult(EmailConfirmationToken);

    public Task<bool> ConfirmEmailAsync(ApplicationUser user, string token) => Task.FromResult(ConfirmEmailResult);

    public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user) => Task.FromResult(PasswordResetToken);

    public Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword) => Task.FromResult(ResetPasswordResult);

    public Task<bool?> CheckConfirmedEmail(string email) => Task.FromResult(ConfirmedEmailResult);
}

using RecipeWeb.Application.Common.Interfaces;

namespace RecipeWeb.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    /// <inheritdoc/>
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    /// <inheritdoc/>
    public bool Verify(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}

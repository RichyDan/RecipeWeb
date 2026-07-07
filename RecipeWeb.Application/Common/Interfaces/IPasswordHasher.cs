namespace RecipeWeb.Application.Common.Interfaces;

public interface IPasswordHasher
{
    public string Hash(string password);

    public bool Verify(string password, string hashedPassword);
}

namespace RecipeWeb.Domain.User;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public string Description { get; private set; }
    
    private User(){}

    public static User Create(string firstName, string login, string password, string description)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            Login = login,
            Password = password,
            Description = description
        };
    }

    public void UpdateFirstName(string newFirstname)
    {
        FirstName = newFirstname;
    }

    public void UpdateLogin(string newLogin)
    {
        Login = newLogin;
    }

    public void UpdatePasswordHash(string newPassword)
    {
        Password = newPassword;
    }
    
    public void UpdateDescription(string newDescription)
    {
        Description = newDescription;
    }
    
}
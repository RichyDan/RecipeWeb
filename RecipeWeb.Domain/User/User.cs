using RecipeWeb.Domain.Common;
namespace RecipeWeb.Domain.User;

public class User : Entity
{
    public User (
        string firstName, 
        string login, 
        string password, 
        string description)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        Login = login;
        Password = password;
        Description = description;
    }
    
    public string FirstName { get; private set; }
    public string Login { get; private set; }
    public string Password { get; private set; }
    public string Description { get; private set; }
    
    public void Update(
        string firstname, 
        string login, 
        string password, 
        string description)
    {
        FirstName = firstname;
        Login = login;
        Password = password;
        Description = description;
    }
    
}
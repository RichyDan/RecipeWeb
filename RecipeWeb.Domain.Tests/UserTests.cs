using FluentAssertions;

namespace RecipeWeb.Domain.Tests;

public class UserTests
{
    [Fact]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var firstName = "Толя";
        var login = "yalot_vopop";
        var password = "Qwerty1!";
        var description = "Люблю готовить";

        // Act
        var user = new User.User(firstName, login, password, description);

        // Assert
        user.Id.Should().NotBeEmpty();
        user.FirstName.Should().Be(firstName);
        user.Login.Should().Be(login);
        user.Password.Should().Be(password);
        user.Description.Should().Be(description);
        user.LikedRecipes.Should().NotBeNull().And.BeEmpty();
        user.FavoriteRecipes.Should().NotBeNull().And.BeEmpty();
    }
    
    // Тесты функционала Лайков
    [Fact]
    public void AddLike_ShouldAddRecipeToLiked()
    {
        // Arrange
        var user = CreateValidUser();
        var recipeId = Guid.NewGuid();

        // Act
        user.AddLike(recipeId);

        // Assert
        user.LikedRecipes.Should().HaveCount(1);
        user.LikedRecipes.Should().ContainSingle(x => x.RecipeId == recipeId && x.UserId == user.Id);
    }

    [Fact]
    public void AddLike_ShouldNotAddDuplicate()
    {
        // Arrange
        var user = CreateValidUser();
        var recipeId = Guid.NewGuid();
        user.AddLike(recipeId);

        // Act
        user.AddLike(recipeId); // Пытаемся добавить второй раз

        // Assert
        user.LikedRecipes.Should().HaveCount(1); // Количество не должно увеличиться
    }

    [Fact]
    public void RemoveLike_ShouldRemoveRecipe()
    {
        // Arrange
        var user = CreateValidUser();
        var recipeId = Guid.NewGuid();
        user.AddLike(recipeId);

        // Act
        user.RemoveLike(recipeId);

        // Assert
        user.LikedRecipes.Should().BeEmpty();
    }

    // Тесты функционала Избранного
    [Fact]
    public void AddToFavorites_ShouldAddRecipe()
    {
        // Arrange
        var user = CreateValidUser();
        var recipeId = Guid.NewGuid();

        // Act
        user.AddToFavorites(recipeId);

        // Assert
        user.FavoriteRecipes.Should().HaveCount(1);
        user.FavoriteRecipes.Should().ContainSingle(x => x.RecipeId == recipeId);
    }

    [Fact]
    public void RemoveFromFavorites_ShouldClearCollection()
    {
        // Arrange
        var user = CreateValidUser();
        var recipeId = Guid.NewGuid();
        user.AddToFavorites(recipeId);

        // Act
        user.RemoveFromFavorites(recipeId);

        // Assert
        user.FavoriteRecipes.Should().BeEmpty();
    }

    // Тест обновления данных
    [Fact]
    public void Update_ShouldUpdateAllFieldsAndKeepLikes()
    {
        // Arrange
        var user = new User.User("OldName", "OldLogin", "Password", "Description1");
        var recipeId = Guid.NewGuid();
        user.AddLike(recipeId);

        // Act
        user.Update("NewName", "NewLogin", "NewPassword", "NewDescription");

        // Assert
        user.FirstName.Should().Be("NewName");
        user.Login.Should().Be("NewLogin");
        user.Password.Should().Be("NewPassword");
        user.Description.Should().Be("NewDescription");
        
        // Проверяем, что лайки не стерлись при обычном обновлении
        user.LikedRecipes.Should().HaveCount(1);
    }

    // Вспомогательный метод для быстрого создания пользователя
    private User.User CreateValidUser()
    {
        return new User.User("TestUser", "test_login", "12345678", "Description");
    }
}
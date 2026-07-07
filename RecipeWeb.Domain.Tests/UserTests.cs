using System.Reflection;
using FluentAssertions;
using RecipeWeb.Domain.Common;
using RecipeWeb.Domain.UserAggregate;

namespace RecipeWeb.Domain.RecipeAggregate.Tests;

public class UserTests
{
    // Вспомогательный метод для установки Id
    private T SetId<T>(T entity, Guid id)
        where T : Entity
    {
        typeof(Entity).GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) !
            .SetValue(entity, id);
        return entity;
    }

    [Fact]
    public void Create_User()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string firstName = "Толя";
        string login = "yalot_vopop";
        string password = "Qwerty1!";
        string description = "Люблю готовить";

        // Act
        User user = new User(
            firstName,
            login,
            password,
            description);

        // Assert
        user.Id.Should().Be(Guid.Empty);
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
        User user = this.CreateValidUser();
        var recipeId = Guid.NewGuid();

        // Act
        user.AddLike(recipeId);

        // Assert
        user.LikedRecipes.Should().HaveCount(1);
        var like = user.LikedRecipes.First();
        like.RecipeId.Should().Be(recipeId);
        like.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void AddLike_ShouldNotAddDuplicate()
    {
        // Arrange
        User user = this.CreateValidUser();
        Guid recipeId = Guid.NewGuid();
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
        User user = this.CreateValidUser();
        Guid recipeId = Guid.NewGuid();
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
        User user = this.CreateValidUser();
        Guid recipeId = Guid.NewGuid();

        // Act
        user.AddToFavorites(recipeId);

        // Assert
        var favorite = user.FavoriteRecipes.First();
        favorite.RecipeId.Should().Be(recipeId);
        favorite.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void RemoveFromFavorites_ShouldClearCollection()
    {
        // Arrange
        User user = this.CreateValidUser();
        Guid recipeId = Guid.NewGuid();
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
        var userId = Guid.NewGuid();
        User user = this.CreateValidUser();
        user = this.SetId(user, userId);

        Guid recipeId = Guid.NewGuid();
        user.AddLike(recipeId);

        // Act
        user.Update(
            "NewName",
            "NewLogin",
            "NewPassword",
            "NewDescription");

        // Assert
        user.Id.Should().Be(userId);
        user.FirstName.Should().Be("NewName");
        user.Login.Should().Be("NewLogin");
        user.Password.Should().Be("NewPassword");
        user.Description.Should().Be("NewDescription");

        // Проверяем, что лайки не стерлись при обычном обновлении
        user.LikedRecipes.Should().HaveCount(1);
        user.LikedRecipes.First().UserId.Should().Be(userId);
    }

    // Вспомогательный метод для быстрого создания пользователя
    private User CreateValidUser() => new User(
        "TestUser",
        "test_login",
        "12345678",
        "Description");
}
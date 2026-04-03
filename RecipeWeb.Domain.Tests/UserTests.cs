using FluentAssertions;
using RecipeWeb.Domain.Useraggrigate;
using Guid = System.Guid;

namespace RecipeWeb.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void Create_User()
        {
            // Arrange
            string firstName = "Толя";
            string login = "yalot_vopop";
            string password = "Qwerty1!";
            string description = "Люблю готовить";

            // Act
            User user = new User(firstName, login, password, description);

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
            User user = CreateValidUser();
            Guid recipeId = Guid.NewGuid();

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
            User user = CreateValidUser();
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
            User user = CreateValidUser();
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
            User user = CreateValidUser();
            Guid recipeId = Guid.NewGuid();

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
            User user = CreateValidUser();
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
            User user = new User("OldName", "OldLogin", "Password", "Description1");
            Guid recipeId = Guid.NewGuid();
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
        private User CreateValidUser()
        {
            return new User("TestUser", "test_login", "12345678", "Description");
        }
    }
}
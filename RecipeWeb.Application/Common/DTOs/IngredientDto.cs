namespace RecipeWeb.Application.Common.DTOs;

public class IngredientDto
{
    public string Name { get; set; } = null!;
    public List<string> Products { get; set; } = [];
}

using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();

        Recipe GetRecipe(int id);

        Recipe GetRecipe(string name);

        decimal GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);

    }
}

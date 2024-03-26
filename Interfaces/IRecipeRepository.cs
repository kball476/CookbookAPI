using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IRecipeRepository
    {
        // Usage of the Interface Segregation Principle, from SOLID, is evident based on 
        // the amount of references by each method being greater than 0. So, there are 
        // no extra methods that are not implemented. 

        ICollection<Recipe> GetRecipes();

        Recipe GetRecipe(int id);

        Recipe GetRecipe(string name);

        Recipe GetRecipeByReview(int reviewId);

        ICollection<Recipe> GetRecipesByOwner(int ownerId);

        decimal GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);

        bool CreateRecipe(int categoryId, Recipe recipe);

        bool UpdateRecipe(int ownerId, int categoryId, Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
 
        bool Save();
    }
}

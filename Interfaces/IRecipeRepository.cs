﻿using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();

        Recipe GetRecipe(int id);

        Recipe GetRecipe(string name);

        Recipe GetRecipeByReview(int reviewId);

        ICollection<Recipe> GetRecipesByOwner(int ownerId);

        decimal GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);
        //

        bool CreateRecipe(int categoryId, Recipe recipe);

        bool UpdateRecipe(int ownerId, int categoryId, Recipe recipe);
        bool DeleteRecipe(Recipe recipe);

 
        bool Save();
    }
}

using cookbook3.Data;
using cookbook3.Interfaces;
using cookbook3.Models;

namespace cookbook3.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;

        public RecipeRepository(DataContext context) 
        {
            _context = context;
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(r => r.Id == id).FirstOrDefault();
        }

        public Recipe GetRecipe(string name)
        {
            return _context.Recipes.Where(r => r.Name == name).FirstOrDefault();
        }

        public decimal GetRecipeRating(int recipeId)
        {
            var review = _context.Reviews.Where(r => r.Recipe.Id == recipeId);

            if (review.Count() <= 0)
                return 0;
            return ( (decimal)review.Sum(t => t.Rating) / review.Count());
        }

        public ICollection<Recipe> GetRecipes()
        {
            return _context.Recipes.OrderBy(p => p.Id).ToList();
        }

        public bool RecipeExists(int recipeId)
        {
            return _context.Recipes.Any(r => r.Id == recipeId);
        }
    }
}

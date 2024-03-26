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

        public bool CreateRecipe(int categoryId, Recipe recipe)
        {
            //may need to change owner similar to recipe since not a one to many
   
            var recipeCategoryEntity = _context.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var recipeCategory = new RecipeCategory()
            {
                Recipe = recipe,
                Category = recipeCategoryEntity,
             };

            _context.Add(recipeCategory);

            _context.Add(recipe);

            return Save();

        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();
        }


        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(r => r.Id == id).FirstOrDefault();
        }

        public Recipe GetRecipe(string name)
        {
            return _context.Recipes.Where(r => r.Name == name).FirstOrDefault();
        }
        //for later installation

        public Recipe GetRecipeByReview(int reviewId)
        {
            return _context.Reviews.Where(p => p.Id == reviewId).Select(o => o.Recipe).FirstOrDefault();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRecipe(int ownerId, int categoryId, Recipe recipe)
        {
            _context.Update(recipe);
            return Save();
        }

        ICollection<Recipe> IRecipeRepository.GetRecipesByOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(o => o.Recipes).FirstOrDefault();
        }
    }
}

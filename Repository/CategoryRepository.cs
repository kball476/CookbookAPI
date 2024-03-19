using cookbook3.Data;
using cookbook3.Interfaces;
using cookbook3.Models;

namespace cookbook3.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipeByCategory(int categoryId)
        {
            return _context.RecipeCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Recipe).ToList();
        }
    }
}

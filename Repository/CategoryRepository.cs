using cookbook3.Data;
using cookbook3.Interfaces;
using cookbook3.Models;


// Usage of the Interface Segregation Principle, from SOLID, is evident based on 
// all methods from the interface being incoorporated, given substance (i.e. NO
// "throw new notimplementedexception()"), and utilized in our CategoryController.

// The Single Responsibility Principle can also be seen in each method, as they 
// are all responsible for one purpose. For example, the CategoryExists() only
// returns a true or false based on if any categories are found in the data context.

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

        public bool CreateCategory(Category category)
        {
            
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public Category GetCategoryByRecipe(int recipeId)
        {
            return _context.RecipeCategories.Where(p => p.RecipeId == recipeId).Select(o => o.Category).FirstOrDefault();
        }

        public ICollection<Recipe> GetRecipeByCategory(int categoryId)
        {
            return _context.RecipeCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Recipe).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}

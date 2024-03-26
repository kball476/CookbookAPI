using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface ICategoryRepository
    {
        //get and read 
        ICollection<Category> GetCategories();
        Category GetCategory(int id);

        Category GetCategoryByRecipe(int recipeId);

        ICollection<Recipe> GetRecipeByCategory(int categoryId);

        bool CategoryExists(int id);

        //create

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        
        bool Save();
    }
}

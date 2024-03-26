using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface ICategoryRepository
    {
        // Usage of the Interface Segregation Principle, from SOLID, is evident based on 
        // the amount of references by each method being greater than 0. So, there are 
        // no extra methods that are not implemented. 

        ICollection<Category> GetCategories();
        Category GetCategory(int id);

        Category GetCategoryByRecipe(int recipeId);

        ICollection<Recipe> GetRecipeByCategory(int categoryId);

        bool CategoryExists(int id);

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        
        bool Save();
    }
}

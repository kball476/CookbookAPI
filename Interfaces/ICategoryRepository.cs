using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);

        ICollection<Recipe> GetRecipeByCategory(int categoryId);

        bool CategoryExists(int id);
    }
}

using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int ownerId);
  //      ICollection<Owner> GetOwnerOfRecipe(int recipeId);

  //      ICollection<Recipe> GetRecipeByOwner(int  ownerId);

        bool OwnerExists(int ownerId);
    }
}

using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int ownerId);
        //      ICollection<Owner> GetOwnerOfRecipe(int recipeId);

        //      ICollection<Recipe> GetRecipeByOwner(int  ownerId);

        Owner GetOwnerByRecipe(int recipeId);

        bool OwnerExists(int ownerId);


        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool Save();
    }
}

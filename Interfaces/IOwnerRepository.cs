using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IOwnerRepository
    {

        // Usage of the Interface Segregation Principle, from SOLID, is evident based on 
        // the amount of references by each method being greater than 0. So, there are 
        // no extra methods that are not implemented. 

        ICollection<Owner> GetOwners();

        Owner GetOwner(int ownerId);

        Owner GetOwnerByRecipe(int recipeId);

        bool OwnerExists(int ownerId);

        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool DeleteOwner(Owner owner);
        bool Save();

        // ICollection<Recipe> GetRecipeByOwner(int  ownerId);
    }
}

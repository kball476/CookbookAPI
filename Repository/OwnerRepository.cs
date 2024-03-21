using cookbook3.Data;
using cookbook3.Interfaces;
using cookbook3.Models;

namespace cookbook3.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner);
            return Save();
        }

        public bool DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            return Save();
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

 //       public ICollection<Owner> GetOwnerOfRecipe(int recipeId)
   //     {
     //       return _context.Recipes.Where(p => p.Recipe.Id == recipeId).Select(o => o.Owner).ToList();
       // }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

  //      public ICollection<Recipe> GetRecipeByOwner(int ownerId)
    //    {
    //        throw new NotImplementedException();
    //    }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOwner(Owner owner)
        {
            _context.Update(owner);
            return Save();
        }
    }
}

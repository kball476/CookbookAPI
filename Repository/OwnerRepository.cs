﻿using cookbook3.Data;
using cookbook3.Interfaces;
using cookbook3.Models;

// Usage of the Interface Segregation Principle, from SOLID, is evident based on 
// all methods from the interface being incoorporated, given substance (i.e. NO
// "throw new notimplementedexception()"), and utilized in our OwnerController.

// The Single Responsibility Principle can also be seen in each method, as they 
// are all responsible for one purpose. For example, CreateOwner() adds a new 
// owner to the data context.

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

        public Owner GetOwnerByRecipe(int recipeId)
        {
            return _context.Recipes.Where(p => p.Id == recipeId).Select(o => o.Owner).FirstOrDefault();
        }

       

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

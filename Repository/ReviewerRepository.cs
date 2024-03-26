using AutoMapper;
using cookbook3.Data;
using cookbook3.Interfaces;
using cookbook3.Models;
using Microsoft.EntityFrameworkCore;

// Usage of the Interface Segregation Principle, from SOLID, is evident based on 
// all methods from the interface being incoorporated, given substance (i.e. NO
// "throw new notimplementedexception()"), and utilized in our ReviewerController.

// The Single Responsibility Principle can also be seen in each method, as they 
// are all responsible for one purpose. For example, GetReviewer() retrieves a 
// single reviewer from the list of reviewers in data context.

namespace cookbook3.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r => r.Id == reviewerId).Include(e => e.Reviews).FirstOrDefault();

        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        //public ICollection<Reviewer> GetReviewersByRecipe(int recipeId)
        //{
        //    return _context.Recipes.Where(r => r.Id == recipeId).Select(o => o.Reviewers).FirstOrDefault();
        //}
        // cant do this because no reviewers in recipe model, is in reviews.

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviewers.Any(r => r.Id == reviewerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
    }
}

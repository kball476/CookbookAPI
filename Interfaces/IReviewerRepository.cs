using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IReviewerRepository
    {
        // Usage of the Interface Segregation Principle, from SOLID, is evident based on 
        // the amount of references by each method being greater than 0. So, there are 
        // no extra methods that are not implemented. 

        ICollection<Reviewer> GetReviewers();

        Reviewer GetReviewer(int reviewerId);

        //ICollection<Reviewer> GetReviewersByRecipe(int recipeId);

        ICollection<Review> GetReviewsByReviewer(int reviewerId);

        bool ReviewerExists(int reviewerId);

        bool CreateReviewer(Reviewer reviewer);

        bool UpdateReviewer(Reviewer reviewer);

        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}

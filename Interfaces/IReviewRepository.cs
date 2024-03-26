using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IReviewRepository
    {
        // Usage of the Interface Segregation Principle, from SOLID, is evident based on 
        // the amount of references by each method being greater than 0. So, there are 
        // no extra methods that are not implemented. 

        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfRecipe(int recipeId);

        bool ReviewExists(int reviewId);

        bool CreateReview(Review review);

        bool UpdateReview(Review review);

        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();
    }
}

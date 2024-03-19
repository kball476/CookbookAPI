using cookbook3.Models;

namespace cookbook3.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfRecipe(int recipeId);

        bool ReviewExists(int reviewId);
    }
}

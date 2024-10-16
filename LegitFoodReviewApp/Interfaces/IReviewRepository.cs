using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review>GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfAFood(int foodId);
        bool ReviewExists(int reviewId);

        bool CreateReview(Review review);
        bool Save();
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool UpdateReview(Review review);
    }
}

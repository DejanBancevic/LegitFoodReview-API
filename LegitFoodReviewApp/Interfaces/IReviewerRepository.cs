using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);

        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        bool ReviewerExists(int id); 
        
        bool CreateReviewer (Reviewer reviewer);
        bool Save();

        bool DeleteReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
    }
}

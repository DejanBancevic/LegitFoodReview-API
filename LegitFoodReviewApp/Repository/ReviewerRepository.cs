using AutoMapper;
using LegitFoodReviewApp.Data;
using LegitFoodReviewApp.Interfaces;
using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Repository
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

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }
        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }
        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(r => r.Id == id);
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); // trenutak da se kod konvertuje u SQL i salje DB-u
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }
    }
}

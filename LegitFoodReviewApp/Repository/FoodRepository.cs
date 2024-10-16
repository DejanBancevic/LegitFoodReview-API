using AutoMapper;
using LegitFoodReviewApp.Data;
using LegitFoodReviewApp.Interfaces;
using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FoodRepository(DataContext context, IMapper mapper) // Ovo je kontstuktor
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<Food> GetFood() 
        { 
            return _context.Food.OrderBy(p => p.Id).ToList();
        }

        public Food GetFoodSingle(int id) // endpoint za trazenje hrane preko id-a
        {
            return _context.Food.Where(p=>p.Id == id).FirstOrDefault(); // FirstofDefault moras stavljati kad ne vracas listu, nego samo jedan clan
        }

        public Food GetFoodSingle(string name) // endpoint za trazenje hrane preko imena
        {
            return _context.Food.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetFoodRating(int foodId) // endpoint za trazenje ratinga preko foodId
        {
            var review = _context.Reviews.Where(p => p.Food.Id == foodId);

            if (review.Count() <= 0)
            
                return 0;
            return ((decimal)review.Sum(r=>r.Rating)/ review.Count());
        }

        public bool FoodExists(int foodId)  // provera da li postoji uopste kolekcija, koja se koristi u kontrolerima
        {
            return _context.Food.Any(p=> p.Id == foodId);
        }

        public bool CreateFood(int ownerId, int categoryId, Food food)
        {
            var foodOwnerEntity = _context.Owners.Where(a=>a.Id==ownerId).FirstOrDefault();
            var category= _context.Categories.Where(a=>a.Id== categoryId).FirstOrDefault();

            var foodOwner = new FoodOwner() // ovo ovako moras da pravis zvog join table-a
            {
                Owner = foodOwnerEntity,
                Food = food,
            };

            _context.Add(foodOwner);

            var foodCategory = new FoodCategory()
            {
                Category = category,
                Food = food,
            };

            _context.Add(foodCategory);

            _context.Add(food);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateFood(Food food, int ownerId, int catergoryId)
        {
            _context.Update(food);
            return Save();
        }

        public bool DeleteFood(Food food)
        {
            _context.Remove(food);
            return Save();
        }

    }
}

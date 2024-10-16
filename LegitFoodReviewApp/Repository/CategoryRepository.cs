using LegitFoodReviewApp.Data;
using LegitFoodReviewApp.Interfaces;
using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private DataContext _context;
        public CategoryRepository(DataContext context) 
        {
            _context = context;
                
        }

        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            // Change Tracker - kad god pravis objekte
            //  add,update,modifying,
            //  connected (najcesce) vs disconnected (EntityState.Added)
            // 
            _context.Add(category);

            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Food> GetFoodByCategory(int categoryId)
        {
            return _context.FoodCategories.Where(e=>e.CategoryId == categoryId).Select(e=>e.Food).ToList();
        }

        public bool Save()
        {
                var saved = _context.SaveChanges(); // trenutak da se kod konvertuje u SQL i salje DB-u
                return saved > 0 ? true: false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

    }
}

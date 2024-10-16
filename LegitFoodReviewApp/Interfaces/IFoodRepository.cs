using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Interfaces
{
    public interface IFoodRepository
    {

        ICollection<Food> GetFood();  // ICollection je editable verzija INumerable sto je najednostavnija kolekcija
        Food GetFoodSingle(int id);
        Food GetFoodSingle(string name);
        decimal GetFoodRating(int foodId);
        bool FoodExists(int foodId);
        bool CreateFood(int ownerId, int categoryId, Food food);
        bool Save();
        bool UpdateFood(Food food, int ownerId, int catergoryId);
        bool DeleteFood(Food food);

    }
}

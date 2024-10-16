using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();

        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAFood(int foodId);
        ICollection<Food> GetFoodByOwner(int ownerId);
        bool OwnerExists(int ownerId);
        bool CreateOwner(Owner owner);
        bool Save();
        bool DeleteOwner(Owner owner);
        bool UpdateOwner(Owner owner);
    }
}

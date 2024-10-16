namespace LegitFoodReviewApp.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<FoodOwner> FoodOwners { get; set; }

        public ICollection<FoodCategory> FoodCategories { get; set; }
    }
}

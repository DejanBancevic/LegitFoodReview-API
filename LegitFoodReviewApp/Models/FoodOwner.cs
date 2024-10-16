namespace LegitFoodReviewApp.Models
{
    public class FoodOwner
    {
        public int FoodId { get; set; }
        public int OwnerId { get; set; }

        public Food Food { get; set; }
        public Owner Owner { get; set; }
    }
}

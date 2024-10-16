namespace LegitFoodReviewApp.Models
{
    public class FoodCategory
    {
        public int FoodId { get; set; }
        public int CategoryId { get; set; }

        public Food Food{ get; set; }

        public Category Category{ get; set; }

    }
}

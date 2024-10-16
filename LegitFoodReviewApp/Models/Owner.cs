namespace LegitFoodReviewApp.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Country Country { get; set; }

        public ICollection<FoodOwner> FoodOwners { get; set; }
    }
}

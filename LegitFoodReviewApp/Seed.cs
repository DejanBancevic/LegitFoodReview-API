using LegitFoodReviewApp.Data;
using LegitFoodReviewApp.Models;

namespace LegitFoodReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.FoodOwners.Any())
            {
                var FoodOwners = new List<FoodOwner>()
                {
                    new FoodOwner()
                    {
                        Food = new Food()
                        {
                            Name = "Pikachu",
                            BirthDate = new DateTime(1903,1,1),
                            FoodCategories = new List<FoodCategory>()
                            {
                                new FoodCategory { Category = new Category() { Name = "Electric"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Pikachu",Text = "Pickahu is the best Food, because it is electric",
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", 
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu",
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {

                            Name = "Jack",
                            Type = "Brocks Gym",

                            Country = new Country()
                            {
                                Name = "Kanto",
                            }
                        }
                    },
                    new FoodOwner()
                    {
                        Food = new Food()
                        {
                            Name = "Squirtle",
                            BirthDate = new DateTime(1903,1,1),
                            FoodCategories = new List<FoodCategory>()
                            {
                                new FoodCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best Food, because it is electric", 
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", 
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", 
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            Name = "Da",
                            Type = "Voce",

                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                    new FoodOwner()
                    {
                        Food = new Food()
                        {
                            Name = "Venasuar",
                            BirthDate = new DateTime(1903,1,1),
                            FoodCategories = new List<FoodCategory>()
                            {
                                new FoodCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venasuar is the best Food, because it is electric", 
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks",
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", 
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {

                            Name = "Ne",
                            Type = "BPov",

                            Country = new Country()
                            {
                                Name = "Millet Town",
                                
                            }
                        }
                    }
                };
                dataContext.FoodOwners.AddRange(FoodOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
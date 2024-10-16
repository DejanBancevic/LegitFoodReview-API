using LegitFoodReviewApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LegitFoodReviewApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Food> Food { get; set; }

        public DbSet<FoodOwner> FoodOwners { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodCategory>()
                .HasKey(pc => new { pc.FoodId, pc.CategoryId });
            modelBuilder.Entity<FoodCategory>()
                .HasOne(p => p.Food)
                .WithMany(pc => pc.FoodCategories)
                .HasForeignKey(p => p.FoodId);
            modelBuilder.Entity<FoodCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.FoodCategories)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<FoodOwner>()
                .HasKey(po => new { po.FoodId, po.OwnerId });
            modelBuilder.Entity<FoodOwner>()
                .HasOne(p => p.Food)
                .WithMany(pc => pc.FoodOwners)
                .HasForeignKey(p => p.FoodId);
            modelBuilder.Entity<FoodOwner>()
                .HasOne(p => p.Owner)
                .WithMany(pc => pc.FoodOwners)
                .HasForeignKey(c => c.OwnerId);

        }
    }
}

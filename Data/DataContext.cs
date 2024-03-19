using cookbook3.Models;
using Microsoft.EntityFrameworkCore;

namespace cookbook3.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Reviewer> Reviewers { get; set; }

        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeCategory>()
                     .HasKey(rc => new { rc.CategoryId, rc.RecipeId });
            modelBuilder.Entity<RecipeCategory>()
                    .HasOne(r => r.Recipe)
                    .WithMany(rc => rc.RecipeCategories)
                    .HasForeignKey(r => r.RecipeId);

            modelBuilder.Entity<RecipeCategory>()
                    .HasOne(r => r.Category)
                    .WithMany(rc => rc.RecipeCategories)
                    .HasForeignKey(c => c.CategoryId);
        }
    }
}

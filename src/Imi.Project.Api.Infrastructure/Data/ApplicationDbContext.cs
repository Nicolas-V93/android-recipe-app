using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Infrastructure.Seeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Imi.Project.Api.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<FavoriteRecipe> FavoriteRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.IngredientId, ri.RecipeId });

            modelBuilder.Entity<FavoriteRecipe>()
                .HasKey(fr => new { fr.RecipeId, fr.ApplicationUserId });

            // Disable cascading
            modelBuilder.Entity<Category>()
                            .HasMany(c => c.Recipes)
                            .WithOne(c => c.Category)
                            .HasForeignKey(c => c.CategoryId)
                            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Diet>()
                            .HasMany(d => d.Recipes)
                            .WithOne(d => d.Diet)
                            .HasForeignKey(d => d.DietId)
                            .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Instruction>()
                        .HasIndex(i => new { i.StepNumber, i.RecipeId })
                        .IsUnique();

            // Seeding
            modelBuilder.Seed();



            base.OnModelCreating(modelBuilder);
        }
    }
}

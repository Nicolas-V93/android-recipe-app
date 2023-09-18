using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Api.Core.Entities
{
    public class Recipe : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        public int PrepTime { get; set; }
        [Required]
        public int CookTime { get; set; }
        [Required]
        public int Servings { get; set; }

        [MaxLength(500)]
        public string? ImgURL { get; set; }


        // Navigation properties
        public Category Category { get; set; }
        public Guid? CategoryId { get; set; }
        public Diet Diet { get; set; }
        public Guid? DietId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public List<Instruction> Instructions { get; set; }
        public List<Review> Reviews { get; set; }
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }
}

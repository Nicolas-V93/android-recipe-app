using System;

namespace Imi.Project.Api.Core.Entities
{
    public class FavoriteRecipe
    {
        public Recipe Recipe { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid RecipeId { get; set; }
        public string ApplicationUserId { get; set; }
    }
}

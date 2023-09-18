using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Imi.Project.Api.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool? HasApprovedTerms { get; set; }
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }
}

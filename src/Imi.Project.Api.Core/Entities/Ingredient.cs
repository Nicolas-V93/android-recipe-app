using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // Navigation properties
        public List<RecipeIngredient> RecipeIngredients { get; set; } 
    }
}

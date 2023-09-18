using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Entities
{
    public class RecipeIngredient
    {
        public Recipe Recipe { get; set; }
        public Guid RecipeId { get; set; }
        public Ingredient Ingredient { get; set; }
        public Guid IngredientId { get; set; }
        public Unit Unit { get; set; }
        public Guid UnitId { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}

using Imi.Project.Api.Core.Dto.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.RecipeIngredient
{
    public class RecipeIngredientResponseDto
    {
        public Guid Id { get; set; }
        public string Ingredient { get; set; }
        public double Amount { get; set; } 
        public string Unit { get; set; }
    }
}

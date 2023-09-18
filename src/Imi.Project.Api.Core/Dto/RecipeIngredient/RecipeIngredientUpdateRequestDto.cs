using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.RecipeIngredient
{
    public class RecipeIngredientUpdateRequestDto
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be larger than 0")]
        public double Amount { get; set; }
        [Required]
        public string Unit { get; set; }
    }
}


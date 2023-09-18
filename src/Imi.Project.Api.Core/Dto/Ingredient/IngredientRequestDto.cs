using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.Ingredient
{
    public class IngredientRequestDto
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(50, ErrorMessage = "{0} must be maximum {1} characters long!")]
        public string Name { get; set; }
    }
}

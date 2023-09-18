using System;
using System.ComponentModel.DataAnnotations;

namespace Imi.Project.Api.Core.Dto.Recipe
{
    public class RecipeRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "{0} can't be more than {1} characters!")]
        public string Title { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "{0} can't be more than {1} characters!")]
        public string Description { get; set; }

        [Required]
        [Range(1, 999, ErrorMessage = "{0} must be between {1} and {2}")]
        public int PrepTime { get; set; }
        [Required]
        [Range(1, 999, ErrorMessage = "{0} must be between {1} and {2}")]
        public int CookTime { get; set; }
        [Required]
        [Range(1, 30, ErrorMessage = "{0} must be between {1} and {2}")]
        public int Servings { get; set; }
        public string ImgURL { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Diet { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Entities
{
    public class Instruction : BaseEntity
    {     
        [Required]
        public int StepNumber { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        // Navigation Properties
        public Recipe Recipe { get; set; }
        public Guid RecipeId { get; set; }
    }
}

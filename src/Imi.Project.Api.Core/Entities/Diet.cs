using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Entities
{
    public class Diet : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public IEnumerable<Recipe> Recipes { get; set; }
    }
}

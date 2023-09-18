using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string Type { get; set; }
    }
}

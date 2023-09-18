using Imi.Project.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Entities
{
    public class Unit : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public UnitType Name { get; set; }
    }
}

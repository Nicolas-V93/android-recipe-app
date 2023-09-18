using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.Review
{
    public class ReviewRequestDto
    {
        [Required]
        [MaxLength(500, ErrorMessage = "{0} can't be more than {1} characters")]
        public string Comment { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}

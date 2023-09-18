using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.Instruction
{
    public class InstructionRequestDto
    {
        [Required]
        [Range(1, 20)]
        public int StepNumber { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "{0} can't be more than {1} characters")]
        public string Description { get; set; }
    }
}

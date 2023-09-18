using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.Instruction
{
    public class InstructionResponseDto
    {
        public Guid Id { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }
    }
}

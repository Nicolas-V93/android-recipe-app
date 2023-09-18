using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Dto.RecipeIngredient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.Recipe
{
    
    public class RecipeDetailsResponseDto : RecipeBaseResponseDto
    {
        [JsonProperty(Order = 98)]
        public IEnumerable<InstructionResponseDto> Instructions { get; set; }
        [JsonProperty(Order = 99)]
        public IEnumerable<RecipeIngredientResponseDto> Ingredients { get; set; }
    }
}

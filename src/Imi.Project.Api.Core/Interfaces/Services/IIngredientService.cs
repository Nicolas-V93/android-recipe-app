using Imi.Project.Api.Core.Dto.Ingredient;
using Imi.Project.Api.Core.Dto.RecipeIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Interfaces.Services
{
    public interface IIngredientService : IBaseService<IngredientResponseDto, IngredientRequestDto>
    {
    }
}

using AutoMapper;
using Imi.Project.Api.Core.Dto.RecipeIngredient;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public RecipeIngredientService(IRecipeIngredientRepository recipeIngredientRepository, 
            IIngredientRepository ingredientRepository,
            IUnitRepository unitRepository,
            IMapper mapper)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _ingredientRepository = ingredientRepository;
            _unitRepository = unitRepository;
            _mapper = mapper;
        }
     
        public async Task<IEnumerable<RecipeIngredientResponseDto>> GetIngredientsAsync(Guid recipeId)
        {
            var ingredients = await _recipeIngredientRepository.GetIngredientsAsync(recipeId);
            return _mapper.Map<IEnumerable<RecipeIngredientResponseDto>>(ingredients);
        }
        public async Task<RecipeIngredientResponseDto> AddAsync(Guid recipeId, RecipeIngredientRequestDto requestDto)
        {
            var unit = await _unitRepository.GetByNameAsync(requestDto.Unit)
                ?? throw new ArgumentException($"Unable to add ingredient because unit '{requestDto.Unit}' does not exist");

            var ingredient = await _ingredientRepository.GetByNameAsync(requestDto.Ingredient);
            var ingredientId = ingredient == null ? Guid.Empty : ingredient.Id;

            if (await RecipeHasIngredientAsync(recipeId, ingredientId))
                throw new ArgumentException($"Recipe with id {recipeId} already has an ingredient of '{requestDto.Ingredient}'");

            if (ingredient == null)
            {  
                ingredient = new Ingredient { Name = requestDto.Ingredient.Trim().ToLower() };
                await _ingredientRepository.AddAsync(ingredient);
            }

            var recipeIngredient = RecipeIngredientProfile.ToEntity(recipeId, ingredient.Id, unit.Id, requestDto.Amount);   
            var result = await _recipeIngredientRepository.AddAsync(recipeIngredient);

            return _mapper.Map<RecipeIngredientResponseDto>(result);

        }
        public async Task UpdateAsync(Guid recipeId, Guid ingredientId, RecipeIngredientUpdateRequestDto requestDto)
        {
            var unit = await _unitRepository.GetByNameAsync(requestDto.Unit)
                ?? throw new ArgumentException($"Unable to update ingredient because unit '{requestDto.Unit}' does not exist");

            var ingredient = await _recipeIngredientRepository.GetIngredientAsync(recipeId, ingredientId);
            RecipeIngredientProfile.Update(ingredient, unit.Id, requestDto.Amount);

            await _recipeIngredientRepository.UpdateAsync(ingredient);
        }      
        public async Task DeleteAsync(Guid recipeId, Guid ingredientId)
        {
            var recipeIngredient = await _recipeIngredientRepository.GetIngredientAsync(recipeId, ingredientId);
            await _recipeIngredientRepository.DeleteAsync(recipeIngredient);
        }
        public async Task<bool> RecipeHasIngredientAsync(Guid recipeId, Guid ingredientId)
        {
            return await _recipeIngredientRepository.RecipeHasIngredientAsync(recipeId, ingredientId);
        }


    }
}

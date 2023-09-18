using AutoMapper;
using Imi.Project.Api.Core.Dto.Ingredient;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<IngredientResponseDto>> ListAllAsync()
        {
            var ingredients = await _ingredientRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<IngredientResponseDto>>(ingredients);
        }
        public async Task<IngredientResponseDto> GetByIdAsync(Guid id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            return _mapper.Map<IngredientResponseDto>(ingredient);
        }
        public async Task<IngredientResponseDto> AddAsync(IngredientRequestDto requestDto)
        {
            var ingredient = _mapper.Map<Ingredient>(requestDto);
            var result = await _ingredientRepository.AddAsync(ingredient);
            return _mapper.Map<IngredientResponseDto>(result);
        }
        public async Task UpdateAsync(Guid id, IngredientRequestDto requestDto)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            _mapper.Map(requestDto, ingredient);
            await _ingredientRepository.UpdateAsync(ingredient);
        }
        public async Task DeleteAsync(Guid id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            await _ingredientRepository.DeleteAsync(ingredient);
        }
        public async Task<bool> EntityExistsAsync(Guid id)
        {
            return await _ingredientRepository.EntityExistsAsync(id);
        }
    }
}

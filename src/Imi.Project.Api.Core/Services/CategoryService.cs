using AutoMapper;
using Imi.Project.Api.Core.Dto.Category;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryResponseDto>> ListAllAsync()
        {
            var categories = await _categoryRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }
        public async Task<CategoryResponseDto> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryResponseDto>(category);
        }
        public async Task<CategoryResponseDto> AddAsync(CategoryRequestDto requestDto)
        {
            var category = _mapper.Map<Category>(requestDto);
            var result = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryResponseDto>(result);
        }
        public async Task UpdateAsync(Guid id, CategoryRequestDto requestDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            _mapper.Map(requestDto, category);
            await _categoryRepository.UpdateAsync(category);
        }
        public async Task DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
        }
        public async Task<bool> EntityExistsAsync(Guid id)
        {
            return await _categoryRepository.EntityExistsAsync(id);
        }

    }
}

using AutoMapper;
using Imi.Project.Api.Core.Dto.Diet;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class DietService : IDietService
    {
        private readonly IDietRepository _dietRepository;
        private readonly IMapper _mapper;

        public DietService(IDietRepository dietRepository, IMapper mapper)
        {
            _dietRepository = dietRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DietResponseDto>> ListAllAsync()
        {
            var diets = await _dietRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<DietResponseDto>>(diets);

        }
        public async Task<DietResponseDto> GetByIdAsync(Guid id)
        {
            var diet = await _dietRepository.GetByIdAsync(id);
            return _mapper.Map<DietResponseDto>(diet);
        }
        public async Task<DietResponseDto> AddAsync(DietRequestDto requestDto)
        {
            var diet = _mapper.Map<Diet>(requestDto);
            var result = await _dietRepository.AddAsync(diet);
            return _mapper.Map<DietResponseDto>(result);
        }
        public async Task UpdateAsync(Guid id, DietRequestDto requestDto)
        {
            var diet = await _dietRepository.GetByIdAsync(id);
            _mapper.Map(requestDto, diet);
            await _dietRepository.UpdateAsync(diet);
        }
        public async Task DeleteAsync(Guid id)
        {
            var diet = await _dietRepository.GetByIdAsync(id);
            await _dietRepository.DeleteAsync(diet);
        }
        public async Task<bool> EntityExistsAsync(Guid id)
        {
            return await _dietRepository.EntityExistsAsync(id);
        }
    }
}

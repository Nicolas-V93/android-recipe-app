using AutoMapper;
using Imi.Project.Api.Core.Dto.Unit;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public UnitService(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UnitResponseDto>> ListAllAsync()
        {
            var units = await _unitRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<UnitResponseDto>>(units);
        }
        public async Task<UnitResponseDto> GetByIdAsync(Guid id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            return _mapper.Map<UnitResponseDto>(unit);
        }
        public async Task<bool> EntityExistsAsync(Guid id)
        {
            return await _unitRepository.EntityExistsAsync(id);
        }
    }

}

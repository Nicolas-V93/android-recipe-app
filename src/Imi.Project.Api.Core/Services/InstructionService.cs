using AutoMapper;
using Imi.Project.Api.Core.Dto.Instruction;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class InstructionService : IInstructionService
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public InstructionService(
            IInstructionRepository instructionRepository, 
            IMapper mapper,
            IUserService userService)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IEnumerable<InstructionResponseDto>> GetInstructionsFromRecipeAsync(Guid recipeId)
        {
            var instructions = await _instructionRepository.GetInstructionsFromRecipeAsync(recipeId);
            return _mapper.Map<IEnumerable<InstructionResponseDto>>(instructions);
        }
        public async Task<InstructionResponseDto> AddInstructionToRecipeAsync(Guid recipeId, InstructionRequestDto requestDto)
        {
            var stepNumbers = await GetStepNumbersAsync(recipeId);

            if (stepNumbers.Any(num => num.Equals(requestDto.StepNumber)))
            {
                throw new InvalidOperationException($"Instruction number {requestDto.StepNumber} already exists for this recipe");
            }
            var instruction = InstructionProfile.ToEntityOnAdd(requestDto, recipeId);
            var result = await _instructionRepository.AddAsync(instruction);
            return _mapper.Map<InstructionResponseDto>(result);
        }
        public async Task UpdateInstructionAsync(Guid recipeId, Guid instructionId, InstructionRequestDto requestDto)
        {
            var instruction = await _instructionRepository.GetInstructionFromRecipeAsync(recipeId, instructionId);
            var stepNumbers = await GetStepNumbersAsync(recipeId);

            if (stepNumbers.Any(num => num.Equals(requestDto.StepNumber) && num != instruction.StepNumber)) 
            {   
               
                throw new InvalidOperationException($"There is already an instruction with number {requestDto.StepNumber} for this recipe");
            }
            
            _mapper.Map(requestDto, instruction);
            await _instructionRepository.UpdateAsync(instruction);
        }
        public async Task DeleteInstructionAsync(Guid recipeId, Guid instructionId)
        {
            var instruction = await _instructionRepository.GetInstructionFromRecipeAsync(recipeId, instructionId);
            await _instructionRepository.DeleteAsync(instruction);
        }
        public async Task<bool> RecipeHasInstruction(Guid recipeId, Guid instructionId)
        {
            return await _instructionRepository.RecipeHasInstruction(recipeId, instructionId);
        }
        public async Task<int[]> GetStepNumbersAsync(Guid recipeId)
        {
            var instructions = await GetInstructionsFromRecipeAsync(recipeId);
            return instructions.OrderBy(i => i.StepNumber)
                               .Select(i => i.StepNumber).ToArray();
        }

    }

}

using AutoMapper;
using Imi.Project.Api.Core.Dto.Review;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper, IUserService userService)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<IEnumerable<ReviewResponseDto>> GetReviewsFromRecipeAsync(Guid recipeId)
        {
            var reviews = await _reviewRepository.GetReviewsFromRecipeAsync(recipeId);
            return _mapper.Map<IEnumerable<ReviewResponseDto>>(reviews);
        }
        public async Task<ReviewResponseDto> AddReviewToRecipeAsync(Guid recipeId, ClaimsPrincipal User, ReviewRequestDto requestDto)
        {
            var review = _mapper.Map<Review>(requestDto);

            review.CreationDate = DateTime.Now;

            review.RecipeId = recipeId;
            review.ApplicationUserId = _userService.GetUserId(User);

            var result = await _reviewRepository.AddAsync(review);
            return _mapper.Map<ReviewResponseDto>(result);
        }
        public async Task UpdateReviewAsync(Guid recipeId, Guid reviewId, ReviewRequestDto requestDto)
        {
            var review = await _reviewRepository.GetReviewFromRecipeAsync(recipeId, reviewId);
            _mapper.Map(requestDto, review);

            await _reviewRepository.UpdateAsync(review);
        }
        public async Task DeleteReviewAsync(Guid recipeId, Guid reviewId)
        {
            var review = await _reviewRepository.GetReviewFromRecipeAsync(recipeId, reviewId);
            await _reviewRepository.DeleteAsync(review);
        }
        public async Task<bool> RecipeHasReview(Guid recipeId, Guid reviewId)
        {
            return await _reviewRepository.RecipeHasReview(recipeId, reviewId);
        }

        public async Task<bool> AuthorizeAsync(Guid reviewId, string policyName)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            //return await _userService.AuthorizeAsync(review, policyName);
            return false;
        }

        public async Task<bool> AuthorizeAsync(Guid reviewId, ClaimsPrincipal user, OperationAuthorizationRequirement requirement)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            return await _userService.AuthorizeAsync(user, review, requirement);
        }
    }
}

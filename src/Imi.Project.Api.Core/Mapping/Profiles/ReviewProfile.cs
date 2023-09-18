using AutoMapper;
using Imi.Project.Api.Core.Dto.Review;
using Imi.Project.Api.Core.Dto.User;
using Imi.Project.Api.Core.Entities;
using System;

namespace Imi.Project.Api.Core.Mapping.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewRequestDto, Review>()
                .BeforeMap((src, dest) => dest.UpdateTimeStamp = DateTime.Now)
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment.Trim()));

            CreateMap<Review, ReviewResponseDto>()
                 .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserResponseDto
                 {
                     Id = src.ApplicationUser.Id,
                     Username = src.ApplicationUser.UserName
                 }));

        }
    }
}

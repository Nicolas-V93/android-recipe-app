using Imi.Project.Api.Core.Dto.User;
using System;

namespace Imi.Project.Api.Core.Dto.Review
{
    public class ReviewResponseDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateTimeStamp { get; set; }
        public UserResponseDto User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Dto.User
{
    public class LoginUserRequestDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Please log in using your email address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

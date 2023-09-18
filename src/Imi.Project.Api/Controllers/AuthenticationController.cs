using Imi.Project.Api.Core.Dto.User;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Helpers.CustomClaimTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace Imi.Project.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationController(
            SignInManager<ApplicationUser> signInManager, 
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto registration)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            ApplicationUser newUser = new ApplicationUser 
            { 
                UserName = registration.Username.Trim(),
                Email = registration.Email.Trim(),
                HasApprovedTerms = registration.HasApprovedTerms,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registration.Password);

            if (!result.Succeeded) 
            { 
                foreach (var error in result.Errors)    
                { 
                    ModelState.AddModelError(error.Code, error.Description); 
                } 
                return BadRequest(ModelState); 
            }

            await _userManager.AddToRoleAsync(newUser, "User");

            await _userManager.AddClaimAsync(newUser, new Claim(CustomClaimType.RegistrationDate, DateTime.UtcNow.ToString("yy-MM-dd")));
            await _userManager.AddClaimAsync(newUser, new Claim(CustomClaimType.Sub, newUser.Id));
            await _userManager.AddClaimAsync(newUser, new Claim(CustomClaimType.HasApprovedTerms, newUser.HasApprovedTerms.ToString() ?? ""));
            await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Name, newUser.UserName));
            await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Email, newUser.Email));
            await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, "User"));

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto requestDto)
        {
            var applicationUser = await _userManager.FindByEmailAsync(requestDto.Email.Trim());
            if (applicationUser is null) return NotFound($"User with email '{requestDto.Email}' does not exist");

            var result = await _signInManager.PasswordSignInAsync(applicationUser.UserName, requestDto.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded) return Unauthorized();

            var token = await GetJwtSecurityTokenAsync(applicationUser);         
            var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginUserResponseDto
            {
                Token = serializedToken
            });
        }

        private async Task<JwtSecurityToken> GetJwtSecurityTokenAsync(ApplicationUser applicationUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretForKey"]));
            var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Loading claims
            var claims = new List<Claim>();
            var userClaims = await _userManager.GetClaimsAsync(applicationUser);
            claims.AddRange(userClaims);

            // Create JWT Token
            var expirationDays = _configuration.GetValue<int>("JWT:TokenExpirationDays");
            var jwtToken = new JwtSecurityToken
                (
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(expirationDays)),
                signinCredentials
                );

            return jwtToken;

        }
    }
}

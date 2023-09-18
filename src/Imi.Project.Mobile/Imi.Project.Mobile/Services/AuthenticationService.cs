using Imi.Project.Mobile.Dto;
using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Imi.Project.Mobile.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBaseRepository _baseRepository;

        public AuthenticationService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<string> Login(AuthenticationRequest request)
        {
            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = Constants.API.LoginEndPoint,
            };

            var result = await _baseRepository.PostAsync<AuthenticationRequest, LoginResponseDto>(request, builder.ToString());
            return result.AuthToken;
        }
        public async Task Register(RegisterRequest request)
        {
            var registerDto = new RegisterRequestDto
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword = request.Password,
                DateOfBirth = request.DateOfBirth.ToString("yyyy-MM-dd"),
                HasApprovedTerms = request.TermsAccepted
            };

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = Constants.API.RegisterEndPoint,
            };

            await _baseRepository.PostAsync(registerDto, builder.ToString());
        }
        public async Task SetAuthToken(string token)
        {
            await SecureStorage.SetAsync(Constants.TokenKey, token);
        }
        public async Task<string> GetAuthToken()
        {
            return await SecureStorage.GetAsync(Constants.TokenKey);
        }
        public bool ClearToken()
        {
            var removed = SecureStorage.Remove(Constants.TokenKey);
            return removed;
        }
    }
}

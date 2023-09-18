using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services
{
    public class DietService : IDietService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IAuthenticationService _authenticationService;

        public DietService(IBaseRepository baseRepository,
            IAuthenticationService authenticationService)
        {
            _baseRepository = baseRepository;
            _authenticationService = authenticationService;
        }

        public async Task<IEnumerable<Diet>> GetAllDiets()
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = Constants.API.DietsEndpoint,
            };

            var result = await _baseRepository.GetAllAsync<Diet>(builder.ToString(), authToken);
            return result;
        }
    }
}

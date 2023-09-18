using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services
{
    public class UnitService : IUnitService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IAuthenticationService _authenticationService;

        public UnitService(IBaseRepository baseRepository, IAuthenticationService authenticationService)
        {
            _baseRepository = baseRepository;
            _authenticationService = authenticationService;
        }

        public async Task<IEnumerable<Unit>> GetAllUnits()
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = Constants.API.UnitsEndPount,
            };

            var result = await _baseRepository.GetAllAsync<Unit>(builder.ToString(), authToken);
            return result;
        }
    }
}

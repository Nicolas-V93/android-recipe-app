using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IAuthenticationService _authenticationService;

        public CategoryService(IBaseRepository baseRepository,
            IAuthenticationService authenticationService)
        {
            _baseRepository = baseRepository;
            _authenticationService = authenticationService;
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var authToken = await _authenticationService.GetAuthToken();

            UriBuilder builder = new UriBuilder(Constants.API.BaseUrl)
            {
                Path = Constants.API.CategoriesEndPoint,
            };

            var result = await _baseRepository.GetAllAsync<Category>(builder.ToString(), authToken);
            return result;
        }
    }
}

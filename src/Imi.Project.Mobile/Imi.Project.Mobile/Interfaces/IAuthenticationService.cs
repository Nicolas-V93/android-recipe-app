using Imi.Project.Mobile.Models;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> Login(AuthenticationRequest request);
        Task Register(RegisterRequest request);
        Task SetAuthToken(string token);
        Task<string> GetAuthToken();
        bool ClearToken();
    }
}

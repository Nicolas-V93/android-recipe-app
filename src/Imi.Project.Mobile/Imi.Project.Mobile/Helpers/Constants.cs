using Xamarin.Forms;

namespace Imi.Project.Mobile.Helpers
{
    public class Constants
    {
        public const string EmailClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public const string UsernameClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        public const string UserIdClaim = "sub";
        public const string TokenKey = "AuthToken";

        public class API
        {
            public static readonly string BaseUrl = Device.RuntimePlatform == Device.Android ?
                "https://10.0.2.2:5001" : "https://localhost:5001";

            public const string RecipesEndpoint = "api/recipes";
            public const string DietsEndpoint = "api/diets";
            public const string CategoriesEndPoint = "api/categories";
            public const string RegisterEndPoint = "api/auth/register";
            public const string LoginEndPoint = "api/auth/login";
            public const string AccountEndpoint = "api/me";
            public const string UnitsEndPount = "api/units";
        }

        public class Messages
        {
            public const string RecipesChangedMessage = "RecipesChanged";
        }
    }
}

using System.IdentityModel.Tokens.Jwt;

namespace Imi.Project.Mobile.Helpers
{
    public static class HelperMethods
    {

        public static JwtSecurityToken DecodeJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            return jwtSecurityToken;
        }
    }
}

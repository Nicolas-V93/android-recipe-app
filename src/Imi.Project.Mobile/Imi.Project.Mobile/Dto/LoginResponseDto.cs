using Newtonsoft.Json;

namespace Imi.Project.Mobile.Dto
{
    public class LoginResponseDto
    {
        [JsonProperty("token")]
        public string AuthToken { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace AspnetRunBasics.Models
{
    public class LoginRequestViewModel
    {
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

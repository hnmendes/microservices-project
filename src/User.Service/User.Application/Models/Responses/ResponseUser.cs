using System.Text.Json.Serialization;

namespace User.Application.Models.Requests
{
    public class ResponseUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("user_name")]
        public string Username { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}

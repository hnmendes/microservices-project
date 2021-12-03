using System.Text.Json.Serialization;

namespace User.Application.Models.Responses
{
    public class AddUserRoleResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }        
    }
}

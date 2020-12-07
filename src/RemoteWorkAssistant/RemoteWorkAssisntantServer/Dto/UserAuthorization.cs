using System.Text.Json.Serialization;

namespace RemoteWorkAssisntantServer.Dto
{
    public class UserAuthorization
    {
        [JsonPropertyName("email")]
        public string MailAddress { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

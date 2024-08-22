using System.Text.Json.Serialization;

namespace TaskManagement.Application.DTOs;

public class LoginResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
}
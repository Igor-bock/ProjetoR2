using BlazorIdentity.Models;
using System.Net.Http.Json;

namespace BlazorIdentity.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<CurrentUser> CurrentUserInfo()
    {
        var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/auth/currentuserinfo");
        return result;
    }
}

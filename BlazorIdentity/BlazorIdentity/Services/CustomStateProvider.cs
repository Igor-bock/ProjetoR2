using Blazored.LocalStorage;
using BlazorIdentity.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorIdentity.Services;

public class CustomStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public CustomStateProvider(HttpClient p_httpClient, ILocalStorageService p_localStorage)
    {
        _httpClient = p_httpClient;
        _localStorage = p_localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var m_token = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrWhiteSpace(m_token))
            return _anonymous;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", m_token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(m_token), "jwtAuthType")));
    }
}

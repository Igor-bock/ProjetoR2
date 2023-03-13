using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace REI_MAUI;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		var m_identity = new ClaimsIdentity();
		try
		{
			var m_usuario = await SecureStorage.GetAsync("accounttoken");
			if (m_usuario != null)
			{
				var claims = new[] { new Claim(ClaimTypes.Name, "ffUser") };
				m_identity = new ClaimsIdentity(claims, "Server authentication");
			}
		}
		catch (HttpRequestException ex)
		{
			Console.WriteLine("Request failed:" + ex.ToString());
		}

		return new AuthenticationState(new ClaimsPrincipal(m_identity));
	}
}

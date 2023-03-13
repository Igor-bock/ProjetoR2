using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using REI_MAUI.Data;

namespace REI_MAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();
		
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<WebAuthenticationBrowser>();
		builder.Services.AddTransient(sp => new OidcClient(new OidcClientOptions
		{
#if ANDROID
			Authority = "https://d655-179-109-192-138.sa.ngrok.io/",
#elif WINDOWS
			Authority = "https://localhost:5001",
#endif
            ClientId = "rei_blazor",
			RedirectUri = "reimaui://",
			Scope = "openid profile esperanto",
			Browser = sp.GetRequiredService<WebAuthenticationBrowser>()
		}));
		builder.Services.AddSingleton<AccessTokenHttpMessageHandler>();
		builder.Services.AddTransient<HttpClient>(sp =>
			new HttpClient(sp.GetRequiredService<AccessTokenHttpMessageHandler>())
			{
				BaseAddress = new Uri("https://localhost:9001")
			});

		builder.Services.AddOidcAuthentication(options =>
		{
			options.ProviderOptions.Authority = "https://localhost:5001";
			options.ProviderOptions.ClientId = "rei_blazor";
			options.ProviderOptions.DefaultScopes.Add("openid");
			options.ProviderOptions.DefaultScopes.Add("profile");
			options.ProviderOptions.DefaultScopes.Add("esperanto");
			options.ProviderOptions.PostLogoutRedirectUri = "reimaui://authentication/logout-callback";
			options.ProviderOptions.RedirectUri = "reimaui://authentication/login-callback";
			options.ProviderOptions.ResponseType = "code";
		});
		//builder.Services.AddAuthorizationCore();
		//builder.Services.AddScoped<CustomAuthenticationStateProvider>();
		//builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());
		//builder.Services.AddScoped<IRemoteAuthenticationService, ();

		return builder.Build();
	}
}

using IdentityModel.OidcClient;
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
			Authority = "https://d655-179-109-192-138.sa.ngrok.io/",//"https://localhost:5001",
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

		return builder.Build();
	}
}

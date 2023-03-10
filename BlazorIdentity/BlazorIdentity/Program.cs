using BlazorIdentity;
using Maui_Lib.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:9001") });

builder.Services.AddHttpClient<IEtiquetaDataService, EtiquetaDataService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient(Microsoft.Extensions.Options.Options.DefaultName, cl =>
{
    cl.BaseAddress = new Uri("https://localhost:9001/r2glass/");
})
    .AddHttpMessageHandler(sp =>
    {
        var m_handler = sp.GetService<AuthorizationMessageHandler>()
        .ConfigureHandler(
            authorizedUrls: new[] { "https://localhost:9001" },
            scopes: new[] { "esperanto" }
        );

        return m_handler;
    });

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient(Microsoft.Extensions.Options.Options.DefaultName));

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);

    options.UserOptions.NameClaim = "name";
    options.UserOptions.RoleClaim = "role";
    options.UserOptions.ScopeClaim = "scope";
});

await builder.Build().RunAsync();
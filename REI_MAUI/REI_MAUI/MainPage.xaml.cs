using IdentityModel.OidcClient;

namespace REI_MAUI;

public partial class MainPage : ContentPage
{
	protected OidcClient c_OidcClient { get; }
	private readonly HttpClient c_HttpClient;

	public MainPage(OidcClient p_oidcClient, HttpClient p_httpClient)
	{
		InitializeComponent();

		c_OidcClient = p_oidcClient;
		c_HttpClient = p_httpClient;
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		try
		{
			var m_loginResult = await c_OidcClient.LoginAsync(new LoginRequest());
			await DisplayAlert("Resultado do Token", $"O token de acesso é:\n\n {m_loginResult.AccessToken}", "Fechar");

            Preferences.Set(Constantes.C_AcessToken, m_loginResult.AccessToken);

			var m_json = await c_HttpClient.GetStringAsync("r2glass/clientes");
			await DisplayAlert("r2glass/clientes", m_json, "Fechar");
		}
		catch (Exception ex)
		{
			await DisplayAlert("Um erro aconteceu", ex.ToString(), "Ok");
		}
	}
}

using Maui_Lib.Models;
using IdentityModel.Client;
using System.Text.Json;

namespace BlazorIdentity.Services;

public class TokenService
{
    public HttpClient c_HttpClient;

    public TokenService(HttpClient p_HttpClient)
    {
        c_HttpClient = p_HttpClient;
    }

    private async Task<string> cm_SolicitarNovoToken()
    {
        try
        {
            var m_discovery = await c_HttpClient.GetDiscoveryDocumentAsync("https://localhost:5001/.well-known/openid-configuration");

            if (m_discovery.IsError)
                throw new ApplicationException(m_discovery.Error);

            var m_tokenResponse = await c_HttpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Scope = "esperanto",
                ClientSecret = "api_secret",
                Address = m_discovery.TokenEndpoint,
                ClientId = "rei_api"
            });
            //var m_tokenClient = new TokenClient(c_HttpClient, new TokenClientOptions { ClientId = "rei_blazor", ClientSecret = "segredo_do_rei_blazor" });
            //var m_usuario = c_HttpClient.GetAsync(m_discovery.UserInfoEndpoint);
            //var m_teste = await m_usuario;
            //var m_user = await m_teste.Content.ReadAsStringAsync();
            //var m_tokenResponse = await m_tokenClient.RequestPasswordTokenAsync("adm", "lippert", "esperanto");
            if (m_tokenResponse.IsError)
                throw new ApplicationException(m_tokenResponse.Error);

            return m_tokenResponse.AccessToken;//m_tokenResponse.AccessToken;
        }
        catch(Exception ex)
        {
            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<List<Cliente>> CM_GetClientesAsync(string p_token)
    {
        //var m_token = await cm_SolicitarNovoToken();
        c_HttpClient.SetBearerToken(p_token);

        var m_response = await c_HttpClient.GetAsync("https://localhost:9001/r2glass/clientes");
        m_response.EnsureSuccessStatusCode();

        using var m_responseContent = await m_response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<List<Cliente>>(m_responseContent);
    }
}

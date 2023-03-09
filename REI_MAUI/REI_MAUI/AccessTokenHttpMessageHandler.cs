using IdentityModel.Client;
using IdentityModel.OidcClient;

namespace REI_MAUI;

public class AccessTokenHttpMessageHandler : DelegatingHandler
{
    protected OidcClient c_OidcClient { get; }

    public AccessTokenHttpMessageHandler(OidcClient p_oidcClient) : base(new HttpClientHandler())
    {
        c_OidcClient = p_oidcClient;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var m_token = Preferences.Get(Constantes.C_AcessToken, null);
        if (string.IsNullOrEmpty(m_token) == false)
        {
            request.SetBearerToken(m_token);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        var m_response = await base.SendAsync(request, cancellationToken);

        //if(m_response.StatusCode != System.Net.HttpStatusCode.Unauthorized)
        //{
        //    var m_result = await c_OidcClient.LoginAsync(new LoginRequest());
        //    request.SetBearerToken(m_result.AccessToken);

        //    Preferences.Set(Constantes.C_AcessToken, m_result.AccessToken);
        //    request.SetBearerToken(m_result.AccessToken);

        //    return await base.SendAsync(request, cancellationToken);
        //}

        return m_response;
    }
}

using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using System.Text.Json;

namespace REI_MAUI;

internal class WebAuthenticationBrowser : IdentityModel.OidcClient.Browser.IBrowser
{
#if WINDOWS
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
            WebAuthenticatorResult m_auth_result = await REI_MAUI.WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));
            var m_authorize_response = ToRawIdentityUrl(options.EndUrl, m_auth_result);

            return new BrowserResult
            {
                Response = m_authorize_response
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new BrowserResult
            {
                ResultType = BrowserResultType.UnknownError,
                Error = ex.ToString()
            };
        }
    }
#else
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
            WebAuthenticatorResult m_auth_result = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));
            var m_authorize_response = ToRawIdentityUrl(options.EndUrl, m_auth_result);

            return new BrowserResult
            {
                Response = m_authorize_response
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new BrowserResult
            {
                ResultType = BrowserResultType.UnknownError,
                Error = ex.ToString()
            };
        }
    }
#endif

    public string ToRawIdentityUrl(string p_redirectUrl, WebAuthenticatorResult p_result)
    {
        List<string> m_propriedades = new List<string>();
        foreach (var prop in p_result.Properties)
        {
            if (prop.Key == "state")
            {
#if WINDOWS
                var m_valor = prop.Value.Replace("%3A", ":");
                var m_json = JsonDocument.Parse(m_valor);
                m_propriedades.Add($"{prop.Key}={m_json.RootElement.GetProperty("state").GetString()}");
#elif ANDROID
                m_propriedades.Add($"{prop.Key}={prop.Value}");
#endif
            }
            else
                m_propriedades.Add($"{prop.Key}={prop.Value}");
        }

        var values = string.Join("&", m_propriedades);
        return $"{p_redirectUrl}#{values}";
    }
}

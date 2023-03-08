using System.Net.Security;
using System.Xml.Serialization;

namespace R2API.Services;

//public abstract class HttpClientService<TipoEnvio>
public static class HttpClientService<TipoRetorno>
{
    public static async Task<TipoRetorno?> CM_EnviarClasseTipoT(HttpMethod p_metodo, string p_endereco, object? p_classe = null)
    {
        try
        {
            var m_clientHandler = new HttpClientHandler();
            m_clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => { return true; };

            using var m_httpClient = new HttpClient(m_clientHandler);
            HttpResponseMessage m_resposta;
            HttpRequestMessage m_mensagemEnvio = new HttpRequestMessage();
            string m_xml = string.Empty;
            TipoRetorno? m_retorno = default;

            FormUrlEncodedContent? m_content = null;
            if(p_classe != null)
            {
                var m_listaKeyValue = new List<KeyValuePair<string, string>>();
                foreach (var m_propriedade in p_classe.GetType().GetProperties())
                {
                    var m_valor = cm_ObterValorDoTipoT(p_classe, m_propriedade.Name);
                    m_listaKeyValue.Add(new KeyValuePair<string, string?>(m_propriedade.Name, m_valor?.ToString() ?? default));
                }
                m_content = new FormUrlEncodedContent(m_listaKeyValue.AsEnumerable());
            }

            switch (p_metodo.Method)
            {
                case "GET":
                    m_resposta = await m_httpClient.GetAsync(p_endereco);
                    m_resposta.EnsureSuccessStatusCode();
                    m_xml = await m_resposta.Content.ReadAsStringAsync();
                    using (var m_reader = new StringReader(m_xml))
                        m_retorno = (TipoRetorno?)new XmlSerializer(typeof(TipoRetorno)).Deserialize(m_reader);
                    break;
                case "POST":
                    m_resposta = await m_httpClient.PostAsync(p_endereco, m_content ?? null);
                    m_resposta.EnsureSuccessStatusCode();
                    m_xml = await m_resposta.Content.ReadAsStringAsync();
                    using (var m_reader = new StringReader(m_xml))
                        m_retorno = (TipoRetorno?)new XmlSerializer(typeof(TipoRetorno)).Deserialize(m_reader);
                    break;
                case "DELETE":
                    m_resposta = await m_httpClient.PostAsync(p_endereco, m_content ?? null);
                    m_resposta.EnsureSuccessStatusCode();
                    break;
                case "PUT":
                    m_resposta = await m_httpClient.PostAsync(p_endereco, m_content ?? null);
                    m_resposta.EnsureSuccessStatusCode();
                    m_xml = await m_resposta.Content.ReadAsStringAsync();
                    using (var m_reader = new StringReader(m_xml))
                        m_retorno = (TipoRetorno?)new XmlSerializer(typeof(TipoRetorno)).Deserialize(m_reader);
                    break;
            };

            return m_retorno;
        }
        catch (Exception)
        {
            return default;
        }
    }

    private static object cm_ObterValorDoTipoT(object p_objeto, string p_nomePropriedade)
    {
        var m_tipo = p_objeto.GetType();
        var m_propriedade = m_tipo.GetProperty(p_nomePropriedade);
        var m_valor = m_propriedade.GetValue(p_objeto, null);
        return m_valor;
    }
}

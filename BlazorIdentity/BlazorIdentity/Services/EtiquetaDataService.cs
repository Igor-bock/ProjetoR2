using BlazorIdentity.Models;
using System.Text.Json;

namespace BlazorIdentity.Services;

public class EtiquetaDataService : IEtiquetaDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<Etiqueta> _logger;
    private readonly JsonSerializerOptions _options;

    public EtiquetaDataService(HttpClient httpClient, ILogger<Etiqueta> logger)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _logger = logger;
    }

    public async Task<Etiqueta> CM_MoverEtiquetaParaSetor(int p_usuario, int p_empresa, int p_codigo_etiqueta, int p_codigoSetor)
    {
        var m_url = $"https://localhost:9001/R2Glass/mover_etiqueta?p_usuario={p_usuario}&p_empresa={p_empresa}&p_codigo_etiqueta={p_codigo_etiqueta}&p_codigoSetor={p_codigoSetor}";
        var m_resultadoDoWS = await _httpClient.GetStreamAsync(m_url);
        return await JsonSerializer.DeserializeAsync<Etiqueta>(m_resultadoDoWS, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

    }

    public async Task<Etiqueta> CM_ObterEtiqueta(int p_usuario, int p_empresa, int p_codigo_etiqueta)
    {
        var m_url = $"https://localhost:9001/R2Glass/etiquetas?p_usuario={p_usuario}&p_empresa={p_empresa}&p_codigo_etiqueta={p_codigo_etiqueta}";
        var m_resultadoDoWS = await _httpClient.GetStreamAsync(m_url);
        return await JsonSerializer.DeserializeAsync<Etiqueta>(m_resultadoDoWS, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    }
}

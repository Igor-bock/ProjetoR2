using Maui_Lib.Models;
using Maui_Lib.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Class_Lib.Services;

public class EtiquetaDataService : IEtiquetaDataService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public EtiquetaDataService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<Etiqueta> CM_MoverEtiquetaParaSetor(int p_usuario, int p_empresa, int p_codigo_etiqueta, int p_codigoSetor)
    {
        var m_url = $"https://localhost:9001/R2Glass/mover_etiqueta?p_usuario={p_usuario}&p_empresa={p_empresa}&p_codigo_etiqueta={p_codigo_etiqueta}&p_codigoSetor={p_codigoSetor}";
        var m_resultadoDoWS = await _httpClient.GetStreamAsync(m_url);
        return await JsonSerializer.DeserializeAsync<Etiqueta>(m_resultadoDoWS, _options);

    }

    public async Task<Etiqueta> CM_ObterEtiqueta(int p_usuario, int p_empresa, int p_codigo_etiqueta)
    {
        var m_url = $"https://localhost:9001/R2Glass/etiquetas?p_usuario={p_usuario}&p_empresa={p_empresa}&p_codigo_etiqueta={p_codigo_etiqueta}";
        var m_resultadoDoWS = await _httpClient.GetStreamAsync(m_url);
        return await JsonSerializer.DeserializeAsync<Etiqueta>(m_resultadoDoWS, _options);
    }
}
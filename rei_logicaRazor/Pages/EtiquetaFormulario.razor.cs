using Microsoft.AspNetCore.Components;
using rei_logicaRazor.Services;
using rei_logicaRazor.Models;

namespace rei_logicaRazor.Pages;

public partial class EtiquetaFormulario : ComponentBase
{
    [Inject]
    public HttpClient C_HttpClient { get; set; }
    public IEtiquetaDataService C_EtiquetaDataService { get; set; }

    public Etiqueta? C_Etiqueta { get; set; }

    public Etiqueta? _etiquetaMovida;

    public string Titulo = "Formulario Etiqueta";
    public string Descricao = "Pagina que exibe o formulário de uma etiqueta";

    public string? c_CodEtiqueta { get; set; }
    public string? c_CodSetor { get; set; }

    protected override void OnInitialized()
    {
        C_EtiquetaDataService = new EtiquetaDataService(C_HttpClient);
    }

    public async Task c_BuscaEtiqueta(string p_codigo_etiqueta)
    {
        var m_codigo_etiqueta_int = int.Parse(p_codigo_etiqueta);
        var m_etiqueta = await C_EtiquetaDataService.CM_ObterEtiqueta(2, 1, m_codigo_etiqueta_int);

        C_Etiqueta = m_etiqueta;
    }

    public async Task c_MoverEtiqueta(string p_codigo_status)
    {
        var m_codigo_etiqueta_int = C_Etiqueta.cd_codigo;
        var m_codigo_setor_int = int.Parse(p_codigo_status);

        _etiquetaMovida = await C_EtiquetaDataService.CM_MoverEtiquetaParaSetor(2, 1, m_codigo_etiqueta_int, m_codigo_setor_int);
    }

}
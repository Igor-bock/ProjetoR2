using Microsoft.AspNetCore.Components;
using BlazorIdentity.Models;
using BlazorIdentity.Services;

namespace BlazorIdentity.Pages;

public partial class EtiquetaFormulario
{
    [Inject]
    public IEtiquetaDataService C_EtiquetaDataService { get; set; }

    public Etiqueta? C_Etiqueta { get; set; }

    public Etiqueta? _etiquetaMovida;

    public string Titulo = "Formulario Etiqueta";
    public string Descricao = "Pagina que exibe o formulário de uma etiqueta";

    public string c_CodEtiqueta { get; set; }
    public string c_CodSetor { get; set; }

    public async Task c_BuscaEtiqueta(string p_codigo_etiqueta)
    {
        var m_codigo_etiqueta_int = Int32.Parse(p_codigo_etiqueta);
        var m_etiqueta = await C_EtiquetaDataService.CM_ObterEtiqueta(2, 1, m_codigo_etiqueta_int);

        C_Etiqueta = m_etiqueta;
    }

    public async Task c_MoverEtiqueta(string p_codigo_status)
    {
        var m_codigo_etiqueta_int = C_Etiqueta.cd_codigo;
        var m_codigo_setor_int = Int32.Parse(p_codigo_status);

        _etiquetaMovida = await C_EtiquetaDataService.CM_MoverEtiquetaParaSetor(2, 1, m_codigo_etiqueta_int, m_codigo_setor_int);
    }

}
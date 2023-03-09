using BlazorIdentity.Models;

namespace BlazorIdentity.Services;

public interface IEtiquetaDataService
{
    Task<Etiqueta> CM_ObterEtiqueta(int p_usuario, int p_empresa, int p_codigo_etiqueta);
    Task<Etiqueta> CM_MoverEtiquetaParaSetor(int p_usuario, int p_empresa, int p_codigo_etiqueta, int p_codigoSetor);
}
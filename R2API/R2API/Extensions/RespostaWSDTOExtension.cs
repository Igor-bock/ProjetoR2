using R2API.Models;

namespace R2API.Extensions;

public static class RespostaWSDTOExtension
{
    public static void CMX_GerarExceptionSeNecessario(this RespostaWSDTO p_resposta)
    {
        if (string.IsNullOrEmpty(p_resposta.ErroMensagem))
            return;

        throw new Exception(p_resposta.ErroMensagem);
    }
}

namespace rei_esperantolib.Extensions;

public static class ExceptionExtensions
{
    public static string CMX_RetornaMensagemComHost(this Exception p_exception)
    {
        if (p_exception.Source == "Npgsql"
            && p_exception.Message.Contains("Failed to connect to"))
        {
            var m_primeiroIndex = p_exception.Message.LastIndexOf("to");
            var m_length = p_exception.Message.Length;
            var m_host =
                p_exception.Message.Substring(m_primeiroIndex, m_length - m_primeiroIndex)
                .Replace("to", string.Empty)
                .Trim();
            return string.Format(Utils.Constantes.C_IMPOSSIVEL_CONECTAR_AO_HOST, m_host);
        }
        return Utils.Constantes.C_ERRO_COM_A_CONEXAO;
    }

    public static string CMX_RetornaHost(this Exception p_exception)
    {
        if (p_exception.Source == "Npgsql"
            && p_exception.Message.Contains("Failed to connect to"))
        {
            var m_primeiroIndex = p_exception.Message.LastIndexOf("to");
            var m_length = p_exception.Message.Length;
            return p_exception.Message.Substring(m_primeiroIndex, m_length - m_primeiroIndex)
                .Replace("to", string.Empty)
                .Trim();
        }
        return Utils.Constantes.C_ERRO_COM_A_CONEXAO;
    }
}

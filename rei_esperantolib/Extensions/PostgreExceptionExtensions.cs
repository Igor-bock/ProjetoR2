namespace rei_esperantolib.Extensions;

public static class PostgreExceptionExtensions
{
    public static string CMX_RetornaMensagemDeErro(this Npgsql.PostgresException m_exception)
    {
        var m_primeiroIndex = m_exception.MessageText.IndexOf("\"");
        var m_ultimoIndex = m_exception.MessageText.LastIndexOf("\"");
        var m_nomeTabelaOuDatabase = m_exception.MessageText.Substring(m_primeiroIndex, m_ultimoIndex - m_primeiroIndex)
            .Replace("\\", string.Empty)
            .Replace("\"", string.Empty);

        switch (m_exception.SqlState)
        {
            case "3D000": return string.Format(Utils.Constantes.C_DATABASE_NAO_EXISTE, m_nomeTabelaOuDatabase);
            case "42P01": return string.Format(Utils.Constantes.C_TABELA_NAO_ENCONTRADA, m_nomeTabelaOuDatabase);
            default: return string.Empty;
        }
    }
}

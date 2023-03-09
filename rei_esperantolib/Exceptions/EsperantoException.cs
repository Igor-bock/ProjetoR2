namespace rei_esperantolib.Exceptions;

public class EsperantoException : Exception
{
    public E_ESPERANTO_EXCEPTION C_EXCEPTION { get; private set; }
    public string C_PROPRIEDADE { get; private set; }
    public string C_TABELA { get; private set; }
    public int C_CODIGO_INTERNO { get; private set; }
    public Exception C_EXCEPTION_RECEBIDA { get; private set; }

    public override string Message
    {
        get
        {
            switch (C_EXCEPTION)
            {
                case E_ESPERANTO_EXCEPTION.PROPRIEDADE_NAO_ENCONTRADA: return string.Format(ExceptionsConstantes.C_PROPRIEDADE_NAO_ENCONTRADA, C_PROPRIEDADE);
                case E_ESPERANTO_EXCEPTION.SOLICITACAO_COM_PENDENCIA: return string.Format(ExceptionsConstantes.C_SOLICITACOES_PENDENTES, C_TABELA, C_CODIGO_INTERNO);
                case E_ESPERANTO_EXCEPTION.IMPOSSIVEL_CONECTAR_COM_HOST: return string.Format(ExceptionsConstantes.C_IMPOSSIVEL_CONECTAR_COM_HOST, C_EXCEPTION_RECEBIDA.CMX_RetornaHost());
                case E_ESPERANTO_EXCEPTION.PROPRIEDADE_NAO_ALTERADA: return string.Format(ExceptionsConstantes.C_PROPRIEDADE_NAO_ALTERADA, C_PROPRIEDADE);
                default: return ExceptionsConstantes.C_ERRO_INESPERADO;
            }
        }
    }

    public EsperantoException(E_ESPERANTO_EXCEPTION p_exception)
    {
        C_EXCEPTION = p_exception;
    }
    public EsperantoException(E_ESPERANTO_EXCEPTION p_exception, string p_propriedade)
    {
        C_EXCEPTION = p_exception;
        C_PROPRIEDADE = p_propriedade;
    }
    public EsperantoException(E_ESPERANTO_EXCEPTION p_exception, string p_tabela, int p_codigo_interno)
    {
        C_EXCEPTION = p_exception;
        C_TABELA = p_tabela;
        C_CODIGO_INTERNO = p_codigo_interno;
    }
    public EsperantoException(E_ESPERANTO_EXCEPTION p_exception, Exception p_exceptionRecebida)
    {
        C_EXCEPTION_RECEBIDA = p_exceptionRecebida;
    }
}

namespace rei_esperantolib.Exceptions;

public class JsonHelperDeserializeObjectException<T> : ApplicationException
{
    private const string c_MensagemErro = "Erro ao tentar deserializar a string '{0}' no tipo '{1}'";
    public string C_JsonADeserializar { get; set; }
    public Type C_TipoATransformar { get; private set; }

    public JsonHelperDeserializeObjectException(string p_conteudoQueSeTentouDeserializar)
        : base(string.Format(c_MensagemErro, p_conteudoQueSeTentouDeserializar, typeof(T).ToString()))
    {

    }
}

namespace rei_esperantolib.Utils;

public class JsonHelper
{
    public static string SerializeObject(object value)
    {
        var m_retorno = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        m_retorno = m_retorno.Replace("&", "%26");
        return m_retorno;
    }

    public static string SerializeObjectComFormatacao(object value) => Newtonsoft.Json.JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented);

    public static T DeserializeObject<T>(string value)
    {
        try
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }
        catch
        {
            throw new JsonHelperDeserializeObjectException<T>(value);
        }
    }
}

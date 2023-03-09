namespace rei_esperantolib.Extensions;

public static class JObjectExtensions
{
    public static string CMX_RetornaJsonCompletoComConnectionString(this JObject p_jObject, ConnectionString p_conn)
    {
        var m_novaConnectionString =
            $"Host={p_conn.Host};" +
            $"Port={p_conn.Port};" +
            $"Database={p_conn.Database};" +
            $"User Id={p_conn.Id};" +
            $"Password={p_conn.Password};" +
            $"Application Name={p_conn.Application}";

        p_jObject
            .SelectToken("ConnectionStrings")
            .SelectToken("DefaultConnection")
            .Replace(m_novaConnectionString);

        return p_jObject.ToString();
    }
}

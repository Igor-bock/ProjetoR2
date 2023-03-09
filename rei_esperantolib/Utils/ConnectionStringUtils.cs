namespace rei_esperantolib.Utils;

public class ConnectionStringUtils
{
    public ConnectionString CM_GetConnectionStringDoArquivoAppSettingsJson()
    {
        var m_conn = CM_GetConnectionString().Split(";");
        if (m_conn.Length != 6)
            throw new ApplicationException("Não foi possível encontrar a Connection String.");

        for (int i = 0; i < m_conn.Length; i++)
        {
            var m_sinalDeIgual = m_conn[i].IndexOf("=");
            m_conn[i] = m_conn[i].Replace(m_conn[i].Remove(m_sinalDeIgual, m_conn[i].Length - m_sinalDeIgual), string.Empty).Remove(0, 1);
        }
        return new ConnectionString(m_conn);
    }

    public string CM_GetConnectionString()
    {
        JObject m_jsonDaConnectionString = JObject.Parse(File.ReadAllText("appsettings.json"));
        return m_jsonDaConnectionString.SelectToken("ConnectionStrings").SelectToken("DefaultConnection").ToString();
    }
}

namespace rei_esperantolib.Extensions;

public static class ConnectionStringExtensions
{
    public static string CMX_RetornaConnectionString(this ConnectionString p_conn)
        => $"Host={p_conn.Host};" +
           $"Port={p_conn.Port};" +
           $"Database={p_conn.Database};" +
           $"User Id={p_conn.Id};" +
           $"Password={p_conn.Password};" +
           $"Application Name={p_conn.Application}";
}

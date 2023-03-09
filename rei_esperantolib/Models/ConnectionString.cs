namespace rei_esperantolib.Models;

public class ConnectionString
{
    public ConnectionString() { }
    public ConnectionString(string[] p_properties = null)
    {
        Host = p_properties[0];
        Port = p_properties[1];
        Database = p_properties[2];
        Id = p_properties[3];
        Password = p_properties[4];
        Application = p_properties[5];
    }

    public string Host { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
    public string Id { get; set; }
    public string Password { get; set; }
    public string Application { get; set; }
}

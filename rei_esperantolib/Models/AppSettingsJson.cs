namespace rei_esperantolib.Models;

public class AppSettingsJson
{
    public Config.AppSettings AppSettings { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public string AllowedHosts { get; set; }
    public EnderecoIdentityServer IdentityServer { get; set; }
}

public class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}

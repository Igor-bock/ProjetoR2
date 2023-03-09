using Microsoft.Extensions.Configuration;

namespace rei_esperantolib.Utils;

public class Configuracao
{
    public static IConfiguration CM_ObterConfiguracao()
        => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

    public static string CM_ObterEnderecoIdentityServer()
        => CM_ObterConfiguracao().GetSection("IdentityServer").GetValue(typeof(string), "C_Endereco").ToString();
}

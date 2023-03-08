using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using R2API.Models;
using R2API.Services;
using System.Xml.Serialization;

namespace R2API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class R2GlassController : Controller
{
    private readonly ILogger<R2GlassController> c_Logger;

    public R2GlassController(ILogger<R2GlassController> p_logger)
    {
        c_Logger = p_logger;
    }

    [HttpGet]
    [Route("clientes")]
    public async Task<List<Cliente>> CM_BuscarClientes()
    {
        c_Logger.LogInformation("Consultando clientes em {DT}", DateTime.Now.ToLongDateString());

        var m_endereco = string.Format("{0}/{1}", Constantes.C_EnderecoLocalhost, Constantes.C_EnderecoClientes);
        var m_retorno = await HttpClientService<Clientes>.CM_EnviarClasseTipoT(HttpMethod.Post, m_endereco, null);

        return m_retorno.Cliente;
    }

    [HttpGet]
    [Route("cliente_por_nome")]
    public async Task<List<Cliente>> CM_BuscarClientePorNome(string p_nome)
    {
        c_Logger.LogInformation("Consultando cliente pelo nome {0} em {DT}", p_nome, DateTime.Now.ToLongDateString());

        var m_endereco = string.Format("{0}/{1}", Constantes.C_EnderecoLocalhost, Constantes.C_EnderecoClientePorNome);
        var m_envio = new EnvioCliente { ds_nome = p_nome };
        var m_retorno = await HttpClientService<Clientes>.CM_EnviarClasseTipoT(HttpMethod.Post, m_endereco, m_envio);

        return m_retorno.Cliente;
    }

    public class EnvioCliente
    {
        public string ds_nome { get; set; }
    }

    public class XMLGambi
    {
        public string ds_xml { get; set; }
        public string metodo { get; set; }
    }

    public class IntDto
    {
        public int cd_codigo { get; set; }
    }

    [HttpPost]
    [Route("salvar_cliente")]
    public async Task CM_SalvarCliente(Cliente p_cliente)
    {
        c_Logger.LogInformation("Salvando o registro {0} as {1}", p_cliente.cd_codigo, DateTime.Now.ToLongTimeString());

        if (p_cliente == null)
            return;

        var m_endereco = string.Format("{0}/{1}", Constantes.C_EnderecoLocalhost, Constantes.C_EnderecoSalvarCliente);

        using var m_xml = new StringWriter();
        new XmlSerializer(p_cliente.GetType()).Serialize(m_xml, p_cliente);
        var m_gambi = new XMLGambi { ds_xml = m_xml.ToString(), metodo = "ADICIONAR" };
        await HttpClientService<Cliente>.CM_EnviarClasseTipoT(HttpMethod.Post, m_endereco, m_gambi);
    }

    [HttpPut]
    [Route("editar_cliente")]
    public async Task CM_EditarCliente(Cliente p_cliente)
    {
        c_Logger.LogInformation("Editando o registro {0} as {1}", p_cliente.cd_codigo, DateTime.Now.ToLongTimeString());

        if (p_cliente == null)
            return;

        var m_endereco = string.Format("{0}/{1}", Constantes.C_EnderecoLocalhost, Constantes.C_EnderecoSalvarCliente);

        using var m_xml = new StringWriter();
        new XmlSerializer(p_cliente.GetType()).Serialize(m_xml, p_cliente);
        var m_gambi = new XMLGambi { ds_xml = m_xml.ToString(), metodo = "EDITAR" };
        await HttpClientService<Cliente>.CM_EnviarClasseTipoT(HttpMethod.Put, m_endereco, m_gambi);
    }

    [HttpDelete]
    [Route("apagar_cliente/{p_cd_codigo}")]
    public async Task CM_ApagarCliente(int p_cd_codigo)
    {
        c_Logger.LogInformation("Deletando o registro {0} as {1}", p_cd_codigo, DateTime.Now.ToLongTimeString());

        if (p_cd_codigo == 0)
            return;

        var m_endereco = string.Format("{0}/{1}", Constantes.C_EnderecoLocalhost, Constantes.C_EnderecoApagarCliente);

        await HttpClientService<string>.CM_EnviarClasseTipoT(HttpMethod.Delete, m_endereco, new IntDto { cd_codigo = p_cd_codigo });
    }

    [HttpGet]
    [Route("wcf")]
    public async Task<string> CM_TestarWCF()
    {
        c_Logger.LogInformation("Testando WCF em {DT}", DateTime.Now.ToLongDateString());

        return await new ReiglassWCF.Service1Client().GetDataAsync("Olá mundo!");
    }
}

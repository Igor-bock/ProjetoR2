namespace rei_identityserver.Controllers;

public class ConnectionStringController : Controller
{
    [HttpGet]
    public IActionResult CM_AlterarConnectionString()
    {
        var m_model = new ConnectionStringUtils().CM_GetConnectionStringDoArquivoAppSettingsJson();

        return View("Alterar", m_model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CM_AlterarConnectionString([FromForm] ConnectionString p_conn)
    {
        if (ModelState.IsValid)
        {
            JObject m_jsonDaConnectionString = JObject.Parse(System.IO.File.ReadAllText("appsettings.json"));
            var m_json = m_jsonDaConnectionString.CMX_RetornaJsonCompletoComConnectionString(p_conn);
            var m_connectionString = p_conn.CMX_RetornaConnectionString();

            using var m_contexto = new ApplicationContext(m_connectionString);
            var m_conexaoEstabelecida = m_contexto.Database.CanConnect();
            if (m_conexaoEstabelecida)
            {
                System.IO.File.WriteAllText("appsettings.json", m_json);

                TempData["Alerta"] = "Conexão alterada com sucesso";

                return View("Alterar");
            }

            TempData["Erro"] = Constantes.C_CONEXAO_NAO_ESTABELECIDA;
        }
        return View("Alterar", p_conn);
    }
}

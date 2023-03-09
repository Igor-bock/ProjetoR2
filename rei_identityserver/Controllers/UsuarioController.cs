using AutoMapper;
using rei_esperantolib.Models.Email;
using System.Globalization;
using System.IO.Pipelines;
using System.Text;
using System.Text.Json;

namespace rei_identityserver.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly CargosUtils _cargoUtils;
    private readonly ServicosUtils _servicosUtils;
    private readonly ClaimUtils _claimUtils;
    private readonly IEnvioEmail _envioEmail;

    public UsuarioController(
        IMapper p_mapper,
        UserManager<Usuario> p_userManager,
        RoleManager<IdentityRole> p_roleManager,
        IEnvioEmail envioEmail)
    {
        _mapper = p_mapper;
        _userManager = p_userManager;
        _roleManager = p_roleManager;
        _servicosUtils = new ServicosUtils();
        _cargoUtils = new CargosUtils();
        _claimUtils = new ClaimUtils();
        _envioEmail = envioEmail;
    }

    [HttpGet]
    [Route("usuarios")]
    public string CM_ObtemUsuarios()
    {
        using var m_modelo = new ApplicationContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationContext>());
        var m_usuarios = m_modelo.Users.OrderBy(a => a.UserName).Where(a => a.UserName != "adm").ToList();
        return JsonSerializer.Serialize(m_usuarios);
    }

    [HttpGet]
    [Route("usuario")]
    public async Task<string> CM_ObtemUsuario(string p_id)
    {
        var m_usuario = await _userManager.FindByIdAsync(p_id);
        return JsonSerializer.Serialize(m_usuario);
    }

    [HttpPost]
    [Route("claims")]
    public async Task<string> CM_ObterClaimsDoUsuario()
    {
        var m_json = await CM_ObterJsonDoBodyAsync();
        var m_model = JsonSerializer.Deserialize<Usuario>(m_json);
        var m_claimsUsuario = await _userManager.GetClaimsAsync(m_model);
        return JsonSerializer.Serialize(m_claimsUsuario);
    }

    [HttpPost]
    [Route("criar")]
    public async Task<string> CM_CriarUsuario()
    {
        var m_json = await CM_ObterJsonDoBodyAsync();

        var m_model = JsonSerializer.Deserialize<RegistroUsuarioModel>(m_json);

        var m_nomeDaRole = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(m_model.Cargo.ToString().ToLower());
        var m_novosServicos = _claimUtils.CM_RetornaClaimsDeServicosAtivos(m_model.ServicosAtivos);
        var m_identityRole = await _roleManager.FindByNameAsync(m_nomeDaRole);
        if (m_identityRole == null)
        {
            m_identityRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = m_nomeDaRole };
            await _roleManager.CreateAsync(m_identityRole);

            foreach (var m_claim in m_novosServicos)
                await _roleManager.AddClaimAsync(m_identityRole, m_claim);

            var m_cargo = _claimUtils.CM_RetornaClaimsDeCargoAtivo(m_model.Cargo.GetValueOrDefault());
            await _roleManager.AddClaimAsync(m_identityRole, m_cargo);
        }

        var m_usuario = _mapper.Map<Usuario>(m_model);
        m_usuario.PasswordHash = _userManager.PasswordHasher.HashPassword(m_usuario, m_model.Senha);
        m_usuario.LockoutEnabled = true;
        m_usuario.TwoFactorEnabled = true;
        var m_resultado = await _userManager.CreateAsync(m_usuario);

        var m_token = await _userManager.GenerateEmailConfirmationTokenAsync(m_usuario);
        var m_linkConfirmacao = Url.Action("CM_ConfirmarEmail", "Account", new { p_token = m_token, p_email = m_usuario.Email }, Request.Scheme);

        var m_mensagem = new Mensagem(new string[] { m_usuario.Email }, "Link de confirmação do E-mail R2 Esperanto", m_linkConfirmacao, null);
        await _envioEmail.CM_PreparacaoEEnvioDeEmailAsync(m_mensagem);

        await _userManager.AddToRoleAsync(m_usuario, m_nomeDaRole);
        await _userManager.AddClaimsAsync(m_usuario, m_novosServicos);
        return JsonSerializer.Serialize(m_resultado);
    }

    [HttpPost]
    [Route("alterar")]
    public async Task<string> CM_AlterarUsuario()
    {
        var m_json = await CM_ObterJsonDoBodyAsync();
        var m_model = Newtonsoft.Json.JsonConvert.DeserializeObject<AlteraUsuarioModel>(m_json);
        var m_usuario = await _userManager.FindByNameAsync(m_model.Nome);
        m_usuario.PhoneNumber = m_model.Telefone;

        var m_claimsAntigas = await _userManager.GetClaimsAsync(m_usuario);
        await _userManager.RemoveClaimsAsync(m_usuario, m_claimsAntigas);
        var m_claimsNovas = _claimUtils.CM_RetornaClaimsDeServicosAtivos(m_model.ServicosAtivos);
        await _userManager.AddClaimsAsync(m_usuario, m_claimsNovas);

        var m_resultado = await _userManager.UpdateAsync(m_usuario);

        return JsonSerializer.Serialize(m_resultado);
    }

    [HttpPost]
    [Route("deletar")]
    public async Task<string> CM_DeletarUsuario()
    {
        var m_json = await CM_ObterJsonDoBodyAsync();
        var m_model = Newtonsoft.Json.JsonConvert.DeserializeObject<AlteraUsuarioModel>(m_json);
        var m_usuario = await _userManager.FindByNameAsync(m_model.Nome);

        var m_resultado = await _userManager.DeleteAsync(m_usuario);

        return JsonSerializer.Serialize(m_resultado);
    }

    public async Task<string> CM_ObterJsonDoBodyAsync()
    {
        ReadResult requestBodyInBytes = await Request.BodyReader.ReadAsync();
        Request.BodyReader.AdvanceTo(requestBodyInBytes.Buffer.Start, requestBodyInBytes.Buffer.End);
        return Encoding.UTF8.GetString(requestBodyInBytes.Buffer.FirstSpan);
    }
}

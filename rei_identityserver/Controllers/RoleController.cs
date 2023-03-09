using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using rei_esperantolib.Models;
using rei_esperantolib.Utils;
using System.IO.Pipelines;
using System.Text;
using System.Text.Json;

namespace rei_identityserver.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ClaimUtils _claimUtils;
    private readonly ServicosUtils _servicosUtils;
    private readonly CargosUtils _cargosUtils;

    public RoleController(RoleManager<IdentityRole> p_roleManager)
    {
        _roleManager = p_roleManager;
        _claimUtils = new ClaimUtils();
        _servicosUtils = new ServicosUtils();
        _cargosUtils = new CargosUtils();
    }

    [HttpGet]
    [Route("roles")]
    public string CM_ObterRoles()
    {
        var m_roles = _roleManager.Roles;
        return JsonSerializer.Serialize(m_roles);
    }

    [HttpGet]
    [Route("role")]
    public async Task<string> CM_ObterRole(string p_id)
    {
        var m_identityRole = await _roleManager.FindByIdAsync(p_id);
        var m_claims = await _roleManager.GetClaimsAsync(m_identityRole);

        var m_cargos = _cargosUtils.CM_ObtemCargosDisponiveis(m_claims.Where(a => a.Type == "cargo").ToList());
        var m_servicos = _servicosUtils.CM_ObtemServicosDeIntegracaoDisponiveis(m_claims.Where(a => a.Type == "servico").ToList());

        var m_role = new Role()
        {
            Id = m_identityRole.Id,
            Name = m_identityRole.Name,
            NormalizedName = m_identityRole.NormalizedName,
            ConcurrencyStamp = m_identityRole.ConcurrencyStamp,
            C_Cargos = m_cargos,
            C_Servicos = m_servicos
        };
        
        return JsonSerializer.Serialize(m_role);
    }

    [HttpPost]
    [Route("criar")]
    public async Task<RespostaIdentityUsuario> CM_CriarRole()
    {
        var m_json = await CM_ObterJsonDoBodyAsync();

        var m_role = JsonSerializer.Deserialize<Role>(m_json);
        var m_roleJaExiste = await _roleManager.RoleExistsAsync(m_role.Name);
        if (m_roleJaExiste)
            return new RespostaIdentityUsuario { Succeeded = false };

        var m_identityRole = new IdentityRole { Name = m_role.Name };
        var m_respostaRole = await _roleManager.CreateAsync(m_identityRole);
        if(m_respostaRole.Succeeded)
        {
            var m_claims = _claimUtils.CM_RetornaClaimsDeServicosECargos(m_role);

            foreach (var m_claim in m_claims)
                await _roleManager.AddClaimAsync(m_identityRole, m_claim);

            return new RespostaIdentityUsuario { Succeeded = true };
        }
        return new RespostaIdentityUsuario { Succeeded = false };
    }

    [HttpPost]
    [Route("alterar")]
    public async Task<string> CM_AlterarRole()
    {
        var m_json = await CM_ObterJsonDoBodyAsync();
        var m_identityRole = JsonSerializer.Deserialize<Role>(m_json);
        var m_role = await _roleManager.FindByIdAsync(m_identityRole.Id);
        var m_claimsAntigas = await _roleManager.GetClaimsAsync(m_role);
        foreach(var m_claim in m_claimsAntigas)
            await _roleManager.RemoveClaimAsync(m_role, m_claim);

        var m_novasClaims = _claimUtils.CM_RetornaClaimsDeServicosECargos(m_identityRole);
        foreach (var m_claim in m_novasClaims)
            await _roleManager.AddClaimAsync(m_role, m_claim);

        var m_resultado = await _roleManager.UpdateAsync(m_role);
        return JsonSerializer.Serialize(m_resultado);
    }

    public async Task<string> CM_ObterJsonDoBodyAsync()
    {
        ReadResult requestBodyInBytes = await Request.BodyReader.ReadAsync();
        Request.BodyReader.AdvanceTo(requestBodyInBytes.Buffer.Start, requestBodyInBytes.Buffer.End);
        return Encoding.UTF8.GetString(requestBodyInBytes.Buffer.FirstSpan);
    }
}

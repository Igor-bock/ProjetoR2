namespace rei_esperantolib.Utils;

public class ClaimUtils
{
    public List<Claim> CM_RetornaClaimsDeServicosECargos(Role p_role)
    {
        var m_cargos = new List<Claim>();
        foreach (var m_item in p_role.C_Cargos.Where(a => a.C_Ativo == true))
            m_cargos.Add(new Claim("cargo", m_item.C_Cargo.ToString()));

        var m_servicos = new List<Claim>();
        foreach (var m_item in p_role.C_Servicos.Where(a => a.Ativo == true))
            m_servicos.Add(new Claim("servico", m_item.ServicosIntegracao.ToString()));

        var m_claims = new List<Claim>();
        m_claims.AddRange(m_cargos);
        m_claims.AddRange(m_servicos);

        return m_claims;
    }

    public IEnumerable<Claim> CM_RetornaClaimsDeServicosAtivos(List<Servicos> p_servicos)
    {
        foreach (var m_item in p_servicos.Where(a => a.Ativo == true))
            yield return new Claim("servicos", m_item.ServicosIntegracao.ToString());
    }

    public Claim CM_RetornaClaimsDeCargoAtivo(E_CARGO p_cargo)
        => new("cargo", p_cargo.ToString());
}

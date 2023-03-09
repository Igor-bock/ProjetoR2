namespace rei_esperantolib.Utils;

public class ServicosUtils
{
    public List<Servicos> CM_ObtemServicosDeIntegracaoDisponiveis(IList<Claim> m_claims = null)
    {
        var m_listaDeServicos = new List<Servicos>
        {
            new Servicos{ Ativo = false, ServicosIntegracao = E_MODOS_INTEGRACAO.Whatsapp },
            new Servicos{ Ativo = false, ServicosIntegracao = E_MODOS_INTEGRACAO.MidiaNFC },
            new Servicos{ Ativo = false, ServicosIntegracao = E_MODOS_INTEGRACAO.Fidelimax },
            new Servicos{ Ativo = false, ServicosIntegracao = E_MODOS_INTEGRACAO.CygnusWMS },
            new Servicos{ Ativo = false, ServicosIntegracao = E_MODOS_INTEGRACAO.SAP }
        };

        if (m_claims == null)
            return m_listaDeServicos;

        foreach (var m_claim in m_claims)
            foreach (var m_servico in m_listaDeServicos)
            {
                if (m_servico.ServicosIntegracao == CM_ObtemIntegrador(m_claim.Value))
                    m_servico.Ativo = true;
            }

        return m_listaDeServicos;
    }

    public E_MODOS_INTEGRACAO CM_ObtemIntegrador(string p_integrador)
        => p_integrador switch
        {
            "Fidelimax" => E_MODOS_INTEGRACAO.Fidelimax,
            "SAP" => E_MODOS_INTEGRACAO.SAP,
            "CygnusWMS" => E_MODOS_INTEGRACAO.CygnusWMS,
            "MidiaNFC" => E_MODOS_INTEGRACAO.MidiaNFC,
            "Whatsapp" => E_MODOS_INTEGRACAO.Whatsapp,
            _ => E_MODOS_INTEGRACAO.SinIntegracion
        };
}

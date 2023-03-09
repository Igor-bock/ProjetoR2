namespace rei_esperantolib.Utils;

public class CargosUtils
{
    public List<Cargo> CM_ObtemCargosDisponiveis(IList<Claim> m_claims = null)
    {
        var m_listaDeCargos = new List<Cargo>
        {
            new Cargo{ C_Ativo = false, C_Cargo = E_CARGO.ADMINISTRADOR },
            new Cargo{ C_Ativo = false, C_Cargo = E_CARGO.GERENTE },
            new Cargo{ C_Ativo = false, C_Cargo = E_CARGO.EMPREGADO },
            new Cargo{ C_Ativo = false, C_Cargo = E_CARGO.VISITANTE },
        };

        if (m_claims == null)
            return m_listaDeCargos;

        foreach (var m_claim in m_claims)
            foreach (var m_cargo in m_listaDeCargos)
            {
                if (m_cargo.C_Cargo == CM_ObtemCargosDisponiveis(m_claim.Value))
                    m_cargo.C_Ativo = true;
            }

        return m_listaDeCargos;
    }

    public E_CARGO CM_ObtemCargosDisponiveis(string p_cargo)
        => p_cargo switch
        {
            "ADMINISTRADOR" => E_CARGO.ADMINISTRADOR,
            "GERENTE" => E_CARGO.GERENTE,
            "EMPREGADO" => E_CARGO.EMPREGADO,
            "VISITANTE" => E_CARGO.VISITANTE,
            _ => E_CARGO.VISITANTE
        };

    public List<Cargo> CM_ObtemListaDeCargos()
        => new List<Cargo>
        {
            new Cargo { C_Ativo = false, C_Cargo = E_CARGO.ADMINISTRADOR },
            new Cargo { C_Ativo = false, C_Cargo = E_CARGO.GERENTE },
            new Cargo { C_Ativo = false, C_Cargo = E_CARGO.EMPREGADO },
            new Cargo { C_Ativo = false, C_Cargo = E_CARGO.VISITANTE },
        };
}

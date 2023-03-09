namespace rei_esperantolib.Models;

public class ConfiguracaoModel
{
    public PropriedadeConfiguracao ds_propriedade { get; set; }
    public string ds_valor { get; set; }
}

public class PropriedadeConfiguracao
{
    public E_MODOS_INTEGRACAO? ds_destino { get; set; }
    public string ds_nome { get; set; }
    public int cd_ambiente { get; set; }
}
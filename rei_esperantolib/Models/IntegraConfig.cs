namespace rei_esperantolib.Models;

public class IntegraConfig
{
    [Key]
    public int cd_codigo { get; set; }
    public int cd_destino { get; set; }
    public string ds_nome { get; set; }
    public string ds_valor { get; set; }
    public string ds_tipo { get; set; }
    public int? cd_ambiente { get; set; }
    public int cd_reitech { get; set; }

    [NotMapped]
    public E_Ambiente cd_ambienteEnum
    {
        get { return (E_Ambiente)cd_ambiente.GetValueOrDefault(); }
        set { cd_ambiente = (int)value; }
    }
}
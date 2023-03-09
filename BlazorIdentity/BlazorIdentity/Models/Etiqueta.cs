namespace BlazorIdentity.Models;

public class Etiqueta : IRespostaWSDTO
{
    public int cd_codigo { get; set; }
    public string ds_cliente { get; set; }
    public string ds_setor { get; set; }
    public decimal? vl_m2 { get; set; }
    public string ds_imagem { get; set; }
    public string ErroMensagem { get; set; }
    public int? ErroCodigo { get; set; }
}

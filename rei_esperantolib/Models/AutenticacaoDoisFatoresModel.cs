namespace rei_esperantolib.Models;

public class AutenticacaoDoisFatoresModel
{
    [Required]
    [DataType(DataType.Text)]
    public string C_CodigoDoisFatores { get; set; }
    public bool C_Lembrar { get; set; }
    public string C_UrlRetorno { get; set; }
}

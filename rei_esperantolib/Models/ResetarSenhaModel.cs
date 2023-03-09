namespace rei_esperantolib.Models;

public class ResetarSenhaModel
{
    [Required]
    [DataType(DataType.Password)]
    public string C_Senha { get; set; }

    [DataType(DataType.Password)]
    [Compare("C_Senha", ErrorMessage = "As senhão não são iguais!")]
    public string C_ConfirmaSenha { get; set; }

    public string C_Email { get; set; }
    public string C_Token { get; set; }
}

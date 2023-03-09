namespace rei_esperantolib.Models;

public class EsqueciASenhaModel
{
    [Required(ErrorMessage = "O campo de e-mail é necessário.")]
    [EmailAddress]
    public string Email { get; set; }
}

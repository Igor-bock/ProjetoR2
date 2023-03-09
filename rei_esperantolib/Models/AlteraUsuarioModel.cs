namespace rei_esperantolib.Models;

public class AlteraUsuarioModel
{
    [Display(Name = "Código")]
    public string Id { get; set; }

    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "Campo de e-mail é obrigatório!")]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    [Display(Name = "Número de telefone")]
    [StringLength(30, ErrorMessage = "O campo deve conter no máximo 30 caracteres!")]
    [MinLength(8, ErrorMessage = "O campo deve conter no mínimo 8 caracteres!")]
    public string Telefone { get; set; }

    [Display(Name = "Serviços disponíveis")]
    [Required(ErrorMessage = "Selecione um serviço!")]
    public List<Servicos> ServicosAtivos { get; set; }
}

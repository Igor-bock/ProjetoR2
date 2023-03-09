namespace rei_esperantolib.Models;

public class RegistroUsuarioModel
{
    public RegistroUsuarioModel() { }

    [Display(Name = "Nome")]
    [Required(ErrorMessage = "Nome é obrigatório!", AllowEmptyStrings = false)]
    public string Nome { get; set; }

    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "E-mail é obrigatório!", AllowEmptyStrings = false)]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "Senha")]
    [Required(ErrorMessage = "Campo de senha é obrigatório!", AllowEmptyStrings = false)]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Display(Name = "Confirmar senha")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Campo de senha é obrigatório!", AllowEmptyStrings = false)]
    [Compare("Senha", ErrorMessage = "As senhas não são iguais!")]
    public string ConfirmaSenha { get; set; }

    [Display(Name = "Serviços disponíveis")]
    [Required(ErrorMessage = "Selecione um serviço!")]
    public List<Servicos> ServicosAtivos { get; set; }

    [Display(Name = "Cargo do usuário")]
    [Required(ErrorMessage = "Selecione um cargo!")]
    public E_CARGO? Cargo { get; set; }
}

public class Servicos
{
    public E_MODOS_INTEGRACAO ServicosIntegracao { get; set; }
    public bool Ativo { get; set; }
}

public class Cargo
{
    public E_CARGO C_Cargo { get; set; }
    public bool C_Ativo { get; set; }
}

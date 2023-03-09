namespace rei_esperantolib.Models;

public class EnderecoIdentityServer
{
    [Required(ErrorMessage = "O endereço para conexão com IdentityServer é necessário!", AllowEmptyStrings = true)]
    public string C_Endereco { get; set; }
}

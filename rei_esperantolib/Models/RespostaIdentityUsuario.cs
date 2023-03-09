namespace rei_esperantolib.Models;

public class RespostaIdentityUsuario
{
    public bool Succeeded { get; set; }
    public IEnumerable<IdentityError> Errors { get; set; }
}

namespace rei_esperantolib.Models.Email;

public interface IEnvioEmail
{
    public Task CM_PreparacaoEEnvioDeEmailAsync(Mensagem p_mensagem);
}

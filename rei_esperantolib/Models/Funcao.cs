namespace rei_esperantolib.Models;

public class Funcao : IdentityRole
{
    override public string Name { get; set; }

    public IList<Claim> Claims { get; set; }
}

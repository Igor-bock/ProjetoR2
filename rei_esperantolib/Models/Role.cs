namespace rei_esperantolib.Models;

public class Role : IdentityRole
{
    public override string Name { get => base.Name; set => base.Name = value; }

    public List<Servicos> C_Servicos { get; set; }
    public List<Cargo> C_Cargos { get; set; }
}

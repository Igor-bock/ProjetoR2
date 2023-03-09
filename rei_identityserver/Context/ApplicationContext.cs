namespace rei_identityserver.Context;

public class ApplicationContext : IdentityDbContext<Usuario>
{
    private string c_ConnectionString { get; set; } = new ConnectionStringUtils().CM_GetConnectionString();

    public ApplicationContext(string p_ConnectionString)
    {
        c_ConnectionString = p_ConnectionString;
        OnConfiguring(new DbContextOptionsBuilder<ApplicationContext>());
    }

    public static ApplicationContext CM_GetNovoContexto()
        => new ApplicationContext(new rei_esperantolib.Utils.ConnectionStringUtils().CM_GetConnectionString());

    public ApplicationContext(DbContextOptions<ApplicationContext> p_options) : base(p_options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(c_ConnectionString);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UsuarioConfiguration());
        builder.ApplyConfiguration(new CargoConfiguration());
    }
}

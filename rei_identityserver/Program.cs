using Microsoft.AspNetCore.Http.Features;
using rei_esperantolib.Models.Email;
using rei_identityserver.CustomTokenProviders;

IConfiguration Configuration = Configuracao.CM_ObterConfiguracao();

var m_assembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

var builder = WebApplication.CreateBuilder(args);

var m_esperanto_cors = "esperanto_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: m_esperanto_cors,
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .Build();
        });
});

var m_emailConfig = Configuration
    .GetSection("Email")
    .Get<Email>();

builder.Services.Configure<FormOptions>(opt =>
{
    opt.ValueLengthLimit = int.MaxValue;
    opt.MultipartBodyLengthLimit = int.MaxValue;
    opt.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddSingleton(m_emailConfig);
builder.Services.AddScoped<IEnvioEmail, EnvioEmail>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.Configure<ConfirmarEmailTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromMinutes(2));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;

    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedEmail = true;

    options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";

    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
})
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<ConfirmarEmailTokenProvider<Usuario>>("emailconfirmation");

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<Usuario>()
    .AddConfigurationStore(opt =>
    {
        opt.ConfigureDbContext = context => context.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
            sql => sql.MigrationsAssembly(m_assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = context => context.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
            sql => sql.MigrationsAssembly(m_assembly));
    })
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.CMX_MigrarBaseDeDados();

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();

app.UseCors(m_esperanto_cors);

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();

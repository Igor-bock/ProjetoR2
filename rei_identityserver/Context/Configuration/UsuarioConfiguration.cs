using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using rei_esperantolib.Models;

namespace rei_identityserver.Context.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    private Usuario m_usuario
        => new Usuario
        {
            Nome = "Administrador",
            Id = Guid.NewGuid().ToString(),
            UserName = "adm",
            NormalizedUserName = "ADM",
            Email = "adm@reitech.com.br",
            NormalizedEmail = "EMAIL@REITECH.COM.BR",
            EmailConfirmed = false,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
        };

    public void Configure(EntityTypeBuilder<Usuario> p_builder)
    {
        p_builder.HasData(m_usuario);
    }
}

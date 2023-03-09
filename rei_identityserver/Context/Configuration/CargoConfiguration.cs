using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace rei_identityserver.Context.Configuration;

public class CargoConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> p_builder)
    {
        p_builder.HasData(
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Gerente",
                NormalizedName = "GERENTE"
            },
            new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            }
        );
    }
}

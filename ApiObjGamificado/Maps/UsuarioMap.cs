using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maps;

public class UsuarioMap : EntityBaseConfiguration<Usuario>
{
    public override void ConfigureEntity(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
        builder.Property(x => x.Nome);
        builder.Property(x => x.Email);
        // builder.HasOne(p => p.Engine).WithOne().HasForeignKey<Engine>(p => p.CarId);
    }
}
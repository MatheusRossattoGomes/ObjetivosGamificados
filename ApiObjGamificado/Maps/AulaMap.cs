using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maps;

public class AulaMap : EntityBaseConfiguration<Aula>
{
    public override void ConfigureEntity(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("Aula");
        builder.Property(x => x.Descricao);
        builder.Property(x => x.Resumo);
        builder.Property(x => x.DataAula);
        builder.Property(x => x.IdUsuario);

        builder.HasOne(x => x.Usuario)
        .WithMany(x => x.Aulas)
        .HasForeignKey(x => x.IdUsuario);
        // builder.HasOne(p => p.Engine).WithOne().HasForeignKey<Engine>(p => p.CarId);
    }
}
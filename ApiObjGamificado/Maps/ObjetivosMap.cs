using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maps;

public class ObjetivosMap : EntityBaseConfiguration<Objetivos>
{
    public override void ConfigureEntity(EntityTypeBuilder<Objetivos> builder)
    {
        builder.ToTable("Objetivos");
        builder.Property(x => x.Objetivo);
        builder.Property(x => x.Descricao);
        builder.Property(x => x.DataEntrega);
        builder.Property(x => x.DataCriacao);
        builder.Property(x => x.Quantidade);
        builder.Property(x => x.TipoObjetivo).HasColumnName("IdTipoObjetivo");
        builder.Property(x => x.IdUsuario);

        builder.HasOne(x => x.Usuario)
        .WithMany(x => x.Objetivos)
        .HasForeignKey(x => x.IdUsuario);
        // builder.HasOne(p => p.Engine).WithOne().HasForeignKey<Engine>(p => p.CarId);
    }
}
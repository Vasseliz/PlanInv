using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;
using PlanInv.Domain.ValueObjects;

namespace PlanInv.Infrastructure.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.CreatedAt)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired(false);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Idade)
            .IsRequired();

        builder.Property(u => u.Cpf)
            .HasConversion(
                cpf => cpf.Numero,
                numero => new Cpf(numero)
            )
            .HasColumnName("Cpf")
            .HasMaxLength(11)
            .IsRequired();


        builder.Property(u => u.UsuarioAtivo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(u => u.Cpf)
            .IsUnique();

        builder.Property(u => u.MetaDeAportesMensal)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasMany(u => u.Posicoes)
            .WithOne(p => p.Usuario)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(u => u.Posicoes)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude(false);
    }
}
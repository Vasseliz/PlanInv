using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;
using PlanInv.Domain.ValueObjects;

namespace PlanInv.Infrastructure.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.Id);

        // Configura propriedades simples
        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(u => u.Cpf)
            .HasConversion(
                cpfObj => cpfObj.Numero,

                cpfString => new Cpf(cpfString)
            )
            .HasColumnName("Cpf") 
            .HasMaxLength(11)  
            .IsRequired();

        builder.Property(u => u.MetaDeAportesMensal)
            .HasPrecision(18, 2);
    }
}

// ainda temos que  definir no db context
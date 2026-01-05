using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;
using PlanInv.Domain.ValueObjects;

namespace PlanInv.Infrastructure.Configurations;

public class AtivoConfiguration : IEntityTypeConfiguration<Ativo>
{
    public void Configure(EntityTypeBuilder<Ativo> builder)
    {
        // PK
        builder.HasKey(a => a.Id);

        // MODELO DO TICKER (BBAS3)
        builder.Property(a => a.Ticker)
            .IsRequired()
            .HasMaxLength(10); 

        // ÍNDICE ÚNICO: Garante que não existam dois ativos com o mesmo Ticker no banco.
        // O banco vai dar erro se tentar cadastrar "PETR4" duas vezes.
        builder.HasIndex(a => a.Ticker)
            .IsUnique();

        // (Tipo do Ativo)

        builder.Property(a => a.Tipo)
            .IsRequired()
            .HasConversion<string>();

        // --- Mapeamento do Value Object CNPJ ---
        builder.Property(a => a.Cnpj)
            .HasConversion(
                cnpjObj => cnpjObj.Numero,      
                cnpjString => new Cnpj(cnpjString) 
            )
            .HasColumnName("Cnpj")
            .HasMaxLength(14) 
            .IsRequired();

        // Dinheiro
        builder.Property(a => a.CotacaoAtual)
            .HasPrecision(18, 2) 
            .IsRequired();
    }
}
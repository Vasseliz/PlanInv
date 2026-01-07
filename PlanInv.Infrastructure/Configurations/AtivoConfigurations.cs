using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;
using PlanInv.Domain.ValueObjects;

namespace PlanInv.Infrastructure.Configurations;

public class AtivoConfigurations : IEntityTypeConfiguration<Ativo>
{
    public void Configure(EntityTypeBuilder<Ativo> builder)
    {
        builder.ToTable("Ativos");

        // Primary Key
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        // Ticker (BBAS3, PETR4, etc)
        builder.Property(a => a.Ticker)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired(false);

        // Índice único: impede duplicação de tickers
        builder.HasIndex(a => a.Ticker)
            .IsUnique();

        // Tipo do Ativo (Ação, FII, etc) - convertido para string no banco
        builder.Property(a => a.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);

        // --- Mapeamento do Value Object CNPJ ---
        // Importante: O Cnpj precisa ter um construtor sem parâmetros (protected)
        // para que o EF Core consiga reconstruí-lo
        builder.Property(a => a.Cnpj)
            .HasConversion(
                cnpjObj => cnpjObj.Numero,        // Salva apenas a string
                cnpjString => new Cnpj(cnpjString) // Reconstrói o objeto
            )
            .HasColumnName("Cnpj")
            .HasMaxLength(14)
            .IsRequired();

        // Cotação Atual
        builder.Property(a => a.CotacaoAtual)
            .HasPrecision(18, 2)
            .IsRequired();

        // Relacionamento: Um Ativo tem várias Posições
        builder.HasMany(a => a.Posicoes)
            .WithOne(p => p.Ativo)
            .HasForeignKey(p => p.AtivoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração da coleção de Posições
        // O EF Core usa o campo privado _posicoes para carregar os dados
        builder.Navigation(a => a.Posicoes)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
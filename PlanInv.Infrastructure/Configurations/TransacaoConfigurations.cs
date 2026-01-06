using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;
using PlanInv.Domain.Enums;

namespace PlanInv.Infrastructure.Configurations
{
    internal class TransacaoConfigurations : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.PosicaoId)
                .IsRequired();

            builder.Property(t => t.CorretoraId)
                .IsRequired();

            builder.Property(t => t.DataTransacao)
                .IsRequired();

            builder.Property(t => t.Tipo)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(t => t.QuantidadeCotas)
                .IsRequired();

            builder.Property(t => t.PrecoUnitario)
                .HasPrecision(18, 6)
                .IsRequired();

            builder.Property(t => t.Custos)
                .HasPrecision(18, 2)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(t => t.Observacoes)
                .HasMaxLength(500);

            builder.Ignore(t => t.ValorBruto);
            builder.Ignore(t => t.ValorLiquido);

            builder.HasOne(t => t.Posicao)
                .WithMany()
                .HasForeignKey(t => t.PosicaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Corretora)
                .WithMany()
                .HasForeignKey(t => t.CorretoraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.PosicaoId);
            builder.HasIndex(t => t.CorretoraId);
            builder.HasIndex(t => t.DataTransacao);
            builder.HasIndex(t => new { t.PosicaoId, t.DataTransacao });
        }
    }
}
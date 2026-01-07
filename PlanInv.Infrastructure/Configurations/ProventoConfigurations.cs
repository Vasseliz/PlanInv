using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;
using PlanInv.Domain.Enums;

namespace PlanInv.Infrastructure.Configurations
{
    internal class ProventoConfigurations : IEntityTypeConfiguration<Provento>
    {
        public void Configure(EntityTypeBuilder<Provento> builder)
        {
            builder.ToTable("Proventos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.PosicaoId)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder.Property(p => p.Tipo)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.DataPagamento)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
                .IsRequired();

            builder.Property(p => p.DataCom)
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
                .IsRequired();

            builder.Property(p => p.QuantidadeCotas)
                .IsRequired();

            builder.Property(p => p.ValorPorCota)
                .HasPrecision(18, 6)
                .IsRequired();

            builder.Property(p => p.ValorBruto)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.Imposto)
                .HasPrecision(18, 2)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(p => p.Observacoes)
                .HasMaxLength(500);

            builder.Ignore(p => p.ValorLiquido);

            builder.HasOne(p => p.Posicao)
                .WithMany(pos => pos.Proventos)
                .HasForeignKey(p => p.PosicaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.PosicaoId);
            builder.HasIndex(p => p.DataPagamento);
            builder.HasIndex(p => new { p.PosicaoId, p.DataCom });
        }
    }
}
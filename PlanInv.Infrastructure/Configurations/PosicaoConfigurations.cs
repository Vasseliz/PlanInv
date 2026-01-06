using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanInv.Infrastructure.Configurations
{
    public class PosicaoConfigurations : IEntityTypeConfiguration<Posicao>
    {
        public void Configure(EntityTypeBuilder<Posicao> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Quantidade)
                .IsRequired();

            builder.HasOne(p => p.Usuario)
                .WithMany(u => u.Posicoes)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict); //validar se vai deixar indo e voltando na configuration

            builder.HasOne(p => p.Ativo)
                .WithMany(a => a.Posicoes)
                .HasForeignKey(p => p.AtivoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Transacoes)
                .WithOne(t => t.Posicao)
                .HasForeignKey(t => t.PosicaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Proventos)
                .WithOne(pr => pr.Posicao)
                .HasForeignKey(pr => pr.PosicaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Quantidade)
                .IsRequired();

            builder.Property(p => p.PrecoMedio)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.ValorInvestido)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(p => p.DataPrimeiraCompra)
                .HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.Property(p => p.DataUltimaTransacao)
                .HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));


        }
    }
}

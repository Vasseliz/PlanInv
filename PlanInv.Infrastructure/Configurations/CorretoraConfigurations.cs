using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanInv.Domain.Entities;


namespace PlanInv.Infrastructure.Configurations
{
    public class CorretoraConfigurations : IEntityTypeConfiguration<Corretora>
    {
        public void Configure(EntityTypeBuilder<Corretora> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(c => c.Nome)
                .IsUnique();
            builder.Property(c => c.Ativa)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(c => c.Transacoes)
                .WithOne(t => t.Corretora)
                .HasForeignKey(t => t.CorretoraId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

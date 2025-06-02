using FIAP.FCG.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.FCG.Infrastructure.Configurations
{
    internal class CompraConfigurations : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.ToTable("Compra");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(c => c.DataCriacao).HasColumnType("DATETIME").IsRequired();
            builder.Property(c => c.Valor).HasColumnType("DECIMAL(4,2)").IsRequired();
            builder.Property(c => c.Desconto).HasColumnType("INT").IsRequired();
            builder.Property(c => c.Total).HasColumnType("DECIMAL(4,2)").IsRequired();
            builder.HasOne(c => c.Comprador).WithMany(u => u.Compras).HasForeignKey(c => c.CompradorId).IsRequired();
            builder.HasOne(c => c.Jogo).WithMany().HasForeignKey(c => c.JogoId).IsRequired();
            builder.HasIndex(c => new { c.CompradorId, c.JogoId }).IsUnique();
        }
    }
}

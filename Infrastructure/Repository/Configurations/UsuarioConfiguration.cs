using Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("INT").UseIdentityColumn();
        builder.Property(x => x.DataCriacao).HasColumnType("DATETIME").IsRequired();
        builder.Property(x => x.Nome).HasColumnType("VARCHAR(100)").IsRequired();
    }
}
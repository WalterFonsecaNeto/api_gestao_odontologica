using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios").HasKey(nameof(Usuario.Id));

            builder.Property(nameof(Usuario.Id))
                .HasColumnName("UsuarioId")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(Especialidade.Nome))
                .HasColumnName("Nome")
                .IsRequired(true)
                .HasMaxLength(100); 

            builder.Property(nameof(Usuario.Email))
                .HasColumnName("Email")
                .IsRequired(true)
                .HasMaxLength(100); 

            builder.Property(nameof(Usuario.Senha))
                .HasColumnName("Senha")
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(nameof(Especialidade.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true); 
        }
    }
}

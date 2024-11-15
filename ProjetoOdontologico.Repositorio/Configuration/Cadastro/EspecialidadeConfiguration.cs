using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class EspecialidadeConfiguration : IEntityTypeConfiguration<Especialidade>
    {
        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {
            builder.ToTable("Especialidades").HasKey(nameof(Especialidade.Id));

            builder.Property(nameof(Especialidade.Id))
                .HasColumnName("EspecialidadeId")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(Especialidade.Nome))
                .HasColumnName("Nome")
                .IsRequired(true)
                .HasMaxLength(100); // Nome da especialidade com até 100 caracteres

            builder.Property(nameof(Especialidade.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true);

            builder.Property(nameof(Especialidade.UsuarioId))
                .HasColumnName("UsuarioId")
                .IsRequired(true);

            builder
                .HasOne(u => u.Usuario)
                .WithMany(e => e.Especialidades)
                .HasForeignKey(nameof(Especialidade.UsuarioId))
                .OnDelete(DeleteBehavior.NoAction);
        }


    }
}


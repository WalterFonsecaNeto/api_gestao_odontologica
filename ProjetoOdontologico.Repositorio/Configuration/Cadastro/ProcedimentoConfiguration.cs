using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class ProcedimentoConfiguration : IEntityTypeConfiguration<Procedimento>
    {
        public void Configure(EntityTypeBuilder<Procedimento> builder)
        {
            builder.ToTable("Procedimentos").HasKey(nameof(Procedimento.Id));

            builder.Property(nameof(Procedimento.Id))
                .HasColumnName("ProcedimentoID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(Procedimento.Nome))
                .HasColumnName("Nome")
                .IsRequired(true)
                .HasMaxLength(100); // Nome do procedimento com até 100 caracteres

            builder.Property(nameof(Procedimento.Descricao))
                .HasColumnName("Descricao")
                .IsRequired(false)
                .HasColumnType("TEXT"); // Descrição do procedimento, permitindo texto longo

            builder.Property(nameof(Procedimento.Valor))
                .HasColumnName("Valor")
                .IsRequired(true)
                .HasColumnType("DECIMAL(18,2)"); // Valor do procedimento com precisão

            builder.Property(nameof(Procedimento.EspecialidadeId))
                .HasColumnName("EspecialidadeID")
                .IsRequired(true);
            builder
                .HasOne(p => p.Especialidade) // A referência à especialidade deve ser feita corretamente
                .WithMany(e => e.Procedimentos)
                .HasForeignKey(nameof(Procedimento.EspecialidadeId))
                .OnDelete(DeleteBehavior.NoAction); // Comportamento de exclusão


            builder.Property(nameof(Procedimento.UsuarioId))
                .HasColumnName("UsuarioId")
                .IsRequired(true);

            builder
                .HasOne(u => u.Usuario) 
                .WithMany(p => p.Procedimentos)
                .HasForeignKey(nameof(Procedimento.UsuarioId))
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}

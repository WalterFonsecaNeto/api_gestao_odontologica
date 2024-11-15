using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class OrcamentoConfiguration : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder.ToTable("Orcamentos").HasKey(nameof(Orcamento.Id));

            builder.Property(nameof(Orcamento.Id))
                .HasColumnName("OrcamentoID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(Orcamento.UsuarioId))
                .HasColumnName("UsuarioID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(Orcamento.PacienteId))
                .HasColumnName("PacienteID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(Orcamento.DataCriacao))
                .HasColumnName("DataCriacao")
                .IsRequired(true); // Data de criação do orçamento

            builder.Property(nameof(Orcamento.Total))
                .HasColumnName("Total")
                .IsRequired(true)
                .HasColumnType("decimal(18,2)"); // Total do orçamento com 2 casas decimais

            builder.Property(nameof(Orcamento.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true); // Indica se o orçamento está ativo ou não

            // Relacionamentos
            builder.HasOne(u => u.Usuario)
                .WithMany(o => o.Orcamentos) 
                .HasForeignKey(u => u.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction); // Comportamento de exclusão

            builder.HasOne(p => p.Paciente)
                .WithMany(o => o.Orcamentos) 
                .HasForeignKey(p => p.PacienteId)
                .OnDelete(DeleteBehavior.NoAction); // Comportamento de exclusão
        }
    }
}

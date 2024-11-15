using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class OrcamentoProcedimentoConfiguration : IEntityTypeConfiguration<OrcamentoProcedimento>
    {
        public void Configure(EntityTypeBuilder<OrcamentoProcedimento> builder)
        {
            builder.ToTable("OrcamentoProcedimentos").HasKey(nameof(OrcamentoProcedimento.Id));

            builder.Property(nameof(OrcamentoProcedimento.Id))
                .HasColumnName("OrcamentoProcedimentoID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(OrcamentoProcedimento.OrcamentoId))
                .HasColumnName("OrcamentoID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(OrcamentoProcedimento.ProcedimentoId))
                .HasColumnName("ProcedimentoID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(OrcamentoProcedimento.Descricao))
                .HasColumnName("Descricao")
                .IsRequired(false) // Descrição é opcional
                .HasMaxLength(255); // Descrição do procedimento com até 255 caracteres

            builder.Property(nameof(OrcamentoProcedimento.Valor))
                .HasColumnName("Valor")
                .IsRequired(true)
                .HasColumnType("decimal(18,2)"); // Valor do procedimento com 2 casas decimais

            builder.Property(nameof(OrcamentoProcedimento.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true); // Indica se o procedimento está ativo ou não

            // Relacionamentos
            builder.HasOne(o => o.Orcamento)
                .WithMany(p => p.OrcamentosProcedimentos)
                .HasForeignKey(o => o.OrcamentoId)
                .OnDelete(DeleteBehavior.NoAction); // Comportamento de exclusão

            builder.HasOne(p => p.Procedimento)
                .WithMany(o => o.OrcamentosProcedimentos)
                .HasForeignKey(p => p.ProcedimentoId)
                .OnDelete(DeleteBehavior.NoAction); // Comportamento de exclusão
        }
    }
}


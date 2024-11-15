using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class MovimentacaoFinanceiraConfiguration : IEntityTypeConfiguration<MovimentacaoFinanceira>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoFinanceira> builder)
        {
            builder.ToTable("MovimentacoesFinanceiras").HasKey(nameof(MovimentacaoFinanceira.Id));

            builder.Property(nameof(MovimentacaoFinanceira.Id))
                .HasColumnName("MovimentacaoFinanceiraID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(MovimentacaoFinanceira.UsuarioId))
                .HasColumnName("UsuarioID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(MovimentacaoFinanceira.TipoMovimento))
                .HasColumnName("TipoMovimento")
                .IsRequired(true)
                .HasMaxLength(10); // Tipo de movimento com até 10 caracteres (Entrada ou Saída)

            builder.Property(nameof(MovimentacaoFinanceira.Valor))
                .HasColumnName("Valor")
                .IsRequired(true)
                .HasColumnType("decimal(18,2)"); // Valor com 2 casas decimais

            builder.Property(nameof(MovimentacaoFinanceira.DataMovimentacao))
                .HasColumnName("DataMovimentacao")
                .IsRequired(true); // Data da movimentação

            builder.Property(nameof(MovimentacaoFinanceira.Descricao))
                .HasColumnName("Descricao")
                .IsRequired(false) // Descrição é opcional
                .HasMaxLength(255); // Descrição com até 255 caracteres

            builder.Property(nameof(MovimentacaoFinanceira.FormaPagamentoId))
                .HasColumnName("FormaPagamentoID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(MovimentacaoFinanceira.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true); // Indica se a movimentação está ativa ou não

            // Relacionamentos
            builder.HasOne(u => u.Usuario)
                .WithMany(mf => mf.MovimentacoesFinanceiras) 
                .HasForeignKey(u => u.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(fp => fp.FormaPagamento)
                .WithMany(mf => mf.MovimentacoesFinanceiras) 
                .HasForeignKey(fp => fp.FormaPagamentoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

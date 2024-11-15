using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class FormaPagamentoConfiguration : IEntityTypeConfiguration<FormaPagamento>
    {
        public void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            builder.ToTable("FormasPagamento").HasKey(nameof(FormaPagamento.Id));

            builder.Property(nameof(FormaPagamento.Id))
                .HasColumnName("FormaPagamentoID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(FormaPagamento.Nome))
                .HasColumnName("NomeForma")
                .IsRequired(true)
                .HasMaxLength(50); // Nome da forma de pagamento com até 50 caracteres

            builder.Property(nameof(FormaPagamento.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true);

            builder.Property(nameof(FormaPagamento.UsuarioId))
                .HasColumnName("UsuarioId")
                .IsRequired(true);

            builder
                .HasOne(u => u.Usuario)
                .WithMany(fp => fp.FormasPagamento)
                .HasForeignKey(nameof(FormaPagamento.UsuarioId))
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

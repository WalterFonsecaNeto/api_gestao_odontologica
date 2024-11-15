using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class ContaReceberConfiguration : IEntityTypeConfiguration<ContaReceber>
    {
        public void Configure(EntityTypeBuilder<ContaReceber> builder)
        {
            builder.ToTable("ContasReceber").HasKey(nameof(ContaReceber.Id));

            builder.Property(nameof(ContaReceber.Id))
                .HasColumnName("ContaReceberID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(ContaReceber.UsuarioId))
                .HasColumnName("UsuarioID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(ContaReceber.PacienteId))
                .HasColumnName("PacienteID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(ContaReceber.DataVencimento))
                .HasColumnName("DataVencimento")
                .IsRequired(true); // Data de vencimento da conta

            builder.Property(nameof(ContaReceber.ValorReceber))
                .HasColumnName("ValorReceber")
                .IsRequired(true)
                .HasColumnType("decimal(18,2)"); // Valor a receber com 2 casas decimais

            builder.Property(nameof(ContaReceber.Status))
                .HasColumnName("Status")
                .IsRequired(true)
                .HasMaxLength(20); // Status da conta com até 20 caracteres

            builder.Property(nameof(ContaReceber.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true); // Indica se a conta está ativa ou não

            // Relacionamentos
            builder.HasOne(u => u.Usuario)
                .WithMany(cr => cr.ContasReceber) 
                .HasForeignKey(u => u.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(p => p.Paciente)
                .WithMany(cr => cr.ContasReceber) 
                .HasForeignKey(p => p.PacienteId)
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}

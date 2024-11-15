using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable("Agendamentos").HasKey(nameof(Agendamento.Id));

            builder.Property(nameof(Agendamento.Id))
                .HasColumnName("AgendamentoID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(Agendamento.UsuarioId))
                .HasColumnName("UsuarioID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(Agendamento.DataHora))
                .HasColumnName("DataHora")
                .IsRequired(true); // Data e hora do agendamento

            builder.Property(nameof(Agendamento.Status))
                .HasColumnName("Status")
                .IsRequired(true)
                .HasMaxLength(50); // Status do agendamento com até 50 caracteres

            builder.Property(nameof(Agendamento.PacienteId))
                .HasColumnName("PacienteID")
                .IsRequired(true); // Chave estrangeira

            builder.Property(nameof(Agendamento.Descricao))
                .HasColumnName("Descricao")
                .IsRequired(false) // Descrição é opcional
                .HasColumnType("TEXT"); // Texto longo para histórico médico

            builder.Property(nameof(Agendamento.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true); // Indica se o agendamento está ativo ou não

            // Relacionamentos
            builder.HasOne(u => u.Usuario)
                .WithMany(a => a.Agendamentos) 
                .HasForeignKey(u => u.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction); // Comportamento de exclusão

            builder.HasOne(p => p.Paciente)
                .WithMany(a => a.Agendamentos) 
                .HasForeignKey(p => p.PacienteId)
                .OnDelete(DeleteBehavior.NoAction); // Comportamento de exclusão
        }
    }
}

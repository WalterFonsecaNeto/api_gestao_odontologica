using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes").HasKey(nameof(Paciente.Id));
            builder.Property(nameof(Paciente.Id))
                .HasColumnName("PacienteID")
                .IsRequired(true); // Chave primária

            builder.Property(nameof(Paciente.Nome))
                .HasColumnName("Nome")
                .IsRequired(true)
                .HasMaxLength(100); // Nome máximo de 100 caracteres

            builder.Property(nameof(Paciente.CPF))
                .HasColumnName("CPF")
                .IsRequired(true)
                .HasMaxLength(14); // CPF com formatação (xxx.xxx.xxx-xx)

            builder.Property(nameof(Paciente.Endereco))
                .HasColumnName("Endereco")
                .IsRequired(true)
                .HasMaxLength(255); // Endereço máximo de 255 caracteres

            builder.Property(nameof(Paciente.Telefone))
                .HasColumnName("Telefone")
                .IsRequired(false)
                .HasMaxLength(20); // Telefone com até 20 caracteres

            builder.Property(nameof(Paciente.DataNascimento))
                .HasColumnName("DataNascimento") // Corrigido o nome da coluna (de "DataNacimento" para "DataNascimento")
                .IsRequired(true)
                .HasColumnType("DATE"); // Data de nascimento

            builder.Property(nameof(Paciente.Genero))
                .HasColumnName("Genero")
                .IsRequired(true)
                .HasMaxLength(20); // Gênero com até 20 caracteres

            builder.Property(nameof(Paciente.Email))
                .HasColumnName("Email")
                .IsRequired(false)
                .HasMaxLength(255); // Email com até 255 caracteres

            builder.Property(nameof(Paciente.HistoricoMedico))
                .HasColumnName("HistoricoMedico")
                .IsRequired(false)
                .HasColumnType("TEXT"); // Texto longo para histórico médico

            builder.Property(nameof(Paciente.Ativo))
                .HasColumnName("Ativo")
                .IsRequired(true); // Campo obrigatório para indicar se o paciente está ativo ou não


            builder.Property(nameof(Paciente.UsuarioId))
               .HasColumnName("UsuarioId")
               .IsRequired(true);

            builder
                .HasOne(u => u.Usuario)
                .WithMany(p => p.Pacientes)
                .HasForeignKey(nameof(Paciente.UsuarioId))
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Aplicacao
{
    public interface IPacienteAplicacao
    {
        Task<int> CriarPacienteAsync(Paciente paciente);
        Task AtualizarPacienteAsync(Paciente paciente, int usuarioId);
        Task<Paciente> ObterPacientePorIdAsync(int pacienteId, int usuarioId, bool ativo);
        Task DeletarPacienteAsync(int pacienteId, int usuarioId);
        Task RestaurarPacienteAsync(int pacienteId, int usuarioId);
        Task<IEnumerable<Paciente>> ListarPacientesPorUsuarioAsync(int usuarioId, bool ativo);
    }

}
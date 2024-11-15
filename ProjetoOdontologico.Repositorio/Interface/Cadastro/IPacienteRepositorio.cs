using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IPacienteRepositorio
    {
        Task<int> SalvarAsync(Paciente paciente);
        Task AtualizarAsync(Paciente paciente);
        Task<Paciente> ObterPorIdAsync(int pacienteID, int usuarioId, bool ativo);
        Task DeletarAsync(Paciente paciente);
        Task RestaurarAsync(Paciente paciente);
        Task<IEnumerable<Paciente>> ListarPorUsuarioAsync(int usuarioId, bool ativo);
    }
}

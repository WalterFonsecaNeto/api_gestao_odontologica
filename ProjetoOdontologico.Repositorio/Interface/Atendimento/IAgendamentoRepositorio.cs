using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IAgendamentoRepositorio
    {
        Task<int> SalvarAsync(Agendamento agendamento);
        Task AtualizarAsync(Agendamento agendamento);
        Task<Agendamento> ObterPorIdAsync(int agendamentoId, int usuarioId, bool ativo );
        Task DeletarAsync(Agendamento agendamento);
        Task RestaurarAsync(Agendamento agendamento);
        Task<IEnumerable<Agendamento>> ListarAsync(int usuarioId, bool ativo);
        Task<IEnumerable<Agendamento>> ListarPorPacienteAsync(int usuarioId, int pacienteId, bool ativo);
    }
}

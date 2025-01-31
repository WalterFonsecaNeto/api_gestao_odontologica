using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Aplicacao
{
   public interface IAgendamentoAplicacao
    {
        Task<int> CriarAgendamentoAsync(Agendamento agendamento);
        Task AtualizarAgendamentoAsync(Agendamento agendamento, int agendamentoId, int usuarioId);
        Task<Agendamento> ObterAgendamentoPorIdAsync(int agendamentoId, int usuarioId, bool ativo);
        Task DeletarAgendamentoAsync(int agendamentoId, int usuarioId);
        Task RestaurarAgendamentoAsync(int agendamentoId, int usuarioId);
        Task<IEnumerable<Agendamento>> ListarAgendamentoPorUsuarioIdAsync(int usuarioId, bool ativo);
        Task<IEnumerable<Agendamento>> ListarAgendamentoPorPacienteIdAsync(int usuarioId, int pacienteId, bool ativo);

    }
}


using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IOrcamentoRepositorio
    {
        Task<int> SalvarAsync(Orcamento orcamento);
        Task AtualizarAsync(Orcamento orcamento);
        Task<Orcamento> ObterPorIdAsync(int orcamentoID, int usuarioId, bool ativo );
        Task DeletarAsync(Orcamento orcamento);
        Task RestaurarAsync(Orcamento orcamento);
        Task<IEnumerable<Orcamento>> ListarAsync(int usuarioId, bool ativo);
        Task<IEnumerable<Orcamento>> ListarPorPacienteAsync(int usuarioId, int pacienteId, bool ativo);
    }
}

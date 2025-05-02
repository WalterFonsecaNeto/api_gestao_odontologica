using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IProcedimentoRepositorio
    {
        Task<int> SalvarAsync(Procedimento procedimento);
        Task AtualizarAsync(Procedimento procedimento);
        Task<Procedimento> ObterPorIdAsync(int procedimentoID, int usuarioId, bool ativo);
        Task DeletarAsync(Procedimento procedimento);
        Task RestaurarAsync(Procedimento procedimento);
        Task<IEnumerable<Procedimento>> ListarAsync(int usuarioId, bool ativo);
        Task<IEnumerable<Procedimento>> ListarPorEspecialidadeIdAsync(int especialidadeId);
    }
}
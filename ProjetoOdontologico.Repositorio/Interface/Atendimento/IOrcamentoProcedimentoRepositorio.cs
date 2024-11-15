using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IOrcamentoProcedimentoRepositorio
    {
        Task<int> SalvarAsync(OrcamentoProcedimento orcamentoProcedimento);
        Task AtualizarAsync(OrcamentoProcedimento orcamentoProcedimento);
        Task<OrcamentoProcedimento> ObterPorIdAsync(int orcamentoProcedimentoID, bool ativo);
        Task DeletarAsync(OrcamentoProcedimento orcamentoProcedimento);
        Task RestaurarAsync(OrcamentoProcedimento orcamentoProcedimento);
        Task<IEnumerable<OrcamentoProcedimento>> ListarAsync(bool ativo);
    }
}

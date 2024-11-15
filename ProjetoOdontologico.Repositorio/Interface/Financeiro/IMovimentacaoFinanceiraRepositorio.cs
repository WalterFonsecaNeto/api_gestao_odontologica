using ProjetoOdontologico.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoOdontologico.Repositorio
{
    public interface IMovimentacaoFinanceiraRepositorio
    {
        Task<int> SalvarAsync(MovimentacaoFinanceira movimentacaoFinanceira);
        Task AtualizarAsync(MovimentacaoFinanceira movimentacaoFinanceira);
        Task<MovimentacaoFinanceira> ObterPorIdAsync(int movimentacaoFinanceiraID, int usuarioId, bool ativo);
        Task DeletarAsync(MovimentacaoFinanceira movimentacaoFinanceira);
        Task RestaurarAsync(MovimentacaoFinanceira movimentacaoFinanceira);
        Task<IEnumerable<MovimentacaoFinanceira>> ListarAsync(int usuarioId, bool ativo);
    }
}
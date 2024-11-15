using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IFormaPagamentoRepositorio
    {
        Task<int> SalvarAsync(FormaPagamento formaPagamento);
        Task AtualizarAsync(FormaPagamento formaPagamento);
        Task<FormaPagamento> ObterPorIdAsync(int formaPagamentoID, int usuarioId, bool ativo);
        Task DeletarAsync(FormaPagamento formaPagamento);
        Task RestaurarAsync(FormaPagamento formaPagamento);
        Task<IEnumerable<FormaPagamento>> ListarAsync(int usuarioId, bool ativo);
    }
}
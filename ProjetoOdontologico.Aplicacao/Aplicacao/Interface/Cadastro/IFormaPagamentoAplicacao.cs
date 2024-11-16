using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Aplicacao
{
    public interface IFormaPagamentoAplicacao
    {
        public Task<int> CriarFormaPagamentoAsync(FormaPagamento formaPagamento);
        public Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento, int usuarioId);

        public Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int formaPagamentoId, int usuarioId, bool ativo);

        public Task DeletarFormaPagamentoAsync(int formaPagamentoId, int usuarioId);
        public Task RestaurarFormaPagamentoAsync(int formaPagamentoId, int usuarioId);
        public Task<IEnumerable<FormaPagamento>> ListarFormasPagamentoPorUsuarioAsync(int usuarioId, bool ativo);
    }
}


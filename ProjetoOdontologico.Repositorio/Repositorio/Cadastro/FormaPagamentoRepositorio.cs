using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class FormaPagamentoRepositorio : BaseRepositorio, IFormaPagamentoRepositorio
    {
        public FormaPagamentoRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(FormaPagamento formaPagamento)
        {
            await _contexto.FormasPagamento.AddAsync(formaPagamento);
            await _contexto.SaveChangesAsync();

            return formaPagamento.Id;
        }

        public async Task AtualizarAsync(FormaPagamento formaPagamento)
        {
            _contexto.FormasPagamento.Update(formaPagamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<FormaPagamento> ObterPorIdAsync(int formaPagamentoID, int usuarioId, bool ativo)
        {
            return await _contexto.FormasPagamento
                .Where(fp => fp.Ativo == ativo && fp.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(fp => fp.Id == formaPagamentoID);
        }

        public async Task DeletarAsync(FormaPagamento formaPagamento)
        {
            formaPagamento.Deletar();
            _contexto.FormasPagamento.Update(formaPagamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(FormaPagamento formaPagamento)
        {
            formaPagamento.Restaurar();
            _contexto.FormasPagamento.Update(formaPagamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<FormaPagamento>> ListarAsync(int usuarioId, bool ativo)
        {
            return await _contexto.FormasPagamento
                .Where(fp => fp.Ativo == ativo && fp.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class MovimentacaoFinanceiraRepositorio : BaseRepositorio, IMovimentacaoFinanceiraRepositorio
    {
        public MovimentacaoFinanceiraRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(MovimentacaoFinanceira movimentacaoFinanceira)
        {
            await _contexto.MovimentacoesFinanceiras.AddAsync(movimentacaoFinanceira);
            await _contexto.SaveChangesAsync();

            return movimentacaoFinanceira.Id;
        }

        public async Task AtualizarAsync(MovimentacaoFinanceira movimentacaoFinanceira)
        {
            _contexto.MovimentacoesFinanceiras.Update(movimentacaoFinanceira);
            await _contexto.SaveChangesAsync();
        }

        public async Task<MovimentacaoFinanceira> ObterPorIdAsync(int movimentacaoFinanceiraID, int usuarioId, bool ativo)
        {
            return await _contexto.MovimentacoesFinanceiras
                .Where(m => m.Ativo == ativo && m.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(m => m.Id == movimentacaoFinanceiraID);
        }

        public async Task DeletarAsync(MovimentacaoFinanceira movimentacaoFinanceira)
        {
            movimentacaoFinanceira.Deletar();
            _contexto.MovimentacoesFinanceiras.Update(movimentacaoFinanceira);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(MovimentacaoFinanceira movimentacaoFinanceira)
        {
            movimentacaoFinanceira.Restaurar();
            _contexto.MovimentacoesFinanceiras.Update(movimentacaoFinanceira);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovimentacaoFinanceira>> ListarAsync(int usuarioId, bool ativo)
        {
            return await _contexto.MovimentacoesFinanceiras
                .Where(m => m.Ativo == ativo && m.UsuarioId == usuarioId)
                .ToListAsync();
        }
    }
}


using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class ProcedimentoRepositorio : BaseRepositorio, IProcedimentoRepositorio
    {
        public ProcedimentoRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync( Procedimento procedimento)
        {
            await _contexto.Procedimentos.AddAsync(procedimento);
            await _contexto.SaveChangesAsync();

            return procedimento.Id;
        }

        public async Task AtualizarAsync(Procedimento procedimento)
        {
            _contexto.Procedimentos.Update(procedimento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Procedimento> ObterPorIdAsync(int procedimentoID, int usuarioId ,bool ativo)
        {
            return await _contexto.Procedimentos
                .Where(p => p.Ativo == ativo && p.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(p => p.Id == procedimentoID);
        }

        public async Task DeletarAsync(Procedimento procedimento)
        {
            procedimento.Deletar();
            _contexto.Procedimentos.Update(procedimento);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(Procedimento procedimento)
        {
            procedimento.Restaurar();
            _contexto.Procedimentos.Update(procedimento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Procedimento>> ListarAsync(int usuarioId, bool ativo)
        {
            return await _contexto.Procedimentos
                .Where(p => p.Ativo == ativo && p.UsuarioId == usuarioId) 
                .ToListAsync();
        }
    }
}

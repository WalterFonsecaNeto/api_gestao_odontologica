using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class OrcamentoRepositorio : BaseRepositorio, IOrcamentoRepositorio
    {
        public OrcamentoRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(Orcamento orcamento)
        {
            await _contexto.Orcamentos.AddAsync(orcamento);
            await _contexto.SaveChangesAsync();

            return orcamento.Id;
        }

        public async Task AtualizarAsync(Orcamento orcamento)
        {
            _contexto.Orcamentos.Update(orcamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Orcamento> ObterPorIdAsync(int orcamentoID, int usuarioId, bool ativo )
        {
            return await _contexto.Orcamentos
                .Where(o => o.Ativo == ativo && o.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(o => o.Id == orcamentoID);
        }

        public async Task DeletarAsync(Orcamento orcamento)
        {
            orcamento.Deletar();
            _contexto.Orcamentos.Update(orcamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(Orcamento orcamento)
        {
            orcamento.Restaurar();
            _contexto.Orcamentos.Update(orcamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Orcamento>> ListarAsync(int usuarioId, bool ativo)
        {
            return await _contexto.Orcamentos
                .Where(o => o.Ativo == ativo && o.UsuarioId == usuarioId)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Orcamento>> ListarPorPacienteAsync(int usuarioId, int pacienteId, bool ativo)
        {
            return await _contexto.Orcamentos
                .Where(o => o.Ativo == ativo && o.PacienteId == pacienteId && o.UsuarioId == usuarioId)  
                .ToListAsync();
        }
    }
}

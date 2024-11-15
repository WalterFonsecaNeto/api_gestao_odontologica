using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class ContaReceberRepositorio : BaseRepositorio, IContaReceberRepositorio
    {
        public ContaReceberRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(ContaReceber contaReceber)
        {
            await _contexto.ContasReceber.AddAsync(contaReceber);
            await _contexto.SaveChangesAsync();

            return contaReceber.Id;
        }

        public async Task AtualizarAsync(ContaReceber contaReceber)
        {
            _contexto.ContasReceber.Update(contaReceber);
            await _contexto.SaveChangesAsync();
        }

        public async Task<ContaReceber> ObterPorIdAsync(int contaReceberID, int UsuarioId, bool ativo)
        {
            return await _contexto.ContasReceber
                .Where(c => c.Ativo == ativo && c.UsuarioId == UsuarioId)
                .FirstOrDefaultAsync(c => c.Id == contaReceberID);
        }

        public async Task DeletarAsync(ContaReceber contaReceber)
        {
            contaReceber.Deletar();
            _contexto.ContasReceber.Update(contaReceber);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(ContaReceber contaReceber)
        {
            contaReceber.Restaurar();
            _contexto.ContasReceber.Update(contaReceber);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<ContaReceber>> ListarAsync(int UsuarioId, bool ativo)
        {
            return await _contexto.ContasReceber
                .Where(c => c.Ativo == ativo && c.UsuarioId == UsuarioId)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<ContaReceber>> ListarPorPacienteAsync(int UsuarioId, int PacienteId, bool ativo)
        {
            return await _contexto.ContasReceber
                .Where(c => c.Ativo == ativo && c.UsuarioId == UsuarioId && c.PacienteId == PacienteId)
                .ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class PacienteRepositorio : BaseRepositorio , IPacienteRepositorio
    {
        public PacienteRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(Paciente paciente)
        {
            await _contexto.Pacientes.AddAsync(paciente);
            await _contexto.SaveChangesAsync();

            return paciente.Id;
        }

        public async Task AtualizarAsync(Paciente paciente)
        {
            _contexto.Pacientes.Update(paciente);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Paciente> ObterPorIdAsync(int pacienteId, int usuarioId, bool ativo)
        {
            return await _contexto.Pacientes
                .Where(p => p.Ativo == ativo && p.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(p => p.Id == pacienteId);
        }

        public async Task DeletarAsync(Paciente paciente)
        {
            paciente.Deletar();
            _contexto.Pacientes.Update(paciente);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(Paciente paciente)
        {
            paciente.Restaurar();
            _contexto.Pacientes.Update(paciente);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Paciente>> ListarPorUsuarioAsync(int usuarioId, bool ativo)
        {
            return await _contexto.Pacientes
                .Where(p => p.Ativo == ativo && p.UsuarioId == usuarioId)
                .ToListAsync();
        }
        
    }
}
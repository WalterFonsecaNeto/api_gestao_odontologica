using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class AgendamentoRepositorio : BaseRepositorio, IAgendamentoRepositorio
    {
        public AgendamentoRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(Agendamento agendamento)
        {
            await _contexto.Agendamentos.AddAsync(agendamento);
            await _contexto.SaveChangesAsync();

            return agendamento.Id;
        }

        public async Task AtualizarAsync(Agendamento agendamento)
        {
            _contexto.Agendamentos.Update(agendamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Agendamento> ObterPorIdAsync(int agendamentoId, int usuarioId, bool ativo )
        {
            return await _contexto.Agendamentos
                .Where(a => a.Ativo == ativo && a.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(a => a.Id == agendamentoId);
        }

        public async Task DeletarAsync(Agendamento agendamento)
        {
            agendamento.Deletar();
            _contexto.Agendamentos.Update(agendamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(Agendamento agendamento)
        {
            agendamento.Restaurar();
            _contexto.Agendamentos.Update(agendamento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Agendamento>> ListarAsync(int usuarioId, bool ativo)
        {
            return await _contexto.Agendamentos
                .Where(a => a.Ativo == ativo && a.UsuarioId == usuarioId)
                .ToListAsync();
        }

        //? Lista de agendamentos de um determinado paciente
        public async Task<IEnumerable<Agendamento>> ListarPorPacienteAsync(int usuarioId, int pacienteId, bool ativo)
        {
            return await _contexto.Agendamentos
                .Where(a => a.Ativo == ativo && a.UsuarioId == usuarioId && a.PacienteId == pacienteId) 
                .ToListAsync();
        }
    }
}

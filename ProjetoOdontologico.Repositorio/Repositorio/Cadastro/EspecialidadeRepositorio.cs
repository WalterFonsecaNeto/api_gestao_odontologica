using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class EspecialidadeRepositorio : BaseRepositorio, IEspecialidadeRepositorio
    {
        public EspecialidadeRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(Especialidade especialidade)
        {
            await _contexto.Especialidades.AddAsync(especialidade);
            await _contexto.SaveChangesAsync();

            return especialidade.Id;
        }

        public async Task AtualizarAsync(Especialidade especialidade)
        {
            _contexto.Especialidades.Update(especialidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Especialidade> ObterPorIdAsync(int especialidadeId ,int usuarioId, bool ativo)
        {
            return await _contexto.Especialidades
                .Where(e => e.Ativo == ativo && e.UsuarioId == usuarioId)
                .FirstOrDefaultAsync(e => e.Id == especialidadeId);
        }

        public async Task DeletarAsync(Especialidade especialidade)
        {
            especialidade.Deletar();
            _contexto.Especialidades.Update(especialidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(Especialidade especialidade)
        {
            especialidade.Restaurar();
            _contexto.Especialidades.Update(especialidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Especialidade>> ListarAsync(int usuarioId, bool ativo)
        {
            return await _contexto.Especialidades
                .Where(e => e.Ativo == ativo && e.UsuarioId == usuarioId)
                .ToListAsync();
        }

    }
}
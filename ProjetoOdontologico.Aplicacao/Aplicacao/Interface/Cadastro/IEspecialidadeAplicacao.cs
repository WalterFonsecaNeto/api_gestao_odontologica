using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Aplicacao
{
    public interface IEspecialidadeAplicacao
    {
        Task<int> CriarEspecialidadeAsync(Especialidade especialidade);
        Task AtualizarEspecialidadeAsync(Especialidade especialidade, int usuarioId, int especialidadeId);
        Task<Especialidade> ObterEspecialidadePorIdAsync(int especialidadeId, int usuarioId, bool ativo);
        Task DeletarEspecialidadeAsync(int especialidadeId, int usuarioId);
        Task RestaurarEspecialidadeAsync(int especialidadeId, int usuarioId);
        Task<IEnumerable<Especialidade>> ListarEspecialidadesPorUsuarioAsync(int usuarioIdbool, bool ativo);
    }
}


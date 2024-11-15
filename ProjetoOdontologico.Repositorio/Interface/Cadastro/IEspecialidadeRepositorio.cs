using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IEspecialidadeRepositorio
    {
        Task<int> SalvarAsync(Especialidade especialidade);
        Task AtualizarAsync(Especialidade especialidade);
        Task<Especialidade> ObterPorIdAsync(int especialidadeId ,int usuarioId, bool ativo);
        Task DeletarAsync(Especialidade especialidade);
        Task RestaurarAsync(Especialidade especialidade);
        Task<IEnumerable<Especialidade>> ListarAsync(int usuarioId, bool ativo);
    }
}
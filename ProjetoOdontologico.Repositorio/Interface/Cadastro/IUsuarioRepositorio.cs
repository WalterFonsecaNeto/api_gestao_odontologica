using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<int> SalvarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task<Usuario> ObterPorIdAsync(int usuarioID, bool ativo);
        Task DeletarAsync(Usuario usuario);
        Task RestaurarAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> ListarAsync(bool ativo);
        Task<Usuario> ValidarUsuarioParaLogin(Usuario usuario, bool ativo);
        Task<Usuario> ValidarUsuarioParaCadastro(Usuario usuario, bool ativo);
    }
}

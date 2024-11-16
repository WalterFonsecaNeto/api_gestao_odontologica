using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Aplicacao
{

    public interface IUsuarioAplicacao
    {
        Task<int> CriarUsuarioAsync(Usuario usuario);
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task AlterarSenhaAsync(Usuario usuario, string senhaAntiga);
        Task<Usuario> ObterUsuarioPorIdAsync(int usuarioId, bool ativo);
        Task DeletarUsuarioAsync(int usuarioId);
        Task RestaurarUsuarioAsync(int usuarioId);
        Task<IEnumerable<Usuario>> ListarUsuariosAsync(bool ativo);
        Task<Usuario> ValidarUsuario(Usuario usuario);
    }
}

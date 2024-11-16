using ProjetoOdontologico.Dominio.Entidades;
using ProjetoOdontologico.Repositorio;

namespace ProjetoOdontologico.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {
        #region Atributos
        readonly IUsuarioRepositorio _usuarioRepositorio;
        #endregion

        #region Construtores
        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        #endregion

        #region Funções
        public async Task<int> CriarUsuarioAsync(Usuario usuario)
        {
            // Validações para que seja enviado o dado de forma correta para o banco de dados.
            if (usuario == null)
            {
                throw new Exception("Usuário não pode ser vazio");
            }
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new Exception("Senha não pode ser vazia");
            }
            ValidarInformacoesUsuario(usuario);

            // Quando tudo estiver correto, o usuário será salvo
            return await _usuarioRepositorio.SalvarAsync(usuario);
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(usuario.Id, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            ValidarInformacoesUsuario(usuario);

            usuarioEncontrado.Nome = usuario.Nome;
            usuarioEncontrado.Email = usuario.Email;

            await _usuarioRepositorio.AtualizarAsync(usuarioEncontrado);
        }

        public async Task AlterarSenhaAsync(Usuario usuario, string senhaAntiga)
        {
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(usuario.Id, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            if (usuarioEncontrado.Senha != senhaAntiga)
            {
                throw new Exception("Senha antiga incorreta");
            }
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new Exception("Senha não pode ser vazia");
            }
            usuarioEncontrado.Senha = usuario.Senha;

            await _usuarioRepositorio.AtualizarAsync(usuarioEncontrado);
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int usuarioId, bool ativo)
        {
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(usuarioId, ativo);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            return usuarioEncontrado;
        }

        public async Task DeletarUsuarioAsync(int usuarioId)
        {
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(usuarioId, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            await _usuarioRepositorio.DeletarAsync(usuarioEncontrado);
        }

        public async Task RestaurarUsuarioAsync(int usuarioId)
        {
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(usuarioId, false);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            await _usuarioRepositorio.RestaurarAsync(usuarioEncontrado);
        }

        public async Task<IEnumerable<Usuario>> ListarUsuariosAsync(bool ativo)
        {
            return await _usuarioRepositorio.ListarAsync(ativo);
        }

        public async Task<Usuario> ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("Usuário não pode ser vazio");
            }
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new Exception("Senha não pode ser vazia");
            }
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new Exception("Email não pode ser vazia");
            }
            return await _usuarioRepositorio.ValidarUsuario(usuario);




        }

        #endregion
       
        #region Util
        private static void ValidarInformacoesUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nome))
            {
                throw new Exception("Nome não pode ser vazio");
            }
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new Exception("Email não pode ser vazio");
            }
        }
        
        #endregion
    }
}

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
            
            var usuarioExiste = await _usuarioRepositorio.ValidarUsuario(usuario, true);
            

            if (usuarioExiste != null)
            {
                throw new Exception("Email já existe");
            }
            

            // Quando tudo estiver correto, o usuário será salvo
            return await _usuarioRepositorio.SalvarAsync(usuario);
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario, int usuarioId)
        {
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(usuarioId, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            ValidarInformacoesParaAtualizar(usuario, usuarioEncontrado);

            await _usuarioRepositorio.AtualizarAsync(usuarioEncontrado);
        }

        public async Task AlterarSenhaAsync(string senhaNova, string senhaAntiga, int usuarioId)
        {
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(usuarioId, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            if (usuarioEncontrado.Senha != senhaAntiga)
            {
                throw new Exception("Senha antiga incorreta");
            }
            if (string.IsNullOrEmpty(senhaNova))
            {
                throw new Exception("Senha nova não pode ser vazia");
            }
            usuarioEncontrado.Senha = senhaNova;

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
            var usuarioEncontrado = await _usuarioRepositorio.ValidarUsuario(usuario, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Email ou senha incorretos");
            }
            return usuarioEncontrado;





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
        private static void ValidarInformacoesParaAtualizar(Usuario usuario, Usuario usuarioEncontrado)
        {
            if (string.IsNullOrEmpty(usuario.Nome))
            {
                usuarioEncontrado.Nome = usuarioEncontrado.Nome;
            }
            else
            {
                usuarioEncontrado.Nome = usuario.Nome;
            }
            if (string.IsNullOrEmpty(usuario.Email))
            {
                usuarioEncontrado.Email = usuarioEncontrado.Email;
            }
            else
            {
                usuarioEncontrado.Email = usuario.Email;
            }
        }

        #endregion
    }
}

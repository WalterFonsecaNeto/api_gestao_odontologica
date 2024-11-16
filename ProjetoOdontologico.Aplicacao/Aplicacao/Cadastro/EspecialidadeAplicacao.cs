using ProjetoOdontologico.Dominio.Entidades;
using ProjetoOdontologico.Repositorio;

namespace ProjetoOdontologico.Aplicacao
{
    public class EspecialidadeAplicacao : IEspecialidadeAplicacao
    {

        #region Atributos
        readonly IEspecialidadeRepositorio _especialidadeRepositorio;
        readonly IUsuarioRepositorio _usuarioRepositorio;
        #endregion


        #region Contrutores 
        public EspecialidadeAplicacao(IEspecialidadeRepositorio especialidadeRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _especialidadeRepositorio = especialidadeRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }
        #endregion


        #region Funções
        public async Task<int> CriarEspecialidadeAsync(Especialidade especialidade)
        {
            ValidarInformacoesObrigatorias(especialidade);

            //! Fazer essa verificação para todos, pra ver se o usuarioId pertence a algum usuario mesmo
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(especialidade.UsuarioId, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            int especialidadeSalvaId = await _especialidadeRepositorio.SalvarAsync(especialidade);

            return especialidadeSalvaId;
        }

        public async Task AtualizarEspecialidadeAsync(Especialidade especialidade, int usuarioId, int especialidadeId)
        {
            var especialidadeEncontrada = await _especialidadeRepositorio.ObterPorIdAsync(especialidadeId, usuarioId, true);

            ValidarExistenciaDaEspecialidade(especialidadeEncontrada);

            especialidadeEncontrada = ValidarInformacoesPraAtualizacao(especialidade, especialidadeEncontrada);//? Valida as informações de forma que caso o usuario não queira alterar alguma area ele apenas deixa em branco.

            await _especialidadeRepositorio.AtualizarAsync(especialidadeEncontrada);
        }

        public async Task<Especialidade> ObterEspecialidadePorIdAsync(int especialidadeId, int usuarioId, bool ativo)
        {
            var especialidadeEncontrada = await _especialidadeRepositorio.ObterPorIdAsync(especialidadeId, usuarioId, ativo);

            ValidarExistenciaDaEspecialidade(especialidadeEncontrada);

            return especialidadeEncontrada;
        }

        public async Task DeletarEspecialidadeAsync(int especialidadeId, int usuarioId)
        {
            var especialidadeEncontrada = await _especialidadeRepositorio.ObterPorIdAsync(especialidadeId, usuarioId, true);

            ValidarExistenciaDaEspecialidade(especialidadeEncontrada);

            ValidarInformacoesParaExclusao(especialidadeEncontrada);//! NÃO TEM NADA IMPLEMENTADO AINDA

            await _especialidadeRepositorio.DeletarAsync(especialidadeEncontrada);
        }

        public async Task RestaurarEspecialidadeAsync(int especialidadeId, int usuarioId)
        {
            var especialidadeEncontrada = await _especialidadeRepositorio.ObterPorIdAsync(especialidadeId, usuarioId, false);

            ValidarExistenciaDaEspecialidade(especialidadeEncontrada);

            await _especialidadeRepositorio.RestaurarAsync(especialidadeEncontrada);
        }

        public async Task<IEnumerable<Especialidade>> ListarEspecialidadesPorUsuarioAsync(int usuarioId, bool ativo)
        {
            var listaEspecialidades = await _especialidadeRepositorio.ListarAsync(usuarioId, ativo);

            if (listaEspecialidades == null)
            {
                throw new Exception("Não existem especialidades cadastradas.");
            }
            return listaEspecialidades;
        }

        #endregion


        #region Uteis
        private static void ValidarInformacoesObrigatorias(Especialidade especialidade)
        {

            if (especialidade == null)
            {
                throw new Exception("Especialidade não pode ser vazia");
            }
            if (string.IsNullOrEmpty(especialidade.Nome))
            {
                throw new Exception("Nome da especialidade não pode ser vazio.");
            }
        }

        private static void ValidarExistenciaDaEspecialidade(Especialidade especialidadeEncontrada)
        {
            if (especialidadeEncontrada == null)
            {
                throw new Exception("Especialidade não encontrada.");
            }
        }

        private static Especialidade ValidarInformacoesPraAtualizacao(Especialidade especialidade, Especialidade especialidadeEncontrada)
        {

            if (string.IsNullOrEmpty(especialidade.Nome))
            {
                especialidadeEncontrada.Nome = especialidadeEncontrada.Nome;
            }
            else
            {
                especialidadeEncontrada.Nome = especialidade.Nome;
            }
            return especialidadeEncontrada;
        }


        private static void ValidarInformacoesParaExclusao(Especialidade especialidadeEcontrada)
        {
            //! QUANDO FOR REALIZAR A EXCLUSÃO VERIFICAR SE A ESPECIALIDADE ESTÁ SENDO UTILIZADA
        }
        #endregion

    }
}

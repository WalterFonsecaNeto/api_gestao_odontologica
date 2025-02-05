using ProjetoOdontologico.Dominio.Entidades;
using ProjetoOdontologico.Repositorio;


namespace ProjetoOdontologico.Aplicacao
{
    public class ProcedimentoAplicacao : IProcedimentoAplicacao
    {

        #region Atributos
        readonly IProcedimentoRepositorio _procedimentoRepositorio;
        readonly IUsuarioRepositorio _usuarioRepositorio;
        readonly IEspecialidadeRepositorio _especialidadeRepositorio;
        #endregion


        #region Construtores
        public ProcedimentoAplicacao(IProcedimentoRepositorio procedimentoRepositorio, IUsuarioRepositorio usuarioRepositorio, IEspecialidadeRepositorio especialidadeRepositorio)
        {
            _procedimentoRepositorio = procedimentoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _especialidadeRepositorio = especialidadeRepositorio;
        }
        #endregion


        #region Funções
        public async Task<int> CriarProcedimentoAsync(Procedimento procedimento)
        {
            ValidarInformacoesObrigatorias(procedimento);

            //! Fazer essa verificação para todos, pra ver se o usuarioId pertence a algum usuario mesmo
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(procedimento.UsuarioId, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var especialidadeEncontrada = await _especialidadeRepositorio.ObterPorIdAsync(procedimento.EspecialidadeId, procedimento.UsuarioId, true);

            if (especialidadeEncontrada == null)
            {
                throw new Exception("Especialidade não encontrada.");
            }

            int procedimentoSalvoId = await _procedimentoRepositorio.SalvarAsync(procedimento);

            return procedimentoSalvoId;
        }

        public async Task AtualizarProcedimentoAsync(Procedimento procedimento, int usuarioId, int procedimentoId)
        {
            var procedimentoEncontrado = await _procedimentoRepositorio.ObterPorIdAsync(procedimentoId, usuarioId, true);

            ValidarExistenciaDoProcedimento(procedimentoEncontrado);

            procedimentoEncontrado = ValidarInformacoesPraAtualizacao(procedimento, procedimentoEncontrado);

            await _procedimentoRepositorio.AtualizarAsync(procedimentoEncontrado);
        }

        public async Task<Procedimento> ObterProcedimentoPorIdAsync(int procedimentoId, int usuarioId, bool ativo)
        {
            var procedimentoEncontrado = await _procedimentoRepositorio.ObterPorIdAsync(procedimentoId, usuarioId, ativo);

            ValidarExistenciaDoProcedimento(procedimentoEncontrado);

            return procedimentoEncontrado;
        }

        public async Task DeletarProcedimentoAsync(int procedimentoId, int usuarioId)
        {

            var procedimentoEncontrado = await _procedimentoRepositorio.ObterPorIdAsync(procedimentoId, usuarioId, true);

            ValidarExistenciaDoProcedimento(procedimentoEncontrado);

            ValidarInformacoesParaExclusao(procedimentoEncontrado);

            await _procedimentoRepositorio.DeletarAsync(procedimentoEncontrado);
        }

        public async Task RestaurarProcedimentoAsync(int procedimentoId, int usuarioId)
        {
            var procedimentoEncontrado = await _procedimentoRepositorio.ObterPorIdAsync(procedimentoId, usuarioId, false);

            ValidarExistenciaDoProcedimento(procedimentoEncontrado);

            await _procedimentoRepositorio.RestaurarAsync(procedimentoEncontrado);
        }

        public async Task<IEnumerable<Procedimento>> ListarProcedimentosPorUsuarioAsync(int usuarioId, bool ativo)
        {
            var listaProcedimentos = await _procedimentoRepositorio.ListarAsync(usuarioId, ativo);

            if (listaProcedimentos.Count() == 0)
            {
                throw new Exception("Não existem procedimentos cadastrados.");
            }

            return listaProcedimentos;
        }

        #endregion


        #region Uteis
        private static void ValidarInformacoesObrigatorias(Procedimento procedimento)
        {

            if (procedimento == null)
            {
                throw new Exception("Procedimento não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(procedimento.Nome))
            {
                throw new Exception("Nome do Procedimento não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(procedimento.Valor.ToString()))
            {
                throw new Exception("Valor do procedimento não pode ser vazio.");
            }
            if (procedimento.Valor <= 0)
            {
                throw new Exception("Valor do procedimento não pode ser negativo ou zero.");
            }
            if (procedimento.EspecialidadeId <= 0)
            {
                throw new Exception("Id da especialidade não pode ser negativo ou zero.");
            }
            if (string.IsNullOrEmpty(procedimento.EspecialidadeId.ToString()))
            {
                throw new Exception("Id da especialidade não pode ser vazia.");
            }


        }

        private static void ValidarExistenciaDoProcedimento(Procedimento procedimentoEncontrado)
        {
            if (procedimentoEncontrado == null)
            {
                throw new Exception("Procedimento não encontrado.");
            }
        }

        private static Procedimento ValidarInformacoesPraAtualizacao(Procedimento procedimento, Procedimento procedimentoEncontrado)
        {
            if (string.IsNullOrEmpty(procedimento.Nome))
            {
                procedimentoEncontrado.Nome = procedimentoEncontrado.Nome;
            }
            else
            {
                procedimentoEncontrado.Nome = procedimento.Nome;
            }

            if (string.IsNullOrEmpty(procedimento.Valor.ToString()))
            {
                procedimentoEncontrado.Valor = procedimentoEncontrado.Valor;
            }
            else
            {
                procedimentoEncontrado.Valor = procedimento.Valor;
            }

            if (procedimento.Valor <= 0)
            {
                throw new Exception("Valor do procedimento não pode ser negativo ou zero.");
            }

            if (string.IsNullOrEmpty(procedimento.EspecialidadeId.ToString()))
            {
                procedimentoEncontrado.EspecialidadeId = procedimentoEncontrado.EspecialidadeId;
            }
            else
            {
                procedimentoEncontrado.EspecialidadeId = procedimento.EspecialidadeId;
            }

            if (string.IsNullOrEmpty(procedimento.Descricao))
            {
                procedimentoEncontrado.Descricao = procedimentoEncontrado.Descricao;
            }
            else
            {
                procedimentoEncontrado.Descricao = procedimento.Descricao;
            }

            return procedimentoEncontrado;
        }

        private static void ValidarInformacoesParaExclusao(Procedimento procedimentoEncontrado)
        {
            //! QUANDO FOR REALIZAR A EXCLUSÃO VERIFICAR SE O PROCEDIMENTO ESTÁ SENDO UTILIZADA
        }

        #endregion

    }
}
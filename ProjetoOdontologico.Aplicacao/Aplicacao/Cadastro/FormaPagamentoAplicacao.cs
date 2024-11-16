using ProjetoOdontologico.Dominio.Entidades;
using ProjetoOdontologico.Repositorio;


namespace ProjetoOdontologico.Aplicacao
{
    public class FormaPagamentoAplicacao : IFormaPagamentoAplicacao
    {

        #region Atributos
        readonly IFormaPagamentoRepositorio _formaPagamentoRepositorio;
        readonly IUsuarioRepositorio _usuarioRepositorio;

        #endregion


        #region Contrutores 
        public FormaPagamentoAplicacao(IFormaPagamentoRepositorio formaPagamentoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _formaPagamentoRepositorio = formaPagamentoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        #endregion


        #region Funções

        //! Fazer essa verificação para todos, pra ver se o usuarioId pertence a algum usuario mesmo
        public async Task<int> CriarFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            ValidarInformacoesObrigatorias(formaPagamento);

            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(formaPagamento.UsuarioId, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            int formaPagamentoSalvaId = await _formaPagamentoRepositorio.SalvarAsync(formaPagamento);

            return formaPagamentoSalvaId;
        }

        public async Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento, int usuarioId, int formaPagamentoId)
        {
            var formaPagamentoEncontrada = await _formaPagamentoRepositorio.ObterPorIdAsync(formaPagamentoId, usuarioId, true);

            ValidarExistenciaDaFormaDePagamento(formaPagamentoEncontrada);

            formaPagamentoEncontrada = ValidarInformacoesPraAtualizacao(formaPagamento, formaPagamentoEncontrada);//Valida as informações de forma que caso o usuario não queira alterar alguma area ele apenas deixa em branco.

            await _formaPagamentoRepositorio.AtualizarAsync(formaPagamentoEncontrada);
        }

        public async Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int formaPagamentoId, int usuarioId, bool ativo)
        {
            var formaPagamentoEncontrada = await _formaPagamentoRepositorio.ObterPorIdAsync(formaPagamentoId, usuarioId, ativo);

            ValidarExistenciaDaFormaDePagamento(formaPagamentoEncontrada);

            return formaPagamentoEncontrada;
        }

        public async Task DeletarFormaPagamentoAsync(int formaPagamentoId, int usuarioId)
        {
            var formaPagamentoEncontrada = await _formaPagamentoRepositorio.ObterPorIdAsync(formaPagamentoId, usuarioId, true);

            ValidarExistenciaDaFormaDePagamento(formaPagamentoEncontrada);

            ValidarInformacoesParaExclusao(formaPagamentoEncontrada);

            await _formaPagamentoRepositorio.DeletarAsync(formaPagamentoEncontrada);
        }

        public async Task RestaurarFormaPagamentoAsync(int formaPagamentoId, int usuarioId)
        {
            var formaPagamentoEncontrada = await _formaPagamentoRepositorio.ObterPorIdAsync(formaPagamentoId, usuarioId, false);

            ValidarExistenciaDaFormaDePagamento(formaPagamentoEncontrada);

            await _formaPagamentoRepositorio.RestaurarAsync(formaPagamentoEncontrada);
        }

        public async Task<IEnumerable<FormaPagamento>> ListarFormasPagamentoPorUsuarioAsync(int usuarioId, bool ativo)
        {
            var listaFormasPagamento = await _formaPagamentoRepositorio.ListarAsync(usuarioId, ativo);

            if (listaFormasPagamento == null)
            {
                throw new Exception("Não existem formas de pagamento cadastradas.");
            }
            return listaFormasPagamento;
        }

        #endregion


        #region Uteis
        private static void ValidarInformacoesObrigatorias(FormaPagamento formaPagamento)
        {

            if (formaPagamento == null)
            {
                throw new Exception("Forma de pagamento não pode ser vazia");
            }
            if (string.IsNullOrEmpty(formaPagamento.Nome))
            {
                throw new Exception("Nome da forma de pagamento não pode ser vazia.");
            }


        }

        private static void ValidarExistenciaDaFormaDePagamento(FormaPagamento formaPagamentoEncontrada)
        {
            if (formaPagamentoEncontrada == null)
            {
                throw new Exception("Forma de pagamento não encontrada.");
            }
        }

        private static FormaPagamento ValidarInformacoesPraAtualizacao(FormaPagamento formaPagamento, FormaPagamento formaPagamentoEncontrada)
        {
            if (!string.IsNullOrEmpty(formaPagamento.Nome))
            {
                formaPagamentoEncontrada.Nome = formaPagamento.Nome;
            }
            return formaPagamentoEncontrada;
        }

        private static void ValidarInformacoesParaExclusao(FormaPagamento formaPagamentoEncontrada)
        {
            //! QUANDO FOR REALIZAR A EXCLUSÃO VERIFICAR SE A FORMA DE PAGAMENTO ESTÁ SENDO UTILIZADA
        }
        #endregion

    }
}
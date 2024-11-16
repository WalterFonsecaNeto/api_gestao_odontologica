using ProjetoOdontologico.Dominio.Entidades;
using ProjetoOdontologico.Repositorio;

namespace ProjetoOdontologico.Aplicacao
{
    public class PacienteAplicacao : IPacienteAplicacao
    {

        #region Atributos
        readonly IPacienteRepositorio _pacienteRepositorio;
        readonly IUsuarioRepositorio _usuarioRepositorio;
        #endregion


        #region Contrutores 
        public PacienteAplicacao(IPacienteRepositorio pacienteRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _pacienteRepositorio = pacienteRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }
        #endregion


        #region Funções
        public async Task<int> CriarPacienteAsync(Paciente paciente)
        {
            ValidarInformacoesObrigatorias(paciente);

            //! Fazer essa verificação para todos, pra ver se o usuarioId pertence a algum usuario mesmo
            var usuarioEncontrado = await _usuarioRepositorio.ObterPorIdAsync(paciente.UsuarioId, true);

            if (usuarioEncontrado == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            int pacienteSalvoId = await _pacienteRepositorio.SalvarAsync(paciente);

            return pacienteSalvoId;
        }
        
        public async Task AtualizarPacienteAsync(Paciente paciente, int usuarioId, int pacienteId)
        {
            var pacienteEncontrado = await _pacienteRepositorio.ObterPorIdAsync(pacienteId, usuarioId, true);

            ValidarExistenciaDoPaciente(pacienteEncontrado);

            pacienteEncontrado = ValidarInformacoesPraAtualizacao(paciente, pacienteEncontrado);//Valida as informações de forma que caso o usuario não queira alterar alguma area ele apenas deixa em branco.

            await _pacienteRepositorio.AtualizarAsync(pacienteEncontrado);
        }

        public async Task<Paciente> ObterPacientePorIdAsync(int pacienteId, int usuarioId, bool ativo)
        {
            var pacienteEncontrado = await _pacienteRepositorio.ObterPorIdAsync(pacienteId, usuarioId, ativo);

            ValidarExistenciaDoPaciente(pacienteEncontrado);

            return pacienteEncontrado;
        }

        public async Task DeletarPacienteAsync(int pacienteId, int usuarioId)
        {
            var pacienteEncontrado = await _pacienteRepositorio.ObterPorIdAsync(pacienteId, usuarioId, true);

            ValidarExistenciaDoPaciente(pacienteEncontrado);

            ValidarInformacoesParaExclusao(pacienteEncontrado);//! NÃO TEM NADA IMPLEMENTADO AINDA

            await _pacienteRepositorio.DeletarAsync(pacienteEncontrado);
        }

        public async Task RestaurarPacienteAsync(int pacienteId, int usuarioId)
        {
            var pacienteEncontrado = await _pacienteRepositorio.ObterPorIdAsync(pacienteId, usuarioId, false);

            ValidarExistenciaDoPaciente(pacienteEncontrado);

            await _pacienteRepositorio.RestaurarAsync(pacienteEncontrado);
        }

        public async Task<IEnumerable<Paciente>> ListarPacientesPorUsuarioAsync(int usuarioId, bool ativo)
        {
            var listaPaciente = await _pacienteRepositorio.ListarPorUsuarioAsync(usuarioId, ativo);

            if (listaPaciente == null)
            {
                throw new Exception("Não existem pacientes cadastrados.");
            }
            return listaPaciente;
        }

        #endregion


        #region Uteis
        private static void ValidarInformacoesObrigatorias(Paciente paciente)
        {

            if (paciente == null)
            {
                throw new Exception("Paciente não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(paciente.UsuarioId.ToString()))
            {
                throw new Exception("UsuarioId do paciente não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(paciente.Nome))
            {
                throw new Exception("Nome do paciente não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(paciente.CPF))
            {
                throw new Exception("CPF do paciente não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(paciente.Endereco))
            {
                throw new Exception("Endereço do paciente não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(paciente.DataNascimento.ToString()))
            {
                throw new Exception("Data de nacimento do paciente não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(paciente.Genero))
            {
                throw new Exception("Genero do paciente não pode ser vazio.");
            }

        }

        private static void ValidarExistenciaDoPaciente(Paciente pacienteEncontrado)
        {
            if (pacienteEncontrado == null)
            {
                throw new Exception("Paciente não encontrado.");
            }
        }

        private static Paciente ValidarInformacoesPraAtualizacao(Paciente paciente, Paciente pacienteEncontrado)
        {
            if (!string.IsNullOrEmpty(paciente.Nome))
            {
                pacienteEncontrado.Nome = paciente.Nome;
            }

            if (!string.IsNullOrEmpty(paciente.CPF))
            {
                pacienteEncontrado.CPF = paciente.CPF;
            }

            if (!string.IsNullOrEmpty(paciente.Endereco))
            {
                pacienteEncontrado.Endereco = paciente.Endereco;
            }

            if (paciente.DataNascimento.HasValue)
            {
                pacienteEncontrado.DataNascimento = paciente.DataNascimento;
            }

            if (!string.IsNullOrEmpty(paciente.Genero))
            {
                pacienteEncontrado.Genero = paciente.Genero;
            }

            if (!string.IsNullOrEmpty(paciente.Telefone))
            {
                pacienteEncontrado.Telefone = paciente.Telefone;
            }

            if (!string.IsNullOrEmpty(paciente.Email))
            {
                pacienteEncontrado.Email = paciente.Email;
            }

            if (!string.IsNullOrEmpty(paciente.HistoricoMedico))
            {
                pacienteEncontrado.HistoricoMedico = paciente.HistoricoMedico;
            }

            return pacienteEncontrado;
        }

        private static void ValidarInformacoesParaExclusao(Paciente pacienteEncontrado)
        {
            //! FAZER A VERIFICAÇÃO PARA VER SE O PACIENTE TEM ALGUM AGENDAMENTO OU ORÇAMENTO PORQUE CASO TENHO NÃO SERA POSSIVEL EXCLUIR O PACIENTE
        }
        #endregion

    }
}
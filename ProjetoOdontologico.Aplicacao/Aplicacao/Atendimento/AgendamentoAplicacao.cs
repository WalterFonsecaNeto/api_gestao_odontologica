using System.Diagnostics;
using System.Xml;
using ProjetoOdontologico.Dominio.Entidades;
using ProjetoOdontologico.Repositorio;

namespace ProjetoOdontologico.Aplicacao
{
    public class AgendamentoAplicacao : IAgendamentoAplicacao
    {

        #region Atributos
        readonly IAgendamentoRepositorio _agendamentoRepositorio;

        #endregion


        #region Contrutores 
        public AgendamentoAplicacao(IAgendamentoRepositorio agendamentoRepositorio)
        {
            _agendamentoRepositorio = agendamentoRepositorio;
        }
        #endregion


        #region Funções
        public async Task<int> CriarAgendamentoAsync(Agendamento agendamento)
        {
            ValidarInformacoesObrigatorias(agendamento);

            var agendamentoCriado = await _agendamentoRepositorio.SalvarAsync(agendamento);

            return agendamentoCriado;
        }

        public async Task AtualizarAgendamentoAsync(Agendamento agendamento, int agendamentoId, int usuarioId)
        {
            var agendamentoEncontrado = await _agendamentoRepositorio.ObterPorIdAsync(agendamentoId, usuarioId, true);

            ValidarExistenciaDoAgendamento(agendamentoEncontrado);

            agendamentoEncontrado.PacienteId = agendamento.PacienteId;
            agendamentoEncontrado.DataHora = agendamento.DataHora;
            agendamentoEncontrado.Status = agendamento.Status;
            agendamentoEncontrado.Descricao = agendamento.Descricao;

            await _agendamentoRepositorio.AtualizarAsync(agendamentoEncontrado);
        }
        public async Task AtualizarStatusAgendamentoAsync(string status, int agendamentoId, int usuarioId)
        {
            var agendamentoEncontrado = await _agendamentoRepositorio.ObterPorIdAsync(agendamentoId, usuarioId, true);

            if (string.IsNullOrEmpty(status))
            {
                throw new ArgumentException("Status do agendamento não pode ser vazio.");
            }

            ValidarExistenciaDoAgendamento(agendamentoEncontrado);

            agendamentoEncontrado.Status = status;

            await _agendamentoRepositorio.AtualizarAsync(agendamentoEncontrado);
        }

        public async Task<Agendamento> ObterAgendamentoPorIdAsync(int agendamentoId, int usuarioId, bool ativo)
        {
            var agendamentoEncontrado = await _agendamentoRepositorio.ObterPorIdAsync(agendamentoId, usuarioId, ativo);

            ValidarExistenciaDoAgendamento(agendamentoEncontrado);

            return agendamentoEncontrado;
        }

        public async Task DeletarAgendamentoAsync(int agendamentoId, int usuarioId)
        {
            var agendamentoEncontrado = await _agendamentoRepositorio.ObterPorIdAsync(agendamentoId, usuarioId, true);

            ValidarExistenciaDoAgendamento(agendamentoEncontrado);

            await _agendamentoRepositorio.DeletarAsync(agendamentoEncontrado);
        }

        public async Task RestaurarAgendamentoAsync(int agendamentoId, int usuarioId)
        {
            var agendamentoEncontrado = await _agendamentoRepositorio.ObterPorIdAsync(agendamentoId, usuarioId, false);

            ValidarExistenciaDoAgendamento(agendamentoEncontrado);

            await _agendamentoRepositorio.RestaurarAsync(agendamentoEncontrado);
        }


        public async Task<IEnumerable<Agendamento>> ListarAgendamentoPorUsuarioIdAsync(int usuarioId, bool ativo)
        {
            var listaAgendamentos = await _agendamentoRepositorio.ListarPorUsuarioIdAsync(usuarioId, ativo);

            if (listaAgendamentos.Count() == 0)   
            {
                throw new Exception("Não existem agendamentos cadastrados.");
            }
            return listaAgendamentos;
        }

        public async Task<IEnumerable<Agendamento>> ListarAgendamentoPorPacienteIdAsync(int usuarioId, int pacienteId, bool ativo)
        {
            var listaAgendamentos = await _agendamentoRepositorio.ListarPorPacienteIdAsync(usuarioId, pacienteId, ativo);

            if (listaAgendamentos.Count() == 0)
            {
                throw new Exception("Não existem agendamentos por pacienteId cadastrados.");
            }
            return listaAgendamentos;
        }

        #endregion


        #region Uteis
        private static void ValidarInformacoesObrigatorias(Agendamento agendamento)
        {

            if (agendamento == null)
            {
                throw new Exception("agendamento não pode ser vazia");
            }
            if (agendamento.UsuarioId <= 0)
            {
                throw new Exception("UsuarioId do agendamento não pode ser vazio.");
            }
            if (agendamento.PacienteId <= 0)
            {
                throw new Exception("PacienteId do agendamento não pode ser vazio.");
            }
            if (agendamento.DataHora == DateTime.MinValue)
            {
                throw new Exception("DataHora do agendamento não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(agendamento.Status))
            {
                throw new Exception("Status do agendamento não pode ser vazio.");
            }
            if (string.IsNullOrEmpty(agendamento.Descricao))
            {
                throw new Exception("Descrição do agendamento não pode ser vazio.");
            }





        }



        private static void ValidarExistenciaDoAgendamento(Agendamento agendamentoEncontrado)
        {
            if (agendamentoEncontrado == null)
            {
                throw new Exception("Agendamento não encontrado.");
            }
        }



        #endregion

    }


}

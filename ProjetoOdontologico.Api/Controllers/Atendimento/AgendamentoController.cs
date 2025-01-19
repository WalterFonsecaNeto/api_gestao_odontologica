using Microsoft.AspNetCore.Mvc;
using ProjetoOdontologico.Api.Models;
using ProjetoOdontologico.Aplicacao;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        #region Atributos
        private readonly IAgendamentoAplicacao _agendamentoAplicacao;
        #endregion


        #region Construtores
        public AgendamentoController(IAgendamentoAplicacao agendamentoAplicacao)
        {
            _agendamentoAplicacao = agendamentoAplicacao;
        }
        #endregion


        #region Funções


        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> CriarAgendamentoAsync([FromBody] AgendamentoCriar agendamentoCriar)
        {
            try
            {
                var agendamentoDominio = new Agendamento()
                {
                    UsuarioId = agendamentoCriar.UsuarioId,
                    PacienteId = agendamentoCriar.PacienteId,
                    DataHora = agendamentoCriar.DataHora,
                    Status = agendamentoCriar.Status,
                    Descricao = agendamentoCriar.Descricao
                };

                var agendamentoId = await _agendamentoAplicacao.CriarAgendamentoAsync(agendamentoDominio);

                return Ok(agendamentoId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar agendamento: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("AtualizarPorAgendamentoId/{agendamentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> AtualizarEspecialidadeAsync([FromRoute] int usuarioId, int agendamentoId, [FromBody] AgendamentoAtualizar agendamentoAtualizar)
        {
            try
            {
                var agendamentoDominio = new Agendamento()
                {
                    PacienteId = agendamentoAtualizar.PacienteId,
                    DataHora = agendamentoAtualizar.DataHora,
                    Status = agendamentoAtualizar.Status,
                    Descricao = agendamentoAtualizar.Descricao
                };

                await _agendamentoAplicacao.AtualizarAgendamentoAsync(agendamentoDominio, usuarioId, agendamentoId);

                return Ok("Agendamento atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar agendamento: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("DeletarPorAgendamentoId/{agendamentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> DeletarAgendamentoAsync([FromRoute] int agendamentoId, int usuarioId)
        {
            try
            {
                await _agendamentoAplicacao.DeletarAgendamentoAsync(agendamentoId, usuarioId);

                return Ok("Agendamento deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar agendamento: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("RestaurarPorAgendamentoId/{agendamentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> RestaurarAgendamentoAsync([FromRoute] int agendamentoId, int usuarioId)
        {
            try
            {
                await _agendamentoAplicacao.RestaurarAgendamentoAsync(agendamentoId, usuarioId);

                return Ok("Agendamento restaurada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao restaurar agendamento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ObterPorAgendamentoId/{agendamentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> ObterAgendamentoPorIdAsync([FromRoute] int agendamentoId, int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var agendamentoDominio = await _agendamentoAplicacao.ObterAgendamentoPorIdAsync(agendamentoId, usuarioId, ativo);

                var agendamentoResponse = new AgendamentoResponse()
                {
                    Id = agendamentoDominio.Id,
                    PacienteId = agendamentoDominio.PacienteId,
                    DataHora = agendamentoDominio.DataHora,
                    Status = agendamentoDominio.Status,
                    Descricao = agendamentoDominio.Descricao

                };
                return Ok(agendamentoResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter agendamento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ListarPorUsuarioId/{usuarioId}")]
        public async Task<IActionResult> ListarAgendamentoPorUsuarioIdAsync([FromRoute] int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var listaAgendamentos = await _agendamentoAplicacao.ListarAgendamentoPorUsuarioIdAsync(usuarioId, ativo);

                var listaAgendamentoResponse = listaAgendamentos.Select(agendamento => new AgendamentoResponse()
                {
                    Id = agendamento.Id,
                    PacienteId = agendamento.PacienteId,
                    DataHora = agendamento.DataHora,
                    Status = agendamento.Status,
                    Descricao = agendamento.Descricao
                });
                return Ok(listaAgendamentoResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar especialidades: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ListarPorPacienteId/{pacienteId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> ListarAgendamentoPorPacienteIdAsync([FromRoute] int pacienteId, int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var listaAgendamentos = await _agendamentoAplicacao.ListarAgendamentoPorPacienteIdAsync(pacienteId, usuarioId, ativo);

                var listaAgendamentoResponse = listaAgendamentos.Select(agendamento => new AgendamentoResponse()
                {
                    Id = agendamento.Id,
                    PacienteId = agendamento.PacienteId,
                    DataHora = agendamento.DataHora,
                    Status = agendamento.Status,
                    Descricao = agendamento.Descricao
                });
                return Ok(listaAgendamentoResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar especialidades: {ex.Message}");
            }
        }

        #endregion

    }
}

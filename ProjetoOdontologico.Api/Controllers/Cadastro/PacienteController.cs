using Microsoft.AspNetCore.Mvc;
using ProjetoOdontologico.Api.Models;
using ProjetoOdontologico.Aplicacao;
using ProjetoOdontologico.Dominio.Entidades;


namespace ProjetoOdontologico.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        #region Atributos
        private readonly IPacienteAplicacao _pacienteAplicacao;
        #endregion


        #region Construtores
        public PacienteController(IPacienteAplicacao pacienteAplicacao)
        {
            _pacienteAplicacao = pacienteAplicacao;
        }
        #endregion


        #region Funções
        [HttpGet]
        [Route("Obter/{pacienteId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> ObterPacientePorIdAsync([FromRoute] int pacienteId, int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var pacienteDominio = await _pacienteAplicacao.ObterPacientePorIdAsync(pacienteId, usuarioId, ativo);

                var pacienteResponse = new PacienteResponse()
                {
                    Id = pacienteDominio.Id,
                    Nome = pacienteDominio.Nome,
                    CPF = pacienteDominio.CPF,
                    DataNascimento = pacienteDominio.DataNascimento,
                    Endereco = pacienteDominio.Endereco,
                    Telefone = pacienteDominio.Telefone,
                    Genero = pacienteDominio.Genero,
                    Email = pacienteDominio.Email,
                    HistoricoMedico = pacienteDominio.HistoricoMedico
                };
                return Ok(pacienteResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter paciente: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("Listar/Usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPacientesPorUsuarioAsync([FromRoute] int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var listaPaciente = await _pacienteAplicacao.ListarPacientesPorUsuarioAsync(usuarioId, ativo);

                var listaPacientesResponse = listaPaciente.Select(paciente => new PacienteResponse()
                {
                    Id = paciente.Id,
                    Nome = paciente.Nome,
                    CPF = paciente.CPF,
                    DataNascimento = paciente.DataNascimento,
                    Endereco = paciente.Endereco,
                    Telefone = paciente.Telefone,
                    Genero = paciente.Genero,
                    Email = paciente.Email,
                    HistoricoMedico = paciente.HistoricoMedico
                });
                return Ok(listaPacientesResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar pacientes: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> CriarPacienteAsync([FromBody] PacienteCriar pacienteCriar)
        {
            try
            {
                var pacienteDominio = new Paciente()
                {
                    UsuarioId = pacienteCriar.UsuarioId,
                    Nome = pacienteCriar.Nome,
                    CPF = pacienteCriar.CPF,
                    DataNascimento = pacienteCriar.DataNascimento,
                    Endereco = pacienteCriar.Endereco,
                    Telefone = pacienteCriar.Telefone,
                    Genero = pacienteCriar.Genero,
                    Email = pacienteCriar.Email,
                    HistoricoMedico = pacienteCriar.HistoricoMedico
                };

                var pacienteId = await _pacienteAplicacao.CriarPacienteAsync(pacienteDominio);

                return Ok(pacienteId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar paciente: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Atualizar/{pacienteId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> AtualizarPacienteAsync([FromRoute] int usuarioId, int pacienteId, [FromBody] PacienteAtualizar pacienteAtualizar)
        {
            try
            {
                var pacienteDominio = new Paciente()
                {
                    Nome = pacienteAtualizar.Nome,
                    CPF = pacienteAtualizar.CPF,
                    DataNascimento = pacienteAtualizar.DataNascimento,
                    Endereco = pacienteAtualizar.Endereco,
                    Telefone = pacienteAtualizar.Telefone,
                    Genero = pacienteAtualizar.Genero,
                    Email = pacienteAtualizar.Email,
                    HistoricoMedico = pacienteAtualizar.HistoricoMedico
                };

                await _pacienteAplicacao.AtualizarPacienteAsync(pacienteDominio, usuarioId, pacienteId);

                return Ok("Paciente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar paciente: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("Deletar/{pacienteId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> DeletarPacienteAsync([FromRoute] int pacienteId, int usuarioId)
        {
            try
            {
                await _pacienteAplicacao.DeletarPacienteAsync(pacienteId, usuarioId);

                return Ok("Paciente deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar paciente: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Restaurar/{pacienteId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> RestaurarProcedimentoAsync([FromRoute] int pacienteId, int usuarioId)
        {
            try
            {
                await _pacienteAplicacao.RestaurarPacienteAsync(pacienteId, usuarioId);

                return Ok("Paciente restaurado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao restaurar paciente: {ex.Message}");
            }
        }



        #endregion
    }
}
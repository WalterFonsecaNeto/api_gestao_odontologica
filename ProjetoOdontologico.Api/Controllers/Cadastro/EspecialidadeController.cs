using Microsoft.AspNetCore.Mvc;
using ProjetoOdontologico.Api.Models;
using ProjetoOdontologico.Aplicacao;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadeController : ControllerBase
    {
        #region Atributos
        private readonly IEspecialidadeAplicacao _especialidadeAplicacao;
        #endregion


        #region Construtores
        public EspecialidadeController(IEspecialidadeAplicacao especialidadeAplicacao)
        {
            _especialidadeAplicacao = especialidadeAplicacao;
        }
        #endregion


        #region Funções
        [HttpGet]
        [Route("Obter/{especialidadeId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> ObterEspecialidadePorIdAsync([FromRoute] int especialidadeId, int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var especialidadeDominio = await _especialidadeAplicacao.ObterEspecialidadePorIdAsync(especialidadeId, usuarioId, ativo);

                var especialidadeResponse = new EspecialidadeResponse()
                {
                    Id = especialidadeDominio.Id,
                    Nome = especialidadeDominio.Nome
                };
                return Ok(especialidadeResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter especialidade: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("Listar/Usuario/{usuarioId}")]
        public async Task<IActionResult> ListarEspecialidadesPorUsuarioAsync([FromRoute] int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var listaEspecialidade = await _especialidadeAplicacao.ListarEspecialidadesPorUsuarioAsync(usuarioId, ativo);

                var listaEspecialidadesResponse = listaEspecialidade.Select(especialidade => new EspecialidadeResponse()
                {
                    Id = especialidade.Id,
                    Nome = especialidade.Nome
                });
                return Ok(listaEspecialidadesResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar especialidades: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> CriarEspecialidadeAsync([FromBody] EspecialidadeCriar especialidadeCriar)
        {
            try
            {
                var especialidadeDominio = new Especialidade()
                {
                    UsuarioId = especialidadeCriar.UsuarioId,
                    Nome = especialidadeCriar.Nome
                };

                var especialidadeId = await _especialidadeAplicacao.CriarEspecialidadeAsync(especialidadeDominio);

                return Ok(especialidadeId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar especialidade: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Atualizar/{especialidadeId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> AtualizarEspecialidadeAsync([FromRoute] int usuarioId, int especialidadeId, [FromBody] EspecialidadeAtualizar especialidadeAtualizar)
        {
            try
            {
                var especialidadeDominio = new Especialidade()
                {
                    Nome = especialidadeAtualizar.Nome
                };

                await _especialidadeAplicacao.AtualizarEspecialidadeAsync(especialidadeDominio, usuarioId, especialidadeId);

                return Ok("Especialidade atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar especialidade: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("Deletar/{especialidadeId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> DeletarEspecialidadeAsync([FromRoute] int especialidadeId, int usuarioId)
        {
            try
            {
                await _especialidadeAplicacao.DeletarEspecialidadeAsync(especialidadeId, usuarioId);

                return Ok("Especialidade deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar especialidade: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Restaurar/{especialidadeId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> RestaurarEspecialidadeAsync([FromRoute] int especialidadeId, int usuarioId)
        {
            try
            {
                await _especialidadeAplicacao.RestaurarEspecialidadeAsync(especialidadeId, usuarioId);

                return Ok("Especialidade restaurada com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao restaurar especialidade: {ex.Message}");
            }
        }

        #endregion
    
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjetoOdontologico.Api.Models;
using ProjetoOdontologico.Aplicacao;
using ProjetoOdontologico.Dominio.Entidades;


namespace ProjetoOdontologico.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedimentoController : ControllerBase
    {
        #region Atributos
        private readonly IProcedimentoAplicacao _procedimentoAplicacao;
        #endregion


        #region Construtores
        public ProcedimentoController(IProcedimentoAplicacao procedimentoAplicacao)
        {
            _procedimentoAplicacao = procedimentoAplicacao;
        }

        #endregion


        #region Funções
        [HttpGet]
        [Route("Obter/{procedimentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> ObterProcedimentoPorIdAsync([FromRoute] int procedimentoId, int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var procedimentoDominio = await _procedimentoAplicacao.ObterProcedimentoPorIdAsync(procedimentoId, usuarioId, ativo);

                var procedimentoResponse = new ProcedimentoResponse()
                {
                    Id = procedimentoDominio.Id,
                    Nome = procedimentoDominio.Nome,
                    Descricao = procedimentoDominio.Descricao,
                    Valor = procedimentoDominio.Valor,
                    EspecialidadeId = procedimentoDominio.EspecialidadeId
                };
                return Ok(procedimentoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter procedimento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Listar/Usuario/{usuarioId}")]
        public async Task<IActionResult> ListarProcedimentosAsync([FromRoute] int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var listaProcedimentos = await _procedimentoAplicacao.ListarProcedimentosPorUsuarioAsync(usuarioId, ativo);

                var listaProcedimentosResponse = listaProcedimentos.Select(procedimento => new ProcedimentoResponse()
                {
                    Id = procedimento.Id,
                    Nome = procedimento.Nome,
                    Descricao = procedimento.Descricao,
                    Valor = procedimento.Valor,
                    EspecialidadeId = procedimento.EspecialidadeId
                });

                return Ok(listaProcedimentosResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar procedimento: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> CriarProcedimentoAsync([FromBody] ProcedimentoCriar procedimentoCriar)
        {
            try
            {
                var procedimentoDominio = new Procedimento()
                {
                    UsuarioId = procedimentoCriar.UsuarioId,
                    Nome = procedimentoCriar.Nome,
                    Descricao = procedimentoCriar.Descricao,
                    Valor = procedimentoCriar.Valor,
                    EspecialidadeId = procedimentoCriar.EspecialidadeId
                };

                var procedimentoId = await _procedimentoAplicacao.CriarProcedimentoAsync(procedimentoDominio);

                return Ok($"Procedimento de ID: {procedimentoId} criado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar procedimento: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Atualizar/{procedimentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> AtualizarProcedimentoAsync([FromRoute] int procedimentoId, int usuarioId, [FromBody] ProcedimentoAtualizar procedimentoAtualizar)
        {
            try
            {
                var procedimentoDominio = new Procedimento()
                {
                    Nome = procedimentoAtualizar.Nome,
                    Descricao = procedimentoAtualizar.Descricao,
                    Valor = procedimentoAtualizar.Valor,
                    EspecialidadeId = procedimentoAtualizar.EspecialidadeId
                };

                await _procedimentoAplicacao.AtualizarProcedimentoAsync(procedimentoDominio, usuarioId, procedimentoId);

                return Ok("Procedimento atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar procedimento: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("Deletar/{procedimentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> DeletarProcedimentoAsync([FromRoute] int procedimentoId, int usuarioId)
        {
            try
            {
                await _procedimentoAplicacao.DeletarProcedimentoAsync(procedimentoId, usuarioId);

                return Ok("Procedimento deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar procedimento: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Restaurar/{procedimentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> RestaurarProcedimentoAsync([FromRoute] int procedimentoId, int usuarioId)
        {
            try
            {
                await _procedimentoAplicacao.RestaurarProcedimentoAsync(procedimentoId, usuarioId);

                return Ok("Procedimento restaurado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao restaurar procedimento: {ex.Message}");
            }
        }
        #endregion



    }
}
using Microsoft.AspNetCore.Mvc;
using ProjetoOdontologico.Api.Models;
using ProjetoOdontologico.Aplicacao;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormaPagamentoController : ControllerBase
    {
        #region Atributos
        private readonly IFormaPagamentoAplicacao _formaPagamentoAplicacao;

        #endregion

        #region Construtores
        public FormaPagamentoController(IFormaPagamentoAplicacao formaPagamentoAplicacao)
        {
            _formaPagamentoAplicacao = formaPagamentoAplicacao;
        }
        #endregion

        #region Funções
        [HttpGet]
        [Route("Obter/{formaPagamentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> ObterFormaPagamentoPorIdAsync([FromRoute] int formaPagamentoId, int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var formaPagamentoDominio = await _formaPagamentoAplicacao.ObterFormaPagamentoPorIdAsync(formaPagamentoId, usuarioId, ativo);

                var formaPagamentoResponse = new FormaPagamentoResponse()
                {
                    Id = formaPagamentoDominio.Id,
                    Nome = formaPagamentoDominio.Nome
                };
                return Ok(formaPagamentoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao obter forma de pagamento: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Listar/Usuario/{usuarioId}")]
        public async Task<IActionResult> ListarFormasPagamentoPorUsuarioAsync([FromRoute] int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var listaFormaPagamento = await _formaPagamentoAplicacao.ListarFormasPagamentoPorUsuarioAsync(usuarioId, ativo);

                var listaFormasPagamentoResponse = listaFormaPagamento.Select(formaPagamento => new FormaPagamentoResponse()
                {
                    Id = formaPagamento.Id,
                    Nome = formaPagamento.Nome
                });
                return Ok(listaFormasPagamentoResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao listar formas de pagamento: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> CriarFormaPagamentoAsync([FromBody] FormaPagamentoCriar formaPagamentoCriar)
        {
            try
            {
                var formaPagamentoDominio = new FormaPagamento()
                {
                    UsuarioId = formaPagamentoCriar.UsuarioId,
                    Nome = formaPagamentoCriar.Nome
                };

                var formaPagamentoId = await _formaPagamentoAplicacao.CriarFormaPagamentoAsync(formaPagamentoDominio);

                return Ok(formaPagamentoId);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar forma de pagamento: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Atualizar{formaPagamentoId}/Usuario{usuarioId}")]
        public async Task<IActionResult> AtualizarFormaPagamentoAsync([FromRoute] int usuarioId, int formaPagamentoId, [FromBody] FormaPagamentoAtualizar formaPagamentoAtualizar)
        {
            try
            {
                var formaPagamentoDominio = new FormaPagamento()
                {
                    Nome = formaPagamentoAtualizar.Nome
                };

                await _formaPagamentoAplicacao.AtualizarFormaPagamentoAsync(formaPagamentoDominio, usuarioId, formaPagamentoId);

                return Ok("Forma de pagamento atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar forma de pagamento: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Deletar/{formaPagamentoId}/Usuario/{usuarioId}")]
        public async Task<IActionResult> DeletarFormaPagamentoAsync([FromRoute] int formaPagamentoId, int usuarioId)
        {
            try
            {
                await _formaPagamentoAplicacao.DeletarFormaPagamentoAsync(formaPagamentoId, usuarioId);

                return Ok("Forma de pagamento deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar forma de pagamento: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Restaurar/{formaPagamentoId}/usuario/{usuarioId}")]
        public async Task<IActionResult> RestaurarFormaPagamentoAsync([FromRoute] int formaPagamentoId, int usuarioId)
        {
            try
            {
                await _formaPagamentoAplicacao.RestaurarFormaPagamentoAsync(formaPagamentoId, usuarioId);

                return Ok("Forma de pagamento restaurada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao restaurar forma de pagamento: {ex.Message}");
            }
        }

        #endregion
    }
}

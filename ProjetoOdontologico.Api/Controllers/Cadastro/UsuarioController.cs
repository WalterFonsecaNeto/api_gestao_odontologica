using Microsoft.AspNetCore.Mvc;
using ProjetoOdontologico.Api.Models;
using ProjetoOdontologico.Aplicacao;
using ProjetoOdontologico.Dominio.Entidades;


namespace ProjetoOdontologico.Api
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : ControllerBase
    {
        #region Atributos
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        #endregion


        #region Construtores
        public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
        }
        #endregion


        #region Funções
        [HttpGet]
        [Route("Obter/{usuarioId}")]
        public async Task<ActionResult> ObterUsuarioPorIdAsync([FromRoute] int usuarioId, [FromQuery] bool ativo)
        {
            try
            {
                var usuarioDominio = await _usuarioAplicacao.ObterUsuarioPorIdAsync(usuarioId, ativo);

                var usuarioResposta = new UsuarioResponse()
                {
                    Id = usuarioDominio.Id,
                    Nome = usuarioDominio.Nome,
                    Email = usuarioDominio.Email,
                };

                return Ok(usuarioResposta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("Criar")]
        public async Task<ActionResult> CriarUsuarioAsync([FromBody] UsuarioCriar usuarioCriar)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Nome = usuarioCriar.Nome,
                    Email = usuarioCriar.Email,
                    Senha = usuarioCriar.Senha,
                };

                var usuarioId = await _usuarioAplicacao.CriarUsuarioAsync(usuarioDominio);

                return Ok(usuarioId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Atualizar/{usuarioId}")]
        public async Task<ActionResult> AtualizarUsuarioAsync([FromRoute] int usuarioId, [FromBody] UsuarioAtualizar usuarioAtualizar)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Nome = usuarioAtualizar.Nome,
                    Email = usuarioAtualizar.Email,
                };

                await _usuarioAplicacao.AtualizarUsuarioAsync(usuarioDominio, usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("AtualizarSenha/{usuarioId}")]
        public async Task<ActionResult> AtualizarSenhaUsuarioAsync([FromRoute] int usuarioId, [FromBody] UsuarioAtualizarSenha usuarioAtualizarSenha)
        {
            try
            {
                string senhaNova = usuarioAtualizarSenha.SenhaNova;
                string senhaAntiga = usuarioAtualizarSenha.SenhaAntiga;

                await _usuarioAplicacao.AlterarSenhaAsync(senhaNova, senhaAntiga, usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("Deletar/{usuarioId}")]
        public async Task<ActionResult> DeletarUsuarioAsync([FromRoute] int usuarioId)
        {
            try
            {
                await _usuarioAplicacao.DeletarUsuarioAsync(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Restaurar/{usuarioId}")]
        public async Task<ActionResult> RestaurarUsuarioAsync([FromRoute] int usuarioId)
        {
            try
            {
                await _usuarioAplicacao.RestaurarUsuarioAsync(usuarioId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult> ListarUsuariosAsync([FromQuery] bool ativos)
        {
            try
            {
                var usuariosDominio = await _usuarioAplicacao.ListarUsuariosAsync(ativos);

                var usuarios = usuariosDominio.Select(usuario => new UsuarioResponse()
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                }).ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar: {ex.Message}");
            }
        }


        [HttpPost("Validar")]
        public async Task<IActionResult> ValidarUsuarioAsync([FromBody] UsuarioValidar usuarioValidar)
        {
            try
            {
                var usuarioDominio = new Usuario()
                {
                    Email = usuarioValidar.Email,
                    Senha = usuarioValidar.Senha
                };

                usuarioDominio = await _usuarioAplicacao.ValidarUsuario(usuarioDominio);

                var usuarioValido = new UsuarioResponse()
                {
                    Id = usuarioDominio.Id,
                    Nome = usuarioDominio.Nome,
                    Email = usuarioDominio.Email
                };

                return Ok(usuarioValido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao validar usuario: {ex.Message}");
            }
        }


        #endregion

    }
}
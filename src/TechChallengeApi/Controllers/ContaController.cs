using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.DTOs;
using FIAP.FCG.WebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace FIAP.FCG.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJogoService _jogoService;
        private readonly IJwtService _jwtService;

        public ContaController(
            IUsuarioService usuarioService,
            IJogoService jogoService,
            IJwtService jwtService)
        {
            _usuarioService = usuarioService;
            _jogoService = jogoService;
            _jwtService = jwtService;
        }

        [SwaggerOperation(OperationId = "GetBibliotecaAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Bibliotecas encontradas para o usuário")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Bibliotecas não encontrada para o usuário")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao buscar bibliotecas")]
        [HttpGet("biblioteca")]
        [Authorize]
        public async Task<IActionResult> Biblioteca()
        {
            try
            {
                var result = await _jogoService.ObterJogosDoUsuario(User.GetId());

                if (result.IsNotFound())
                    return NotFound(result.Errors);

                return Ok(result.Value);
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
        }

        [SwaggerOperation(OperationId = "LoginAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Usuário logado com sucesso")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Usuário não autorizado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar login")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _usuarioService.LoginAsync(dto);

                if (result.IsInvalid())
                    return BadRequest(result.Errors);
                if (result.IsUnauthorized())
                    return Unauthorized();
                
                return Ok(_jwtService.GenerateToken(result.Value));
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    error = e.Message
                });
            }
        }
    }
}

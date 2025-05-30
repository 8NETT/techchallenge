using Application.Contracts;
using Application.DTOs;
using Ardalis.Result;
using Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using TechChallenge.Models;
using TechChallenge.Security;

namespace TechChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJogoService _jogoService;
        private readonly IJwtService _jwtService;

        public AccountController(
            IUsuarioService usuarioService,
            IJwtService jwtService)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
        }

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

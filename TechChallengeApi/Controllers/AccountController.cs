using Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Models;
using TechChallenge.Security;

namespace TechChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;

        public AccountController(
            IUnitOfWork unitOfWork, 
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Biblioteca()
        {
            try
            {
                return Ok(await _unitOfWork.UsuarioRepository.ObterJogosPorUsuarioAsync(User.GetId()));
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
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var usuario = await _unitOfWork.UsuarioRepository.ObterPorEmailAsync(model.Email);

                if (usuario == null)
                    return Unauthorized();
                if (!_passwordHasher.Verify(model.Password, usuario.Password))
                    return Unauthorized();

                return Ok(_jwtService.GenerateToken(usuario));
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

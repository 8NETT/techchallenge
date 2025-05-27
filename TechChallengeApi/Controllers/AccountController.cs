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
        private IUsuarioRepository _usuarioRepository;
        private IPasswordHasher _passwordHasher;
        private IJwtService _jwtService;

        public AccountController(
            IUsuarioRepository usuarioRepository, 
            IPasswordHasher passwordHasher,
            IJwtService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        [Authorize]
        public IActionResult Biblioteca() =>
            Ok(_usuarioRepository.ObterJogosPorUsuario(User.GetId()));

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = await _usuarioRepository.ObterPorEmailAsync(model.Email);

            if (usuario == null)
                return Unauthorized();
            if (!_passwordHasher.Verify(model.Password, usuario.Password))
                return Unauthorized();

            return Ok(_jwtService.GenerateToken(usuario));
        }
    }
}

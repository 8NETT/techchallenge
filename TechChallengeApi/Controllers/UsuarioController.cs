using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechChallenge.Models;
using TechChallenge.Security;

namespace TechChallenge.Controllers;

[ApiController]
[Route("/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public UsuarioController(
        IUsuarioRepository usuarioRepository, 
        IPasswordHasher passwordHasher, 
        IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            // throw new Exception("Erro ao obter usuários"); // Simulando um erro para teste
            return Ok(_usuarioRepository.ObterTodos());
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        try
        {
            return Ok(_usuarioRepository.ObterPorId(id));
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] UsuarioInput input)
    {
        try
        {
            var usuario = new Usuario()
            {
                Nome = input.Nome,
                Email = input.Email,
                Password = _passwordHasher.Hash(input.Password),
                Profile = false,
            };

            _usuarioRepository.Cadastar(usuario);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromRoute] int id, [FromBody] UsuarioInput input)
    {
        try
        {
            var usuario = _usuarioRepository.ObterPorId(id);

            usuario.Nome = input.Nome;
            usuario.Email = input.Email;
            usuario.Password = _passwordHasher.Hash(input.Password);

            _usuarioRepository.Alterar(usuario);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        try
        {
            _usuarioRepository.Deletar(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet("jogos/{usuarioId}")]
    public IActionResult ListarJogosDoUsuario([FromRoute] int usuarioId)
    {
        try
        {
            return Ok(_usuarioRepository.ObterJogosPorUsuario(usuarioId));
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpPost("{usuarioId}/jogo/{jogoId}")]
    public IActionResult AdicionarJogoAoUsuario(int usuarioId, int jogoId)
    {
        try
        {
            _usuarioRepository.VincularJogoAoUsuario(jogoId, usuarioId);
            return Ok(new { mensagem = "Jogo vinculado com sucesso." });
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpDelete("{usuarioId}/jogo/{jogoId}")]
    public IActionResult RemoverJogoDoUsuario(int usuarioId, int jogoId)
    {
        try
        {
            _usuarioRepository.DesvincularJogoDoUsuario(jogoId, usuarioId);
            return Ok(new { mensagem = "Jogo desvinculado com sucesso." });
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

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

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var email = User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email);

        return Ok(usuario);
    }
}
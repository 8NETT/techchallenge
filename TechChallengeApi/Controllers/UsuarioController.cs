using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Security;

namespace TechChallenge.Controllers;

[ApiController]
[Route("/[controller]")]
[Authorize(Policy = "Administrador")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UsuarioController(
        IUsuarioRepository usuarioRepository, 
        IPasswordHasher passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
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
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

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

    [HttpGet("jogos/{usuarioId:int}")]
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

    [HttpPost("jogos/{usuarioId:int}")]
    public IActionResult AdicionarJogoAoUsuario([FromRoute] int usuarioId, [FromBody] int jogoId)
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

    [HttpDelete("jogos/{usuarioId:int}")]
    public IActionResult RemoverJogoDoUsuario([FromRoute] int usuarioId, [FromBody] int jogoId)
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
}
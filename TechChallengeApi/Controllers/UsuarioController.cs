using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Controllers;

[ApiController]
[Route("/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
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
        try
        {
            var usuario = new Usuario()
            {
                Nome = input.Nome,
                Email = input.Email,
                Password = input.Password,
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
}
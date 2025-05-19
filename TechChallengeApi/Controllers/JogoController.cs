using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Controllers;

[ApiController]
[Route("/[controller]")]
public class JogoController : ControllerBase
{
    private readonly IJogoRepository _jogoRepository;

    public JogoController(IJogoRepository jogoRepository)
    {
        _jogoRepository = jogoRepository;
    }


    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_jogoRepository.ObterTodos());
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
            return Ok(_jogoRepository.ObterPorId(id));
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
    public IActionResult Post([FromBody] JogoInput input)
    {
        try
        {
            //Todo: implmentar validacao de perfil admin
            var jogo = new Jogo()
            {
                Nome = input.Nome,
                Valor = input.Valor,
                Desconto = input.Desconto,
            };

            _jogoRepository.Cadastar(jogo);

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
    public IActionResult Put([FromRoute] int id, [FromBody] JogoInput input)
    {
        try
        {
            //Todo: implmentar validacao de perfil admin
            var jogo = _jogoRepository.ObterPorId(id);

            jogo.Nome = input.Nome;
            jogo.Valor = input.Valor;
            jogo.Desconto = input.Desconto;

            _jogoRepository.Alterar(jogo);

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
            //Todo: implmentar validacao de perfil admin
            _jogoRepository.Deletar(id);
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
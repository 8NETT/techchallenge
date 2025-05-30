using Core.Entity;
using Core.Input;
using Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Controllers;

[ApiController]
[Route("/[controller]")]
[Authorize]
public class JogoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public JogoController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _unitOfWork.JogoRepository.ObterTodosAsync());
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(id);

            if (jogo == null)
                return NotFound();

            return Ok(jogo);
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
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Post([FromBody] JogoInput input)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var jogo = new Jogo()
            {
                Nome = input.Nome,
                Valor = input.Valor,
                Desconto = input.Desconto,
            };

            _unitOfWork.JogoRepository.Cadastrar(jogo);
            await _unitOfWork.CommitAsync();

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
    
    [HttpPut("{id:int}")]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] JogoInput input)
    {
        try
        {
            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(id);

            if (jogo == null)
                return NotFound();

            jogo.Nome = input.Nome;
            jogo.Valor = input.Valor;
            jogo.Desconto = input.Desconto;

            _unitOfWork.JogoRepository.Alterar(jogo);
            await _unitOfWork.CommitAsync();

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

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var jogo = await _unitOfWork.JogoRepository.ObterPorIdAsync(id);

            if (jogo == null)
                return NotFound();

            _unitOfWork.JogoRepository.Deletar(jogo);
            await _unitOfWork.CommitAsync();

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
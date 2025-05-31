using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.FCG.WebApi.Controllers;

[ApiController]
[Route("/[controller]")]
[Authorize]
public class JogoController : ControllerBase
{
    private readonly IJogoService _jogoService;

    public JogoController(IJogoService jogoService)
    {
        _jogoService = jogoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _jogoService.ObterTodosAsync());
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
            var result = await _jogoService.ObterPorIdAsync(id);

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

    [HttpPost]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Post([FromBody] CadastrarJogoDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _jogoService.CadastrarAsync(dto);

            if (result.IsInvalid())
                return BadRequest(result.Errors);
            if (result.IsConflict())
                return Conflict(result.Errors);

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
    
    [HttpPut]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Put([FromBody] AlterarJogoDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _jogoService.AlterarAsync(dto);

            if (result.IsInvalid())
                return BadRequest(result.Errors);
            if (result.IsNotFound())
                return NotFound(result.Errors);
            if (result.IsConflict())
                return Conflict(result.Errors);

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

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "Administrador")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var result = await _jogoService.DeletarAsync(id);

            if (result.IsNotFound())
                return NotFound(result.Errors);

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
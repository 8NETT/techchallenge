using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.FCG.WebApi.Controllers;

[ApiController]
[Route("/[controller]")]
[Authorize(Policy = "Administrador")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _usuarioService.ObterTodosAsync());
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
            var result = await _usuarioService.ObterPorIdAsync(id);

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
    public async Task<IActionResult> Post([FromBody] CadastrarUsuarioDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _usuarioService.CadastrarAsync(dto);

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

    public async Task<IActionResult> Put([FromBody] AlterarUsuarioDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _usuarioService.AlterarAsync(dto);

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
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var result = await _usuarioService.DeletarAsync(id);

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
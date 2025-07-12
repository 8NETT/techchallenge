using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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

    [SwaggerOperation(OperationId = "GetJogoAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Jogos listados com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de jogos")]
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

    [SwaggerOperation(OperationId = "GetJogoPOrIdAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Jogo encontrado com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de jogo")]
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


    [SwaggerOperation(OperationId = "PostJogoAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Jogo cadastrado com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Jogo já cadastrado")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de jogo")]
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

    [SwaggerOperation(OperationId = "PutJogoAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Jogo atualizado com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Jogo já cadastrado")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Jogo não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar atualização de jogo")]
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

    [SwaggerOperation(OperationId = "DeleteJogoAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Jogo deletado com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Jogo não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar deleção de jogo")]
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
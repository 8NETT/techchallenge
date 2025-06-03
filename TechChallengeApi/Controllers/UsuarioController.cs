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
[Authorize(Policy = "Administrador")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [SwaggerOperation(OperationId = "GetUsuarioAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Usu�rios obtidos com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de usu�rios")]
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

    [SwaggerOperation(OperationId = "GetUsuarioPorIdAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Usu�rio obtido com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Usu�rio n�o encontrado")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de usu�rio por id")]
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

    [SwaggerOperation(OperationId = "PostUsuarioAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Usu�rio cadastrado com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Usu�rio j� cadastrado")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar cadastro de usu�rio")]
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

    [SwaggerOperation(OperationId = "PutUsuarioAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Usu�rio atualizado com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Usu�rio j� cadastrado")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Usu�rio n�o encontrado")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar atualiza��o de usu�rio")]
    [HttpPut]
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

    [SwaggerOperation(OperationId = "DeleteUsuarioAsync")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Usu�rio deletado com sucesso")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Usu�rio n�o encontrado")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar dele��o de usu�rio")]
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
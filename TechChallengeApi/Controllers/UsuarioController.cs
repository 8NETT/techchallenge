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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UsuarioController(
        IUnitOfWork unitOfWork, 
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(await _unitOfWork.UsuarioRepository.ObterTodosAsync());
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
            var usuario = await _unitOfWork.UsuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
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
    public async Task<IActionResult> Post([FromBody] UsuarioInput input)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = new Usuario()
            {
                Nome = input.Nome,
                Email = input.Email,
                Password = _passwordHasher.Hash(input.Password),
                Profile = false,
            };

            _unitOfWork.UsuarioRepository.Cadastrar(usuario);
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
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UsuarioInput input)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var usuario = await _unitOfWork.UsuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            usuario.Nome = input.Nome;
            usuario.Email = input.Email;
            usuario.Password = _passwordHasher.Hash(input.Password);

            _unitOfWork.UsuarioRepository.Alterar(usuario);
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
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var usuario = await _unitOfWork.UsuarioRepository.ObterPorIdAsync(id);

            if (usuario == null)
                return NotFound();

            _unitOfWork.UsuarioRepository.Deletar(usuario);
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

    [HttpGet("jogos/{usuarioId:int}")]
    public IActionResult ListarJogosDoUsuario([FromRoute] int usuarioId)
    {
        try
        {
            return Ok(_unitOfWork.UsuarioRepository.ObterJogosPorUsuarioAsync(usuarioId));
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpPost("jogos/{usuarioId:int}")]
    public async Task<IActionResult> AdicionarJogoAoUsuario([FromRoute] int usuarioId, [FromBody] int jogoId)
    {
        try
        {
            _unitOfWork.UsuarioRepository.VincularJogoAoUsuarioAsync(jogoId, usuarioId);
            await _unitOfWork.CommitAsync();

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
            _unitOfWork.UsuarioRepository.DesvincularJogoDoUsuarioAsync(jogoId, usuarioId);
            return Ok(new { mensagem = "Jogo desvinculado com sucesso." });
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}
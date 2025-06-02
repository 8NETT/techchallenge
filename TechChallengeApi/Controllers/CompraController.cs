using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.WebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.FCG.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompraController : ControllerBase
    {
        private ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _compraService.ObterDoUsuarioAsync(User.GetId());

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

        [HttpPost("{jogoId:int}")]
        public async Task<IActionResult> Post([FromRoute] int jogoId)
        {
            try
            {
                var result = await _compraService.ComprarAsync(User.GetId(), jogoId);

                if (result.IsNotFound())
                    return NotFound(result.Errors);
                if (result.IsConflict())
                    return Conflict(result.Errors);

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

        [HttpPost("estornar/{id:int}")]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> Estornar([FromRoute] int id)
        {
            try
            {
                var result = await _compraService.EstornarAsync(id);

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
}

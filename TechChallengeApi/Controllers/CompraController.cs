using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int usuarioId, [FromBody] int jogoId)
        {
            try
            {
                var result = await _compraService.ComprarAsync(usuarioId, jogoId);

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

        [HttpPost("estornar")]
        [Authorize(Policy = "Administrador")]
        public async Task<IActionResult> Estornar([FromBody] int usuarioId, [FromBody] int jogoId)
        {
            try
            {
                var result = await _compraService.EstornarAsync(usuarioId, jogoId);

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
    }
}

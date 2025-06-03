using Ardalis.Result;
using FIAP.FCG.Application.Contracts;
using FIAP.FCG.WebApi.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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

        [SwaggerOperation(OperationId = "GetCompraAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Compras do usuário")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Usuário informado não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de compras")]
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

        [SwaggerOperation(OperationId = "PostCompraAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Criada uma nova compra para o usuário")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Usuário informado não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.Conflict, "Usuário já possui jogo comprado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de compras")]
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

        [SwaggerOperation(OperationId = "PostEstornoAsync")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Estorno realizado")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Usuário informado não encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro ao efetuar busca de compras")]
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

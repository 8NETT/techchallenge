using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Core.Entity;

namespace FIAP.FCG.Application.Mappers
{
    public static class CompraMapper
    {
        public static CompraDTO ToDTO(this Compra entidade) => new CompraDTO
        {
            Id = entidade.Id,
            UsuarioId = entidade.Comprador.Id,
            JogoId = entidade.Jogo.Id,
            Valor = entidade.Valor,
            Desconto = entidade.Desconto,
            Total = entidade.Total
        };
    }
}

using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Core.Entity;

namespace FIAP.FCG.Application.Mappers
{
    internal static class JogoMapper
    {
        public static JogoDTO ToDTO(this Jogo entidade) => new JogoDTO
        {
            Nome = entidade.Nome,
            Valor = entidade.Valor,
            Desconto = entidade.Desconto
        };

        public static Jogo ToEntity(this CadastrarJogoDTO dto) => new Jogo
        {
            Nome = dto.Nome,
            Valor = dto.Valor,
            Desconto = dto.Desconto,
            DataCriacao = DateTime.Now
        };

        public static Jogo ToEntity(this AlterarJogoDTO dto, Jogo entidade) => new Jogo
        {
            Id = entidade.Id,
            DataCriacao = entidade.DataCriacao,
            Nome = dto.Nome,
            Valor = dto.Valor,
            Desconto = dto.Desconto
        };
    }
}

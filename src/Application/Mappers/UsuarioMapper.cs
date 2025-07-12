using FIAP.FCG.Application.DTOs;
using FIAP.FCG.Core.Entity;

namespace FIAP.FCG.Application.Mappers
{
    internal static class UsuarioMapper
    {
        public static UsuarioDTO ToDTO(this Usuario entidade) => new UsuarioDTO
        {
            Id = entidade.Id,
            Nome = entidade.Nome,
            Email = entidade.Email,
            Profile = entidade.Profile,
            DataCriacao = entidade.DataCriacao
        };

        public static Usuario ToEntity(this CadastrarUsuarioDTO dto, string passwordHash) => Usuario.New()
            .Nome(dto.Nome)
            .Email(dto.Email)
            .DataCriacao(DateTime.Now)
            .Password(passwordHash)
            .Profile(dto.Profile)
            .Build();

        public static Usuario ToEntity(this AlterarUsuarioDTO dto, Usuario entidade, string passwordHash) => Usuario.New()
            .Id(entidade.Id)
            .DataCriacao(entidade.DataCriacao)
            .Nome(dto.Nome)
            .Email(dto.Email)
            .Password(passwordHash)
            .Profile(dto.Profile)
            .Build();
    }
}

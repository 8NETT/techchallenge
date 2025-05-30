using Application.DTOs;
using Core.Entity;

namespace Application.Mappers
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

        public static Usuario ToEntity(this CadastrarUsuarioDTO dto, string passwordHash) => new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Profile = dto.Profile,
            DataCriacao = DateTime.Now,
            Password = passwordHash
        };

        public static Usuario ToEntity(this AlterarUsuarioDTO dto, Usuario entidade, string passwordHash) => new Usuario
        {
            Id = entidade.Id,
            DataCriacao = entidade.DataCriacao,
            Nome = dto.Nome,
            Email = dto.Email,
            Profile = dto.Profile,
            Password = passwordHash
        };
    }
}

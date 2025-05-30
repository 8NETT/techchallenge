using Application.DTOs;
using Core.Entity;

namespace TechChallenge.Security
{
    public interface IJwtService
    {
        string GenerateToken(UsuarioDTO usuario);
    }
}

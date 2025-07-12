using FIAP.FCG.Application.DTOs;

namespace FIAP.FCG.WebApi.Security
{
    public interface IJwtService
    {
        string GenerateToken(UsuarioDTO usuario);
    }
}

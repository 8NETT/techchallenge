using Core.Entity;

namespace TechChallenge.Security
{
    public interface IJwtService
    {
        string GenerateToken(Usuario usuario);
    }
}

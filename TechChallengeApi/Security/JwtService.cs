using FIAP.FCG.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAP.FCG.WebApi.Security
{
    public class JwtService : IJwtService
    {
        private string _key;
        private string _issuer;

        public JwtService(string key, string issuer)
        {
            _key = key;
            _issuer = issuer;
        }

        public string GenerateToken(UsuarioDTO usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                claims: GenerateClaims(usuario),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private Claim[] GenerateClaims(UsuarioDTO usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString())
            };

            if (usuario.Profile)
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            else
                claims.Add(new Claim(ClaimTypes.Role, "Usuário"));

            return claims.ToArray();
        }
    }
}

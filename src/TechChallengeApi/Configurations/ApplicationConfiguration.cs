using FIAP.FCG.Application.Contracts;
using FIAP.FCG.Application.Security;
using FIAP.FCG.Application.Services;

namespace FIAP.FCG.WebApi.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IJogoService, JogoService>();
            builder.Services.AddScoped<ICompraService, CompraService>();
        }
    }
}

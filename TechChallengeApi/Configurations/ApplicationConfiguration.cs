using Application.Contracts;
using Application.Security;
using Application.Services;

namespace TechChallenge.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IJogoService, JogoService>();
        }
    }
}

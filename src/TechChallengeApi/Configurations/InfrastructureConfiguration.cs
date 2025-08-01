﻿using FIAP.FCG.Core.Repository;
using FIAP.FCG.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.FCG.WebApi.Configurations
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ConnectionString")
                ?? throw new InvalidOperationException("ConnectionString não localizada no arquivo de configuração.");

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => { options.UseSqlServer(connectionString); }, ServiceLifetime.Scoped);

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IJogoRepository, JogoRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

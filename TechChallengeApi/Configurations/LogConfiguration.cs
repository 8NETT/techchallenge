﻿using Serilog;

namespace FIAP.FCG.WebApi.Configurations
{
    public static class LogConfiguration
    {
        public static void AddLogConfiguration(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}

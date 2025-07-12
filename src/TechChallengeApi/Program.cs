using FIAP.FCG.WebApi.Configurations;
using FIAP.FCG.WebApi.Middleware;
using FIAP.FCG.Infrastructure.Repository;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogConfiguration();
builder.AddApiConfiguration();
builder.AddInfrastructureConfiguration();
builder.AddApplicationConfiguration();
builder.AddAuthenticationConfiguration();
builder.AddAuthorizationConfiguration();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var dbContext = services.GetRequiredService<ApplicationDbContext>();

//        await dbContext.Database.MigrateAsync();

//        Console.WriteLine("Migrations applied successfully.");
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while applying migrations.");
//    }
//}

app.UseErrorLogging();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
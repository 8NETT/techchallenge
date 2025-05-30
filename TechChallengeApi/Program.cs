using TechChallenge.Configurations;
using TechChallenge.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogConfiguration();
builder.AddApiConfiguration();
builder.AddInfrastructureConfiguration();
builder.AddApplicationConfiguration();
builder.AddAuthenticationConfiguration();
builder.AddAuthorizationConfiguration();

var app = builder.Build();

app.UseErrorLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
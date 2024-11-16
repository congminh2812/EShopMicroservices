using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddApplicationServices()
    .AddInfrastructureServices(configuration)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await DatabaseExtensions.InitializeDatabaseAsync(context);
}

app.Run();

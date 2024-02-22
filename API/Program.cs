using API.Database;
using API.Entities.Identity;
using API.Extensions;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var assembly = typeof(Program).Assembly;

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

// Configure MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

// Configure Carter - used to create endpointes from vertical slice
builder.Services.AddCarter();

// Configure Validators
builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

app.UseCors("CorsPolicy");
app.UseSwaggerDocumentation();
app.UseAuthorization();
app.UseAuthentication();
app.UseHttpsRedirection();

// Map Carter Routes
app.MapCarter();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ApplicationDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await ApplicationDbContextSeed.SeedUsersAsync(userManager, config);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
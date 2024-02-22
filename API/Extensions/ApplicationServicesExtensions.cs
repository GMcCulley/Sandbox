using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Services;
using API.Database;
using API.Shared;
using API.Services;
using API.Interfaces;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value?.Errors != null && e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new Response();
                    // {
                    //     Errors = errors.ToList(),
                    // };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

            return services;
        }
    }
}
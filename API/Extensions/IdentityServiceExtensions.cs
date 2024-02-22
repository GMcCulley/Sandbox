using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Database;
using API.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
            IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(options => 
            {
                // add identity options here
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            // Configure Identity
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 12;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider("MyApp", typeof(DataProtectorTokenProvider<AppUser>));

            // Configure Authentication & Issuer Token
            services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            config["Jwt:Key"] ??
                                throw new InvalidOperationException("Connection string 'Jwt:Key' not found")
                            )),
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false,
                    };
                });

            services.AddAuthorization(options =>
                {
                    var authorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                        JwtBearerDefaults.AuthenticationScheme
                    );
                    options.DefaultPolicy = authorizationPolicyBuilder.RequireAuthenticatedUser().Build();
                });

            return services;
        }
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.Interfaces;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Context;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Models;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Services;

/// <summary>
/// we need to keep our business related entities separate from the identity management
/// </summary>
namespace Yamaanco.Infrastructure.EF.Identity.Persistence.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpContextAccessor();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<YamaancoIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IAccountService, AccountService>();

            services.Configure<JwtOptions>(config.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config["JWTSettings:Issuer"],
                    ValidAudience = config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]))
                };
                o.Events = new JwtBearerEvents()
                {
                    //OnAuthenticationFailed = context =>
                    //{
                    //    context.Response.OnStarting(async () =>
                    //    {
                    //        context.NoResult();
                    //        context.Response.Headers.Add("Token-Expired", "true");
                    //        context.Response.ContentType = "text/plain";
                    //        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    //        await context.Response.WriteAsync("Un-Authorized");
                    //    });

                    //    return Task.CompletedTask;
                    //},

                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                        return context.Response.WriteAsync(result);
                    },
                };
            });
        }
    }
}
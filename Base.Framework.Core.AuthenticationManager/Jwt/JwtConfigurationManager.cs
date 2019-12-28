using System;
using System.Text;
using System.Threading.Tasks;
using Base.Framework.Core.AuthenticationManager.Jwt.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Base.Framework.Core.AuthenticationManager.Jwt
{
    public static class JwtConfigurationManager
    {
        public static void JwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            var jwtOptions = new JwtOption();
            configuration.GetSection("Messages").Bind(jwtOptions);
            var key = Encoding.ASCII.GetBytes(jwtOptions.Key);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
               .AddJwtBearer(x =>
               {
                   x.Events = new JwtBearerEvents
                   {
                       //It is the first event that meets and accepts all requests from the Client, whether token or not.
                       OnMessageReceived = context => Task.CompletedTask,
                       //If the token sent with the request is valid, it is triggered and verification procedures are performed
                       OnTokenValidated = context =>
                       {
                           //var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                           var userId = int.Parse(context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       },
                       //The token that came with the request is invalid, worn or corrupted
                       OnAuthenticationFailed = context =>
                       {
                           Console.WriteLine($"Exception : {context.Exception.Message}");
                           return Task.CompletedTask;
                       },
                       //
                      
                   };
                   x.RequireHttpsMetadata = false;
                   x.SaveToken = true;
                   x.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateLifetime = true,
                       ValidateIssuer = false,
                       //ValidIssuer = "The name of the issuer",
                       ValidateAudience = false,
                       //ValidAudience = "The name of the audience",
                       //ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                   };
               }); 
        }

    }
}

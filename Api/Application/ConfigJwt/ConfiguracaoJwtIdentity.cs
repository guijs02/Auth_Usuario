using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Api.Application.Authorization;
using UsuariosApi.Api.Domain.Data.Dtos;

namespace UsuariosApi.Api.Application.ConfigJwt
{
    public static class ConfiguracaoJwtIdentity
    {
        public static AuthenticationBuilder ConfigJwtBearer(IServiceCollection services)
        {
            return services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
                                "fjdik4343493ADFJFAK933432FDxxs&$#33444fsjdbabaii(9%22"
                            )
                        ),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = async context =>
                        {
                            if (
                                context.Exception.GetType() == typeof(SecurityTokenExpiredException)
                            )
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                return;
                            }
                        },


                    };
                });
        }

        public static IServiceCollection ConfigAddPolicy(IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "IdadeMinima",
                    policy => policy.AddRequirements(new IdadeMinima(18))
                );

                options.AddPolicy(
                    "ApenasSupervisor",
                    policy => policy.AddRequirements(new Supervisor(ETipoCargo.Supervisor))
                );
            });
        }

        public static IServiceCollection ConfigIdentityOptions(IServiceCollection services)
        {
            return services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 3;
            });
        }
    }
}

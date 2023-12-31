using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Api.Domain.Models;
using UsuariosApi.Api.Application.Authorization;
using UsuariosApi.Api.Application.ConfigJwt;
using UsuariosApi.Api.Application.Services;
using UsuariosApi.Api.Infraestructure.Context;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlServer<UsuarioDbContext>(
    builder.Configuration["ConnectionStrings:Database"]
);

builder.Services.AddDbContext<UsuarioDbContext>();

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<TokenService>();

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();
builder.Services.AddSingleton<IAuthorizationHandler, SupervisorAuthorization>();

//código omitido

ConfiguracaoJwtIdentity.ConfigJwtBearer(builder.Services);

ConfiguracaoJwtIdentity.ConfigAddPolicy(builder.Services);

ConfiguracaoJwtIdentity.ConfigIdentityOptions(builder.Services);
//código omitido

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme{
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
});

var app = builder.Build();

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

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsuariosApi.Api.Domain.Models;

namespace UsuariosApi.Api.Infraestructure.Context
{
    public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options)
        {
        }
    }
}
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Api.Domain.Data.Dtos;

namespace UsuariosApi.Api.Domain.Models
{
    public class Usuario : IdentityUser
    {
        public DateTime DataNascimento { get; set; }
        public ETipoCargo TipoCargo { get; set; }
        public Usuario() : base() { }
    }
}
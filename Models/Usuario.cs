using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Models
{
    public class Usuario : IdentityUser
    {
     public DateTime DataNascimento { get; set; }   
     public ETipoCargo TipoCargo { get; set; }   
     public Usuario() : base() { }
    }
}
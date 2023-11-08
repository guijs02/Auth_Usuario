using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Api.Application.Authorization
{
    public class IdadeMinima : IAuthorizationRequirement
    {
        public int Idade { get; set; }
        public IdadeMinima(int idade)
        {
            Idade = idade;
        }
    }
}
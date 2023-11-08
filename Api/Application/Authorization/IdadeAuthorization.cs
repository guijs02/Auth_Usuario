using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Api.Application.Authorization
{
    //O authorizationHandler diz ao .NET que está classe será a responsavel por validar a permissão
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var dataNascimentoClaim = context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth);

            if (dataNascimentoClaim is null)
                //diz que a tarefa foi completada
                return Task.CompletedTask;

            var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

            var idadeUsuario = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddYears(-idadeUsuario))
                idadeUsuario--;

            if (idadeUsuario >= requirement.Idade)
                //indica que a condição para a autorização foi satisfeita
                context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }
}